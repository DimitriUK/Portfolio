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

    public void SerializeData(string name, int gender, int skin, int hair, int hairColor, int beard, int beardColor)
    {
        CharacterData charData = new CharacterData(name, gender, skin, hair, hairColor, beard, beardColor);

        string jsonString = JsonConvert.SerializeObject(charData, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        string charDir = Application.persistentDataPath + "/SavedData";

        if (!Directory.Exists(charDir))
            Directory.CreateDirectory(charDir);

        string path = charDir + "/Character1" + ".json";
        Debug.Log("AssetPath:" + path);
        File.WriteAllText(path, jsonString);
        Debug.Log("Character created successfully!");
    }
}
