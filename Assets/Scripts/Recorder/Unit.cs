using System;
using UnityEngine;

public class Unit : ReplayUnit
{
    protected override void Awake()
    {
        base.Awake();
        RecordManager.Instance.Rigister(this);
    }
}