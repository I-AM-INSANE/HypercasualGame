using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : Abstract_ExploddingTrapps
{
    #region Methods

    private void Awake()
    {
        trapAnimName = "DropBombExplosion";
        explosionDelay = 2f;
    }

    #endregion
}
