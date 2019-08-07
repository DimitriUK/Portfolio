using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIDisplayer : MonoBehaviour
{
    private AI AI_ID;

    public new Text name;
    public Text desc;
    public Image faction;

    void Start()
    {
        AI_ID = transform.parent.GetComponent<AIType>().AI_ID;

        name.text = AI_ID.Name;
        desc.text = AI_ID.Desc;
        faction.sprite = AI_ID.Faction;
    }
}
