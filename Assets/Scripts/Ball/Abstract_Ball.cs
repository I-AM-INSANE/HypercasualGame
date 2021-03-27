using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Abstract_Ball : MonoBehaviour, IInteractable
{
    #region Fields

    private Vector3 startPosition;
    private Rigidbody2D rb2d;
    private float magnitude = 10f;  // Величина импульса для мяча
    private float maxDistance = 1f; // Максимальаня дистанция, на которую может отдоляться мяч при запуске
    private float minDistanceToShot = 0.2f; // Минимальное расстояние, на которое нужно отдалить мяч для его запуска
    private int bouncesNumber = 1;  // Количество отскоков для  мяча
    private bool interactable = true;   // Можно ли интерактировать с мячом
    LineRenderer aimRope;   // Верёвка, которая растягивается при прицеливании
    private int ballsForEnemyKill;  // Количество мячей, которые прибавятся за убийство врага
    private int killCount = 0;  // Количество убийств данным шариком (нужно для проверки на серию убийств)
    private bool wasCollision = false;  // Запрещает затронуть 2 GameBorder одновременно

    #endregion

    #region Properties

    bool IInteractable.IsInteractable
    {
        get { return interactable; }
    }

    public Enum_Elements Element { get; private set; }

    #endregion

    #region Methods
    private void Awake()
    {
        startPosition = transform.position;
        rb2d = GetComponent<Rigidbody2D>();

        aimRope = GetComponent<LineRenderer>();
        aimRope.positionCount = 2;
        aimRope.SetPosition(0, startPosition);
        aimRope.startColor = Color.gray;
        aimRope.endColor = GetComponent<SpriteRenderer>().color;

        Element = Enum_Elements.Standard;
        Physics2D.IgnoreLayerCollision(8, 8);
    }
    private void Start()
    {
    }

    private void Update()
    {
        aimRope.SetPosition(1, transform.position);     // Посылаем позицию мяча для Line renderer 

        transform.right = GetComponent<Rigidbody2D>().velocity.normalized;
    }

    private void OnMouseDrag()
    {
        if (interactable)
            Aiming();
    }

    private void OnMouseUp()
    {
        if (interactable)
        {
            Vector3 direction = startPosition - transform.position;
            if (Vector3.Distance(transform.position, startPosition) > minDistanceToShot)
            {
                GetComponent<Trajectory>().DestroyTrajectory();    // Уничтожаем траекторию при запуске снаряда
                rb2d.AddForce(direction * magnitude, ForceMode2D.Impulse);
                interactable = false;
                aimRope.enabled = false;
                GetComponent<CircleCollider2D>().radius = GetComponent<SpriteRenderer>().bounds.extents.x;  // Коллайдер меняю на адекватный размер
            }
            else
                transform.position = startPosition;
        }
    }

    private void Aiming()
    {
        aimRope.enabled = true;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = -Camera.main.transform.position.z;
        if (Vector3.Distance(startPosition, newPosition) > maxDistance)
            newPosition = startPosition + (newPosition - startPosition).normalized * maxDistance;
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameBorder") && !wasCollision)
        {
            CollisionWithBorder(collision);
            ChangeBallElement(collision);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameBorder"))
            wasCollision = false;
    }

    private void CollisionWithBorder(Collision2D collision)
    {
        bouncesNumber--;

        if (bouncesNumber < 0) // Уничтожаем мяч, если кол-во отскоков меньше 0
        {
            Destroy(gameObject);
            GameManager.Instance.BallsOnScreen--;
            if (killCount == 0)
                OnStreakEnd();
        }
        wasCollision = true;
    }

    private void ChangeBallElement(Collision2D collision)   // Замена стихии мяча
    {
        if (collision.gameObject.GetComponent<GameBorder>().Element == Enum_Elements.Fire)
        {
            gameObject.GetComponent<Animator>().Play("Fireball");
            Element = Enum_Elements.Fire;
        }

        if (collision.gameObject.GetComponent<GameBorder>().Element == Enum_Elements.Water)
        {
            gameObject.GetComponent<Animator>().Play("Waterball");
            Element = Enum_Elements.Water;
        }
    }

    private void OnStreakEnd()  // Закончить серию убийств
    {
        GameManager.Instance.KillStreak = 0;
        GameManager.Instance.ChangeMultiplier();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Столкновение с врагом
        {
            Enum_Elements enemyElement = collision.GetComponent<Abstract_Enemy>().Element;
            // Проверка соответствия стихий
            if (enemyElement == Enum_Elements.Fire && Element == Enum_Elements.Water ||
                enemyElement == Enum_Elements.Water && Element == Enum_Elements.Fire)
            {
                CollisionWithEnemy(collision);
            }
        }
    }

    private void CollisionWithEnemy(Collider2D collision)
    {
        // Анимация врыва врагов
        if (collision.gameObject.GetComponent<Abstract_Enemy>().Element == Enum_Elements.Water)
            Instantiate(Resources.Load<GameObject>("FireBurst"), collision.gameObject.transform.position, Quaternion.identity);
        if (collision.gameObject.GetComponent<Abstract_Enemy>().Element == Enum_Elements.Fire)
            Instantiate(Resources.Load<GameObject>("SmokeBurst"), collision.gameObject.transform.position, Quaternion.identity);

        Destroy(collision.gameObject);
        bouncesNumber++;
        killCount++;
        GameManager.Instance.Score++;

        BallIncreased();
    }

    private void BallIncreased()    // Увеличиваем количество мячей у игрока
    {
        GameManager.Instance.KillStreak++;
        GameManager.Instance.ChangeMultiplier();
        ballsForEnemyKill = GameManager.Instance.Multiplier;
        GameManager.Instance.ProjectileNumber += ballsForEnemyKill;
    }

    #endregion
}
