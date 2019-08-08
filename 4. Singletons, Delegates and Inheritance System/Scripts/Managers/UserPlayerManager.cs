using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayerManager : MonoBehaviour
{
    public static UserPlayerManager instance;
    
    public int exp;
    
    public void Awake()
    {
        instance = this;
    }

    public void IncreaseExperience(NPCObject npc)
    {
        int minExp = npc.NPCType.NPC_ExpMin;
        int maxExp = npc.NPCType.NPC_ExpMax;
        int expGiven = Random.Range(minExp, maxExp);

        exp += expGiven;

        UIManager.instance.IncreaseExperienceUIWindow(exp);
    }
}
