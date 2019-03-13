using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]

public class CharacterData : ScriptableObject
{
    public enum Factions
    {
        Civilian,
        Colonial,
        Mercenary,
        Criminal,
    }

    [Header("Character Info")]
    public string FirstName;
    public string LastName;
    public Factions Faction;
    public int ModelID;

    [Header("Character Assets")]
    public List<GameObject> Models;
}
