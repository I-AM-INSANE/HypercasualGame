using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : Menu<GameOverMenu>
{
    private Text scoreText;
    private const string ScorePrefix = "SCORE: ";

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            scoreText = transform.Find("Body").transform.Find("ScoreText").GetComponent<Text>();
            scoreText.text = ScorePrefix + GameManager.Instance.Score.ToString();
        }
    }
    public void OnRestartPressed()
    {
        Time.timeScale = 1;
        LevelLoader.ReloadLevel();
        base.OnBackPressed();
    }

    public void OnMainMenuPressed()
    {
        Time.timeScale = 1;
        LevelLoader.LoadMainMenuLevel();
    }
}
