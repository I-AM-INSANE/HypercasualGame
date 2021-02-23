using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour
{
    /// <summary>
    /// Awake is called before Start
    /// </summary>
	private void Awake()
    {
        ScreenUtils.Initialize();
        //ConfigurationUtils.Initialize();
    }
}