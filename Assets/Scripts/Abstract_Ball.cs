using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Ball : MonoBehaviour
{
    #region Fields

    private Vector3 startPosition;
    private Rigidbody2D rb2d;
    private float magnitude = 10f;  // Величина импульса для мяча
    private float maxDistance = 1f; // Максимальаня дистанция, на которую может отдоляться мяч при запуске
    private int bouncesNumber = 1;  // Количество отскоков для  мяча
    private bool interactable = true;   // Можно ли интерактировать с мячом
    LineRenderer aimRope;   // Верёвка, которая растягивается при прицеливании

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
    }
    void Start()
    {

    }

    void Update()
    {
        aimRope.SetPosition(1, transform.position);
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
            rb2d.AddForce(direction * magnitude, ForceMode2D.Impulse);
        }
        interactable = false;
        aimRope.enabled = false;
    }

    private void Aiming()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = -Camera.main.transform.position.z;
        if (Vector3.Distance(startPosition, newPosition) > maxDistance)
            newPosition = startPosition + (newPosition - startPosition).normalized * maxDistance;
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("MainCamera"))
        if (collision.gameObject.CompareTag("GameBorder"))
            CollisionWithBorder();
    }

    private void CollisionWithBorder()
    {
        bouncesNumber--;
        if (bouncesNumber < 0)
            Destroy(gameObject);
    }

    #endregion
}
