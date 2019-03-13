using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class AI : ScriptableObject
{
    [Header("NPC Info")]
    public string Name;
    public string Desc;
    public Sprite Faction;

    [Header("NPC Stats")]
    public int Health;
    public int Stamina;
    public int Level;

    [Header("NPC Type")]
    public int Race; 
    public int FearLevel;

    [Header("NPC Relations")]
    public List<AI> Allied;
    public List<AI> Hostile;

    [Header("NPC Unlockables")]
    public string[] LearnedDesc;

    [Header("NPC Audio Systems")]
    public List<AudioClip> IdleClips;
    public List<AudioClip> TalkClips;
    public List<AudioClip> ShoutClips;
    public List<AudioClip> CombatClips;

    [Header("NPC Audio Footsteps")]
    public List<AudioClip> WalkStone;
    public List<AudioClip> WalkWood;
    public List<AudioClip> WalkMetal;

    public List<AudioClip> RunStone;
    public List<AudioClip> RunWood;
    public List<AudioClip> RunMetal;

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
