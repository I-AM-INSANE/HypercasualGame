using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : Menu<LoseMenu>
{
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
