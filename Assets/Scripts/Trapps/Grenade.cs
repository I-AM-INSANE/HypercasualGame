using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Abstract_ExploddingTrapps
{
    #region Methods

    private void Awake()
    {
        trapAnimName = "GrenadeExplosion";
        explosionDelay = 1f;
    }

    #endregion
}
