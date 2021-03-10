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

    public int KillStreak { get; set; }

    #endregion

    #region Methods

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        KillStreak = 0;

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        if (!lose)
        {
            if (projectileNumber <= 0 && ballsOnScreen <= 0)
            {
                Time.timeScale = 0;
                LoseMenu.Open();
                lose = true;
            }
        }
            
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        KillStreak = 0;
        projectileNumber = 10;
        ballsOnScreen = 0;
        lose = false;
    }

    #endregion
}
