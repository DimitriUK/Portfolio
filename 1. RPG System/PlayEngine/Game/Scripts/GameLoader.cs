using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public bool isNewCharacter;

    [Header("Data Containers")]
    public CharacterData characterData; // Scriptable Object
    public SoldierData playerData;

    [Header("Soldier Attributes")]
    public Text SoldierName;
    public int RaceID;
    public int ModelID;

    public void Start()
    {
        string path = Application.persistentDataPath + "/SavedData.json";
        string test = File.ReadAllText(path);
        SoldierData soldierData = JsonUtility.FromJson<SoldierData>(test);
        playerData = soldierData;

        DeserializePlayerData(playerData);
    }



    public void DeserializePlayerData(SoldierData data)
    {
        SoldierName.text = data.FirstName + " " + data.LastName;
        RaceID = data.RaceID;
        ModelID = data.ModelID;

        GameObject playerSpawned = Instantiate(PlayerPrefab, transform.position, transform.rotation);

        if (isNewCharacter)
            playerSpawned.transform.position = new Vector3(0, 0.5f, 0); 

        else playerSpawned.transform.position = new Vector3(0, 0, 0); // Change to Last Saved Coordinates in new system to be created.





        if (RaceID == 1)
            Instantiate(characterData.HumanModels[ModelID - 1], playerSpawned.transform);

        else if (RaceID == 2)
            Instantiate(characterData.MechModels[ModelID - 1], playerSpawned.transform);
    }
}
