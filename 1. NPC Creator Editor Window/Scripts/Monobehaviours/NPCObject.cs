using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

[Serializable]
public class NPCObject : MonoBehaviour
{
    [Header("Assign Object")]
    public NPC NPCType;

    [Header("Special Properties")]
    public bool IsInvunerable;

    private Rigidbody rb;
    private NavMeshAgent agent;

    private HumanMotor humanMotor;

    public void Start()
    {
        if (NPCType == null)
            Debug.LogError("No Scriptable Object has been asigned.");

        humanMotor = new HumanMotor();
        SetupNPC();
    }

    public void Update()
    {

    }

    public virtual void ApplyNPCType()
    {
        Debug.Log("Testasdta");
    }

    public void SetupNPC()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
    }
}

public class Human : NPCObject
{
    public override void ApplyNPCType()
    {
        base.ApplyNPCType();

        Debug.Log("zzzTestasdta");
    }
}
