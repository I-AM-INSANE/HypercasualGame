using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrappsCanvas : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject trappsPanel;

    [SerializeField] private GameObject prefabGrenade;
    [SerializeField] private GameObject prefabDropBomb;
    private GameObject objectForSpawn;

    private Touch touch;
    private RaycastHit2D hit;

    #endregion

    #region Methods
    private void Awake()
    {

    }
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
            {
                if (trappsPanel.activeSelf)
                {
                    trappsPanel.SetActive(false);
                }
                else
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                    if (hit.collider != null && hit.collider.CompareTag("Ball"))
                    {
                        return;
                    }
                    else
                        EnableTrappsPanel();
                }
            }
        }
    }
    public void OnGrenadePressed()
    {
        objectForSpawn = prefabGrenade;
        PlaceTrap();
    }
    public void OnDropBombPressed()
    {
        objectForSpawn = prefabDropBomb;
        PlaceTrap();
    }

    private void EnableTrappsPanel()
    {
        Vector3 positionForPanel = Camera.main.ScreenToWorldPoint(touch.position);
        positionForPanel.z = -Camera.main.transform.position.z;
        trappsPanel.transform.position = positionForPanel;
        trappsPanel.SetActive(true);
    }

    private void PlaceTrap()
    {
        Vector3 positionForSpawn = Camera.main.ScreenToWorldPoint(touch.position);
        positionForSpawn.z = - Camera.main.transform.position.z;
        Instantiate(objectForSpawn, positionForSpawn, Quaternion.identity);
        objectForSpawn = null;
        trappsPanel.SetActive(false);
    }

    #endregion
}
