using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region Fields

    private Text ballsText;
    private Text scoreText;
    private const string ScorePrefix = "Score: ";
    private Text multiplierText;
    private const string MultiplierPrefix = "X";

    #endregion

    #region Methods

    private void Awake()
    {
        ballsText = transform.Find("BallsImage").transform.Find("BallsText").GetComponent<Text>();
        ballsText.text = GameManager.Instance.ProjectileNumber.ToString();

        scoreText = transform.Find("ScoreText").GetComponent<Text>();
        scoreText.text = ScorePrefix + 5;

        multiplierText = transform.Find("MultiplierText").GetComponent<Text>();
        multiplierText.text = MultiplierPrefix + GameManager.Instance.KillStreak.ToString();
    }

    private void Update()
    {
        if (GameManager.Instance.ProjectileNumber >= 0)
            ballsText.text = GameManager.Instance.ProjectileNumber.ToString();

        scoreText.text = ScorePrefix + GameManager.Instance.Score.ToString();
        multiplierText.text = MultiplierPrefix + GameManager.Instance.Multiplier.ToString();
    }

    #endregion
}
