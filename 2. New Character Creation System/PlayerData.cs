using System;

[Serializable]
public class CharacterData
{
    public string FirstName;
    public int GenderID, SkinID, HairID, HairColorID, BeardID, BeardColorID;

    public CharacterData(string name, int genderID, int skinID, int hairID, int hairColorID, int beardID, int beardColorID)
    {
        FirstName = name;
        GenderID = genderID;
        SkinID = skinID;
        HairID = hairID;
        HairColorID = hairColorID;
        BeardID = beardID;
        BeardColorID = beardColorID;
    }
}
