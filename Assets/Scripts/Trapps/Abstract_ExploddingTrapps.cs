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
        GameObject explosion = Instantiate(Resources.Load<GameObject>(trapAnimName), gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Debug.Log(explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        while (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length <=
            explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length / 2)
        {
            yield return null;
        }
        explosion.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("CHECK");
        }
    }

    #endregion
}
