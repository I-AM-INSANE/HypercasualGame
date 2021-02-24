using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBordersManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject gameBorderLeft;
    [SerializeField] private GameObject gameBorderRight;
    [SerializeField] private GameObject gameBorderTop;
    [SerializeField] private GameObject gameBorderBottom;

    private static GameBordersManager instance;

    #endregion

    #region Properties

    public static GameBordersManager Instance { get { return instance; } }

    public float BorderLeft { get; set; }
    public float BorderRight { get; set; }
    public float BorderTop { get; set; }
    public float BorderBottom { get; set; }

    #endregion

    #region Methods

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("Multiple instances of " + this.GetType().Name + " #1", gameObject);
            Debug.LogError("Multiple instances of " + this.GetType().Name + " #2", Instance.gameObject);
        }

        GameBorderToScreenSize();
        SearchWorldPointBorders();
    }

    private void GameBorderToScreenSize()   // Границы подстраиваются под размер экрана
    {
        gameBorderLeft.transform.position = new Vector3(ScreenUtils.ScreenLeft, 0, 0);
        gameBorderRight.transform.position = new Vector3(ScreenUtils.ScreenRight, 0, 0);
        gameBorderTop.transform.position = new Vector3(0, ScreenUtils.ScreenTop, 0);
        gameBorderBottom.transform.position = new Vector3(0, ScreenUtils.ScreenBottom, 0);
    }

    private void SearchWorldPointBorders()
    {
        BorderLeft = gameBorderLeft.transform.position.x + gameBorderLeft.GetComponent<Collider2D>().bounds.extents.x;
        BorderRight = gameBorderRight.transform.position.x - gameBorderRight.GetComponent<Collider2D>().bounds.extents.x;
        BorderTop = gameBorderTop.transform.position.y - gameBorderTop.GetComponent<Collider2D>().bounds.extents.y;
        BorderBottom = gameBorderBottom.transform.position.y + gameBorderBottom.GetComponent<Collider2D>().bounds.extents.y;
    }
    #endregion
}
