using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abstract_Enemy : MonoBehaviour
{
    #region Fields

    Rigidbody2D rb2d;
    private float magnitude;

    #endregion

    #region Methods

    private void Awake()
    {
        magnitude = Random.Range(1f, 3f);
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb2d.AddForce(magnitude * Vector2.down, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (transform.position.y < ScreenUtils.ScreenBottom)
        {
            if (GameManager.Instance.ProjectileNumber > 0)
            {
                GameManager.Instance.KillStreak = 0;
                GameManager.Instance.ProjectileNumber -= 3;
            }
            Destroy(gameObject);
        }
    }
    #endregion
}
