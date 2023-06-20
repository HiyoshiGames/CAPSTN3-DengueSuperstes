using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stat", menuName = "Scriptable Objects/EnemyStat")]
public class SO_EnemyStat : SO_Stat
{
    [Header(DS_Constants.RESISTANCES)]
    public float katolSlowPercent;
    public float insecticideDefPercent;
    public float saltGunDefPercent;
}
