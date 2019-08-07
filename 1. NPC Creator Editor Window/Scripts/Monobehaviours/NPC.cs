using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC/New NPC")]
public class NPC : ScriptableObject
{
    [Header("NPC Icon")]
    public Sprite NPC_Profile;

    [Header("NPC Info")]
    public NPCTypes NPC_Type;
    public enum NPCTypes { Human = 1, Monster, Alien, Demon }
    public string NPC_Name;

    public int NPC_Level;
    public int NPC_ExpMin, NPC_ExpMax;
    public int NPC_StartHealth, NPCHealth;
    public int NPC_AttackDmgMin, NPC_AttackDmgMax;
    public int NPC_StopDistance;
    public int NPC_ActID;

    [Header("NPC Stats")]
    public HostileType NPC_Fear;
    public enum HostileType { Coward = 1, Average, Normal, Brave, Fearless }

    public void Test()
    {
        Debug.Log("Test");
    }
}
