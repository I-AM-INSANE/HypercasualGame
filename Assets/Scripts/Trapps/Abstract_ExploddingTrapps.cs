using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_ExploddingTrapps : MonoBehaviour
{
    #region Fields

    protected string trapAnimName;
    protected float explosionDelay;

    #endregion

    #region Methods
    private void Start()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionDelay);
        Animator anim = GetComponent<Animator>();
        anim.Play(trapAnimName);
        StartCoroutine(DestroyAnimation());
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length / 4);
        GetComponent<Collider2D>().enabled = true;
    }

    IEnumerator DestroyAnimation()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Debug.Log(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.Score++;
            Destroy(collision.gameObject);
        }
    }

    #endregion
}
