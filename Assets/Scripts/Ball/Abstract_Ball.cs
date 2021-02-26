using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Ball : MonoBehaviour, IInteractable
{
    #region Fields

    private Vector3 startPosition;
    private Rigidbody2D rb2d;
    private float magnitude = 10f;  // Величина импульса для мяча
    private float maxDistance = 1f; // Максимальаня дистанция, на которую может отдоляться мяч при запуске
    private float minDistanceToShot = 0.2f;
    private int bouncesNumber = 1;  // Количество отскоков для  мяча
    private bool interactable = true;   // Можно ли интерактировать с мячом
    LineRenderer aimRope;   // Верёвка, которая растягивается при прицеливании
    private bool ballIncreased = false; // Прибавился ли снаряд за столкновение с врагом
    private int ballsForEnemyKill;  // Количество мячей, которые прибавятся за убийство врага
    private int killCount = 0;  // Количество убийств данным шариком (нужно для проверки на серию убийств)

    SpriteRenderer spriteRenderer;

    #endregion

    bool IInteractable.IsInteractable
    {
        get { return interactable; }
    }

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

        spriteRenderer = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(8, 8);
    }
    void Start()
    {
    }

    void Update()
    {
        aimRope.SetPosition(1, transform.position);     // Посылаем позицию мяча для Line renderer 
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
        if (collision.gameObject.CompareTag("GameBorder"))
            CollisionWithBorder(collision);
    }

    private void CollisionWithBorder(Collision2D collision)
    {
        bouncesNumber--;
        spriteRenderer.color = collision.gameObject.GetComponent<SpriteRenderer>().color;   // Меняем цвет мяча на цвет стены

        if (bouncesNumber < 0) // Уничтожаем мяч, если кол-во отскоков меньше 0
        {
            Destroy(gameObject);
            OnStreakEnd();

        }
    }

    private void OnStreakEnd()
    {
        if (killCount == 0)
            GameManager.Instance.KillStreak = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Color collisionObjColor = collision.GetComponent<SpriteRenderer>().color;

        if (collision.gameObject.CompareTag("Enemy") && spriteRenderer.color == collisionObjColor)  // Столкновение с врагом
            CollisionWithEnemy(collision);

        if (collision.gameObject.CompareTag("MultiEnemy") && spriteRenderer.color != Color.white)   // Столкновение с Мульти врагом
            CollisionWithEnemy(collision);

    }

    private void CollisionWithEnemy(Collider2D collision)
    {
        Destroy(collision.gameObject);
        bouncesNumber++;
        killCount++;

        if (!ballIncreased)
        {
            BallIncreased();
            ballIncreased = true;
        }
    }

    private void BallIncreased()    // Увеличиваем количество мячей у игрока
    {
        GameManager.Instance.KillStreak++;
        if (GameManager.Instance.KillStreak > 0 && GameManager.Instance.KillStreak <= 3)
            ballsForEnemyKill = 1;
        if (GameManager.Instance.KillStreak > 3 && GameManager.Instance.KillStreak <= 6)
            ballsForEnemyKill = 2;
        if (GameManager.Instance.KillStreak > 6 && GameManager.Instance.KillStreak <= 10)
            ballsForEnemyKill = 3;
        if (GameManager.Instance.KillStreak > 10)
            ballsForEnemyKill = 4;

        GameManager.Instance.ProjectileNumber += ballsForEnemyKill;
    }

    #endregion
}
