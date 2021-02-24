using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Fields

    private Text ballsText;
    private const string BallsPrefix = "Balls: ";

    #endregion

    #region Methods

    private void Awake()
    {
        ballsText = gameObject.GetComponentInChildren<Text>();
        ballsText.text = BallsPrefix + GameManager.Instance.ProjectileNumber.ToString();
    }

    private void Update()
    {
        ballsText.text = BallsPrefix + GameManager.Instance.ProjectileNumber.ToString();
    }

    #endregion
}
