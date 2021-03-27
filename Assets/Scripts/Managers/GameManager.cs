using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    private static GameManager instance;

    private int projectileNumber = 10;
    private int ballsOnScreen = 0;
    private int score = 0;
    private int multiplier = 1;

    private bool lose = false;

    #endregion

    #region Properties

    public static GameManager Instance { get { return instance; } }

    public int BallsOnScreen
    {
        get { return ballsOnScreen; }
        set { ballsOnScreen = value; }
    }

    public int ProjectileNumber
    {
        get { return projectileNumber; }
        set { projectileNumber = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public int KillStreak { get; set; }

    public int Multiplier => multiplier;

    #endregion

    #region Methods

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        if (projectileNumber <= 0)
        {
            if (!lose)
            {
                projectileNumber = 0;
                Time.timeScale = 0;
                GameOverMenu.Open();
                lose = true;
            }
        }           
    }

    public void ChangeMultiplier()
    {
        if (KillStreak >= 0 && KillStreak <= 3)
            multiplier = 1;
        if (KillStreak > 2 && KillStreak <= 6)
            multiplier = 2;
        if (KillStreak > 5 && KillStreak <= 10)
            multiplier = 3;
        if (KillStreak > 10)
            multiplier = 4;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        KillStreak = 0;
        multiplier = 1;
        projectileNumber = 10;
        ballsOnScreen = 0;
        score = 0;
        lose = false;
    }

    #endregion
}
