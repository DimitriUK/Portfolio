using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Character")]

public class CreateCharacterData : ScriptableObject
{
    public List<Material> PlayerMaterials;
    public List<Color> SkinColor, StubbleColor, HairColor, BeardColor;
}
