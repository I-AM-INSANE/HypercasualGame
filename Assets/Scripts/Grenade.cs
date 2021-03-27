using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    #region Fields

    private string trapAnimName;

    #endregion

    private void Start()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2);
        Instantiate(Resources.Load<GameObject>(trapAnimName), gameObject.transform.position, Quaternion.identity);
        yield return null;
    }
}
