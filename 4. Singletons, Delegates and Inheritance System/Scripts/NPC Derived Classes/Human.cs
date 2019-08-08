using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : NPCObject
{
    public override void SetupNPC()
    {
        Debug.Log("Human Created");
        base.NpcDeath += ConfirmKill;
    }

    public void ConfirmKill()
    {
        Debug.Log("Human Died");
    }
}