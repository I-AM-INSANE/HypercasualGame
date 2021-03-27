using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        if (Input.GetMouseButtonDown(0) && objectForSpawn != null)
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        Vector3 positionForSpawn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionForSpawn.z = - Camera.main.transform.position.z;
        Instantiate(objectForSpawn, positionForSpawn, Quaternion.identity);
        objectForSpawn = null;
    }

    #endregion
}
