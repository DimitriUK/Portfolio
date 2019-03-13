using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public CharacterData Character;

    public InputField FirstName;
    public InputField LastName;

    public Dropdown Faction;

    public Text CharacterIDText;
    public int CharacterID;

    public List<GameObject> CharactersSpawnList;

    public Vector3 SpawnLocation = new Vector3(396.7f, 138.5f, 572.4f);
    public GameObject RotFixer;

    void Start()
    {
        CharacterIDCounter(this.gameObject);
    }

    public void CharacterIDCounter(GameObject obj)
    {
        if (obj.name == "Left_Choice")
        {
            CharacterID -= 1;
        }
        if (obj.name == "Right_Choice")
        {
            CharacterID += 1;
        }

        if (CharacterID > 8)
            CharacterID = 1;
        else if (CharacterID < 1)
            CharacterID = 8;

        CharacterIDText.text = "Model " + CharacterID;

        Update3DModel();
    }

    public void Update3DModel()
    {
        foreach (GameObject character in CharactersSpawnList)
        {
            Destroy(character);
        }

        CharactersSpawnList.Clear();

        GameObject NewModel = Instantiate(Character.Models[CharacterID - 1]);
        NewModel.transform.position = SpawnLocation;
        NewModel.transform.rotation = RotFixer.transform.rotation;
        CharactersSpawnList.Add(NewModel);
    }
}
