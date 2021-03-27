using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimationObj : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyAnimation());
    }
    IEnumerator DestroyAnimation()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
