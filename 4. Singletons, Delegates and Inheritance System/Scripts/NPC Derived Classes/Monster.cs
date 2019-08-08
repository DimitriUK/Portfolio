using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : NPCObject
{
    public override void SetupNPC()
    {
        Debug.Log("Monster Created");
        base.NpcDeath += ConfirmKill;
    }

    public void ConfirmKill()
    {
        Debug.Log("Monster Died");
    }
}