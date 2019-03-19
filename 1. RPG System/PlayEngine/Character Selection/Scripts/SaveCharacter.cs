using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveCharacter : MonoBehaviour
{
    public static SaveCharacter instance;

    private void Awake()
    {
        instance = this;
    }

    public void SerializeData(string firstName, string lastName, int race, int model)
    {
        PlayerData playerData = new PlayerData(firstName, lastName, race, model);

        string jsonString = JsonConvert.SerializeObject(playerData, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        string charDir = Application.persistentDataPath + "/SavedData";

        if (!Directory.Exists(charDir))
            Directory.CreateDirectory(charDir);

        string path = charDir + ".json";
        Debug.Log("AssetPath:" + path);
        File.WriteAllText(path, jsonString);
        Debug.Log("Character created successfully!");
    }

}
