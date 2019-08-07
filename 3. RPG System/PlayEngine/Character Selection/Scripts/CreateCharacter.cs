using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    [Header("Assign Character")]
    public CharacterData Character;

    [Header("Name Components")]
    public InputField FirstName;
    public InputField LastName;

    [Header("Race Components")]
    public Text RaceIDText;
    public int RaceID;

    [Header("Character Components")]
    public Text CharacterIDText;
    public int CharacterID;

    [Header("Spawn List Object")]
    public List<GameObject> CharactersSpawnList;

    [Header("Spawn Position")]
    public Vector3 SpawnLocation = new Vector3(396.7f, 138.5f, 572.4f);

    public GameObject RotFixer; // To be changed to Quaternion eventually.

    private GameObject CurrentModel;

    public static CreateCharacter instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        RaceIDCounter(this.gameObject);
        CharacterIDCounter(this.gameObject);
    }

    public void CharacterSave()
    {
        string firstName = FirstName.text;
        string lastName = LastName.text;

        SaveCharacter.instance.SerializeData(firstName, lastName, RaceID, CharacterID);
    }

    public void RaceIDCounter(GameObject obj)
    {
        if (obj.name == "Left_Choice")
        {
            RaceID -= 1;
        }
        if (obj.name == "Right_Choice")
        {
            RaceID += 1;
        }

        if (RaceID > 2)
            RaceID = 1;
        else if (RaceID < 1)
            RaceID = 2;


        if (RaceID == 1)
            RaceIDText.text = "Human";
        else
            RaceIDText.text = "Mech";

        Update3DModel();
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

        if (CharacterID > Character.HumanModels.Count)
            CharacterID = 1;
        else if (CharacterID < 1)
            CharacterID = 4;

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

        if (RaceID == 1)
        {
            GameObject NewModel = Instantiate(Character.HumanModels[CharacterID - 1]);
            CurrentModel = NewModel;
        }
        else
        {
            GameObject NewModel = Instantiate(Character.MechModels[CharacterID - 1]);
            CurrentModel = NewModel;
        }

        CurrentModel.transform.position = SpawnLocation;
        CurrentModel.transform.rotation = RotFixer.transform.rotation;
        CharactersSpawnList.Add(CurrentModel);
    }
}
