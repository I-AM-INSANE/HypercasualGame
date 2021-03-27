using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Abstract_Enemy
{
    protected override void Awake()
    {
        element = Enum_Elements.Water;
        dyingAnimation = "FireBurst";
        base.Awake();
    }
}
