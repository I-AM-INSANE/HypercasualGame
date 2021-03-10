using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu<T> : Menu where T : Menu<T>
{
    private static T instance;

    public static T Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    public static void Open()
    {
        if (Instance != null)
            MenuManager.Instance.OpenMenu(Instance);
    }
}

public abstract class Menu : MonoBehaviour
{
    public virtual void OnBackPressed()
    {
        if (MenuManager.Instance != null)
            MenuManager.Instance.CloseMenu();
    }
}
