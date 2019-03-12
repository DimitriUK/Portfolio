using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIDisplayer : MonoBehaviour
{
    public AI ai;

    public new Text name;
    public Text desc;

    public Image faction;

    // Start is called before the first frame update
    void Start()
    {
        name.text = ai.name;
        desc.text = ai.desc;
        faction.sprite = ai.faction;
    }
}
