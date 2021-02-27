using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    #region Fields

    private static EventManager instance;

    #endregion

    #region Properties

    public static EventManager Instance { get { return instance; } }

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
    }

    #endregion
}
