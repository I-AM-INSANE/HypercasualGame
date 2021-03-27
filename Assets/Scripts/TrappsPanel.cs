using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrappsPanel : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject grenade;
    [SerializeField] GameObject dropBomb;
    private GameObject objectForSpawn;

    #endregion

    #region Methods

    public void OnGrenadePressed()
    {
        objectForSpawn = grenade;
    }
    public void OnDropBombPressed()
    {
        objectForSpawn = dropBomb;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(objectForSpawn);
        }
    }

    #endregion
}
