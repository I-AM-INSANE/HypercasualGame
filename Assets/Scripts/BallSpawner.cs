﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject prefabBall;

    private GameObject createdBall; // Созданный мяч
    IInteractable interactable;
    private bool timerWasLaunched = false;

    Timer spawnTimer;

    #endregion

    #region Methods

    private void Awake()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = 0.5f;
        SpawnBall();
    }

    private void Update()
    {
        if (createdBall)
            interactable = createdBall.GetComponent<IInteractable>();

        if (!interactable.IsInteractable)
        {
            if (!timerWasLaunched)
            {
                spawnTimer.Run();
                timerWasLaunched = true;
            }
        }
        if (spawnTimer.Finished && timerWasLaunched)
            SpawnBall();
    }

    private void SpawnBall()
    {
        createdBall = Instantiate(prefabBall);
        timerWasLaunched = false;
    }

    #endregion

}