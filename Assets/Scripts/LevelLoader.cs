using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    #region Fields

    private static int mainMenuIndex = 0;

    #endregion

    #region Methods
    public static void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (levelIndex == mainMenuIndex)
                MainMenu.Open();

            SceneManager.LoadScene(levelIndex);
        }
    }

    public static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel()
    {
        int nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % (SceneManager.sceneCountInBuildSettings);

        LoadLevel(nextLevelIndex);
    }

    public static void LoadMainMenuLevel()
    {
        LoadLevel(mainMenuIndex);
    }

    #endregion
}
