using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private MainMenu mainMenuPrefab;
    [SerializeField] private SettingsMenu settingsMenuPrefab;
    [SerializeField] private PauseMenu pauseMenuPrefab;
    [SerializeField] private GameMenu gameMenuPrefab;
    [SerializeField] private GameOverMenu gameOverMenuPrefab;

    [SerializeField] private Transform menuParent;

    private Stack<Menu> menuStack = new Stack<Menu>();

    private static MenuManager instance;

    #endregion

    #region Properties

    public static MenuManager Instance { get { return instance; } }

    #endregion

    #region Methods

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            InitializeMenus();
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitializeMenus()
    {
        if (menuParent == null)
        {
            GameObject menuParentObject = new GameObject("Menus");
            menuParent = menuParentObject.transform;
        }

        Object.DontDestroyOnLoad(menuParent.gameObject);

        Menu[] menuPrefabs = { mainMenuPrefab, settingsMenuPrefab, pauseMenuPrefab, gameMenuPrefab, gameOverMenuPrefab };

        foreach (Menu menuPrefab in menuPrefabs)
        {
            if (menuPrefab != null)
            {
                Menu menuInstance = Instantiate(menuPrefab, menuParent);
                if (menuPrefab != mainMenuPrefab)
                    menuInstance.gameObject.SetActive(false);
                else
                    OpenMenu(menuInstance);
            }
        }
    }

    public void OpenMenu(Menu menuInstance)
    {
        if (menuStack.Count > 0)
        {
            foreach (Menu menu in menuStack)
            {
                menu.gameObject.SetActive(false);
            }
        }

        menuInstance.gameObject.SetActive(true);
        menuStack.Push(menuInstance);
    }

    public void CloseMenu()
    {
        Menu topMenu = menuStack.Pop();
        topMenu.gameObject.SetActive(false);

        if (menuStack.Count > 0)
        {
            Menu nextMenu = menuStack.Peek();
            nextMenu.gameObject.SetActive(true);
        }
    }
}

    #endregion
