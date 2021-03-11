using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : Menu<GameMenu>
{
    public void OnPausePressed()
    {
        Time.timeScale = 0;

        PauseMenu.Open();
    }

    private void Update()
    {
        
    }
}
