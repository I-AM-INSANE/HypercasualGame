using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBordersInitializer : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject gameBorderLeft;
    [SerializeField] private GameObject gameBorderRight;
    [SerializeField] private GameObject gameBorderTop;
    [SerializeField] private GameObject gameBorderBottom;

    #endregion

    #region Methods

    private void Awake()
    {
        GameBorderToScreenSize();
    }
    private void GameBorderToScreenSize()   // Границы подстраиваются под размер экрана
    {
        float colliderSize;
        colliderSize = gameBorderLeft.GetComponent<SpriteRenderer>().bounds.extents.x;
        gameBorderLeft.transform.position = new Vector3(ScreenUtils.ScreenLeft, 0, 0);
        colliderSize = gameBorderRight.GetComponent<SpriteRenderer>().bounds.extents.x;
        gameBorderRight.transform.position = new Vector3(ScreenUtils.ScreenRight, 0, 0);
        colliderSize = gameBorderTop.GetComponent<SpriteRenderer>().bounds.extents.y;
        gameBorderTop.transform.position = new Vector3(0, ScreenUtils.ScreenTop, 0);
        colliderSize = gameBorderBottom.GetComponent<SpriteRenderer>().bounds.extents.y;
        gameBorderBottom.transform.position = new Vector3(0, ScreenUtils.ScreenBottom, 0);
    }

    #endregion
}
