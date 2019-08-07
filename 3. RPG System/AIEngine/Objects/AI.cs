using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class AI : ScriptableObject
{
    [Header("NPC Info")]
    public new string name;
    public string desc;
    public Sprite faction;

    [Header("NPC Stats")]
    public int health;
    public int stamina;
    public int level;

    [Header("NPC Type")]

    public int race; 
    public int fearLevel;


    [Header("NPC Unlockables")]
    public string[] learnedDesc;

    /// <summary>
    /// RACES:
    /// 0: HUMAN
    /// 1: DWARF
    /// 2: UNDEAD
    /// 3: ELVEN
    /// 4: INFECTED HUMAN
    /// 
    /// FEAR LEVELS:
    /// 0 = MOST BRAVE
    /// 5 = MOST SCARED
    /// </summary>
}
