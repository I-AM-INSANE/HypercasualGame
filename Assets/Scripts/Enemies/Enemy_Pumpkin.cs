using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pumpkin : Abstract_Enemy
{
    protected override void Awake()
    {
        element = Enum_Elements.Water;
        base.Awake();
    }
}
