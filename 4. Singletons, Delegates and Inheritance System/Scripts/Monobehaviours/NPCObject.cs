using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class NPCObject : MonoBehaviour
{
    [Header("Assign Object")]
    public NPC NPCType;

    [Header("Special Properties")]
    public bool IsInvunerable;

    private Rigidbody rb;
    private NavMeshAgent agent;

    public delegate void DeathHandler();
    public event DeathHandler NpcDeath;

    public void Start()
    {
        if (NPCType == null)
        {
            Debug.LogError("No Scriptable Object has been asigned. NPC could not be initialized");
            return;
        }

        SetupNPC();

        NpcDeath += OnDeath;
        NpcDeath += UIManager.instance.OnNPCDeath;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            NpcDeath();
    }

    public void OnDeath()
    {
        Debug.Log("I have been killed");

        UserPlayerManager.instance.IncreaseExperience(this);
    }

    public virtual void SetupNPC()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        switch (NPCType.NPC_Type)
        {
            case NPC.NPCTypes.Human:
                Human human = new Human();
                human.SetupNPC();
                break;

            case NPC.NPCTypes.Monster:
                Monster monster = new Monster();
                monster.SetupNPC();
                break;
        }
    }
}
