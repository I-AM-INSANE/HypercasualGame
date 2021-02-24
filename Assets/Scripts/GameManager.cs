using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

    private static GameManager instance;

    private int projectileNumber = 10;

    #endregion

    #region Properties

    public static GameManager Instance { get { return instance; } }

    public int ProjectileNumber
    {
        get { return projectileNumber; }
        set { projectileNumber = value; }
    }

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
