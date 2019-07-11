using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public bool isDeveloperMode;
    Animator anim;

    [Header("Choose your Name!")]
    public Text PlayerName;

    [Header("Choose your Gender!")]
    public int GenderID;
    public Text GenderName;

    [Header("Choose your Skin!")]
    public int SkinID;
    public Text SkinName;
    public Material PlayerMaterial;

    [Header("Choose your Hair!")]
    public int HairID;
    public Text HairName;
    public List<GameObject> HairTypes;

    [Header("Choose your Hair Color!")]
    public int HairColorID;
    public Text HairColorName;

    [Header("Choose your Beard!")]
    public int BeardID;
    public Text BeardName;
    public List<GameObject> BeardTypes;
    public Material BeardMaterial;

    [Header("Choose your Beard Color!")]
    public int BeardColorID;
    public Text BeardColorName;

    public GameObject MaleParts;
    public GameObject FemaleParts;
    public CreateCharacterData creationCharacterData;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ResetCharacter()
    {
        if (GenderID == 0)
        {
            MaleParts.SetActive(true);
        }
        else if (GenderID == 1)
        {
            FemaleParts.SetActive(true);
        }

        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Skin", creationCharacterData.SkinColor[0]);
        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Stubble", creationCharacterData.StubbleColor[0]);
        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Hair", creationCharacterData.HairColor[0]);
        creationCharacterData.PlayerMaterials[1].SetColor("_Color_Hair", creationCharacterData.BeardColor[0]);

        foreach (GameObject hair in HairTypes)
        {
            hair.SetActive(false);
        }
        if (HairID != 0)
            HairTypes[0].SetActive(true);

        foreach (GameObject beard in BeardTypes)
        {
            beard.SetActive(false);
        }
        if (BeardID != 0)
            BeardTypes[0].SetActive(true);
    }

    private void Start()
    {
        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Skin", creationCharacterData.SkinColor[0]);
        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Stubble", creationCharacterData.StubbleColor[0]);
        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Hair", creationCharacterData.HairColor[0]);
        creationCharacterData.PlayerMaterials[1].SetColor("_Color_Hair", creationCharacterData.BeardColor[0]);
    }

    public void Update()
    {
        if (isDeveloperMode)
        {
            ChangeGender();

            SkinName.text = "Skin Tone " + SkinID;

            if (HairID != 0)
                HairName.text = "Hair Style " + HairID;


            creationCharacterData.PlayerMaterials[0].SetColor("_Color_Skin", creationCharacterData.SkinColor[SkinID - 1]);
            creationCharacterData.PlayerMaterials[0].SetColor("_Color_Stubble", creationCharacterData.StubbleColor[SkinID - 1]);
            creationCharacterData.PlayerMaterials[0].SetColor("_Color_Hair", creationCharacterData.HairColor[HairColorID - 1]);
            creationCharacterData.PlayerMaterials[1].SetColor("_Color_Hair", creationCharacterData.BeardColor[BeardColorID - 1]);
        }
    }

    public void GenderIDCounter(bool add)
    {
        if (add)
            GenderID += 1;
        else
            GenderID -= 1;

        if (GenderID > 0 && GenderID < 2)
            GenderID = 1;
        else if (GenderID > 1)
            GenderID = 0;
        else if (GenderID < 0)
            GenderID = 1;

        if (GenderID == 0)
            GenderName.text = "Male";
        else GenderName.text = "Female";

        ChangeGender();
    }

    public void SkinIDCounter(bool add)
    {
        if (add)
            SkinID += 1;
        else
            SkinID -= 1;

        if (SkinID > 6)
            SkinID = 1;
        else if (SkinID < 1)
            SkinID = 6;

        SkinName.text = "Skin Tone " + SkinID;

        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Skin", creationCharacterData.SkinColor[SkinID - 1]);
        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Stubble", creationCharacterData.StubbleColor[SkinID - 1]);
    }

    public void HairIDCounter(bool add)
    {
        if (add)
            HairID += 1;
        else
            HairID -= 1;

        if (HairID > HairTypes.Count)
            HairID = 0;
        else if (HairID < 0)
            HairID = HairTypes.Count;

        HairName.text = "Hair Style " + HairID;

        HairChange();
    }

    public void HairChange()
    {
        foreach (GameObject hair in HairTypes)
        {
            hair.SetActive(false);
        }
        if (HairID != 0)
            HairTypes[HairID - 1].SetActive(true);

        if (HairID == 0)
            HairName.text = "No Hair";
    }

    public void HairColorIDCounter(bool add)
    {
        if (add)
            HairColorID += 1;
        else
            HairColorID -= 1;

        if (HairColorID > creationCharacterData.HairColor.Count)
            HairColorID = 1;
        else if (HairColorID < 1)
            HairColorID = creationCharacterData.HairColor.Count;

        HairColorName.text = "Hair Color " + HairColorID;

        HairColorChange();
    }

    public void BeardIDCounter(bool add)
    {
        if (add)
            BeardID += 1;
        else
            BeardID -= 1;

        if (BeardID > BeardTypes.Count)
            BeardID = 0;
        else if (BeardID < 0)
            BeardID = BeardTypes.Count;

        BeardName.text = "Beard Style " + BeardID;

        BeardChange();
    }

    public void BeardChange()
    {
        foreach (GameObject beard in BeardTypes)
        {
            beard.SetActive(false);
        }
        if (BeardID != 0)
            BeardTypes[BeardID - 1].SetActive(true);

        if (BeardID == 0)
            BeardName.text = "No Beard";
    }

    public void BeardHairColorChange()
    {
        creationCharacterData.PlayerMaterials[1].SetColor("_Color_Hair", creationCharacterData.BeardColor[BeardColorID - 1]);
    }

    public void BeardHairColorIDCounter(bool add)
    {
        if (add)
            BeardColorID += 1;
        else
            BeardColorID -= 1;

        if (BeardColorID > creationCharacterData.BeardColor.Count)
            BeardColorID = 1;
        else if (BeardColorID < 1)
            BeardColorID = creationCharacterData.BeardColor.Count;

        BeardColorName.text = "Beard Color " + BeardColorID;

        BeardHairColorChange();
    }

    public void HairColorChange()
    {
        creationCharacterData.PlayerMaterials[0].SetColor("_Color_Hair", creationCharacterData.HairColor[HairColorID - 1]);
    }

    public void ChangeGender()
    {
        if (GenderID == 0)
        {
            MaleParts.SetActive(true); FemaleParts.SetActive(false);
        }
        else
        {
            MaleParts.SetActive(false); FemaleParts.SetActive(true);
        }
    }

    public void TurnCharacter(bool i)
    {
        if (i)
            anim.SetBool("TurnLeft", true);
        else anim.SetBool("TurnRight", true);

    }

    public void StopTurning()
    {
        anim.SetBool("TurnLeft", false);
        anim.SetBool("TurnRight", false);
    }

    public void CharacterSave()
    {
        SaveCharacter.instance.SerializeData(PlayerName.text, GenderID, SkinID, HairID, HairColorID, BeardID, BeardColorID);

        StartCoroutine(LoadFirstLevel());
    }

    public IEnumerator LoadFirstLevel()
    {
        MainMenu.instance.BlackFader.CrossFadeAlpha(1, 3, true);
        yield return new WaitForSeconds(5);
        /// Load First Level
    }
}
