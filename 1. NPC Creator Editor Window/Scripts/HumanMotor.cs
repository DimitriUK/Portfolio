using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanMotor : Human
{
    private Rigidbody rb;
    private NavMeshAgent agent;

    public void SetupNPC(Rigidbody rigidBody, NavMeshAgent npcAgent)
    {
        rb = rigidBody;
        agent = npcAgent;
        Debug.Log("NPC set up successful!");
    }
}
