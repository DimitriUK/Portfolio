using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EXPPlayer : MonoBehaviour
{
    [Header("Player System")]
    public NavMeshAgent agent;
    public EXPCombat expCombat;
    public EXPAnim expAnim;
    public Animator anim;

    public float timeAmount;

    public bool isHolding;
    public bool turnOn;
    public bool isInteraction;

    [Header("UI Elements")]
    public GameObject chanceUI;
    public Button[] Buttons;
    public Color orange;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (agent != null)
        {
            distance = Vector3.Distance(agent.transform.position, agent.GetComponent<EXPAnim>().moveMarker.transform.position);

            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(1) && !isHolding && hit.collider.tag != "Enemy")
                {
                    agent.GetComponent<EXPAnim>().moveMarker.transform.position = hit.point;
                    agent.GetComponent<EXPAnim>().moveHologram.SetActive(false);
                }

                if (Input.GetMouseButtonUp(1) && !isHolding && hit.collider.tag != "Enemy")
                {
                    if (!anim.GetBool("isRunning"))
                    {
                        anim.SetBool("isWalking", true);
                    }
                    if (anim.GetBool("isRunning"))
                    {
                        anim.SetBool("isWalking", true);
                    }
                    agent.GetComponent<EXPAnim>().moveMarker.transform.position = hit.point;
                    agent.GetComponent<EXPAnim>().moveHologram.SetActive(false);
                    agent.GetComponent<EXPAnim>().turnOn = false;
                    isInteraction = true;
                    agent.destination = agent.GetComponent<EXPAnim>().moveMarker.transform.position;
                    timeAmount = 0;
                }

                if (Input.GetMouseButtonUp(1) && !isHolding && hit.collider.tag == "Enemy")
                {
                    expCombat.FireWeapon();
                }

                if (hit.collider.tag == "Enemy")
                {
                    expCombat.cursor.sprite = expCombat.combatCursor;
                    Vector3 mp = Input.mousePosition;
                    mp.y += 50;
                    chanceUI.transform.position = mp;
                    chanceUI.GetComponent<CanvasGroup>().alpha = 1;

                    if (expCombat.isAdjusting)
                    {
                        chanceUI.transform.GetChild(5).GetComponent<Text>().text = "100% Chance of Hit";
                        chanceUI.transform.GetChild(5).GetComponent<Text>().color = Color.green;
                    }
                    else
                    {
                        chanceUI.transform.GetChild(6).GetComponent<Text>().text = "No Line of Sight";
                        chanceUI.transform.GetChild(6).GetComponent<Text>().color = Color.red;
                    }
                }
                else
                {
                    expCombat.cursor.sprite = expCombat.defCursor;
                    chanceUI.GetComponent<CanvasGroup>().alpha = 0;
                }

                if (Input.GetMouseButton(1) && hit.collider.tag != "Enemy")
                {
                    timeAmount += 0.1f;

                    if (timeAmount > 3.0f)
                    {
                        isHolding = true;
                    }
                }

                if (isHolding)
                {
                    agent.GetComponent<EXPAnim>().rotMarker.transform.position = hit.point;
                    agent.GetComponent<EXPAnim>().moveHologram.SetActive(true);

                    agent.GetComponent<EXPAnim>().moveMarker.transform.LookAt(agent.GetComponent<EXPAnim>().rotMarker.transform);

                    if (Input.GetMouseButtonUp(1))
                    {
                        if (!anim.GetBool("isRunning"))
                        {
                            anim.SetBool("isWalking", true);
                        }
                        if (anim.GetBool("isRunning"))
                        {
                            anim.SetBool("isWalking", true);
                        }
                        agent.speed = 1;
                        agent.destination = agent.GetComponent<EXPAnim>().moveMarker.transform.position;
                        isInteraction = true;
                        agent.GetComponent<EXPAnim>().moveMarker.transform.LookAt(agent.GetComponent<EXPAnim>().rotMarker.transform);

                        timeAmount = 0;
                        agent.GetComponent<EXPAnim>().turnOn = true;
                        isHolding = false;
                    }
                }
            }
        }
    }

    public void CombatMode()
    {
       expAnim.ToggleCombatMode();
    }
    public void TacticalMode()
    {
        expAnim.Tactical();
    }
    public void RunningMode()
    {
        expAnim.Running();
    }
    public void CrouchingMode()
    {
        expAnim.Crouching();
    }
    public void ProneMode()
    {
        expAnim.Proning();
    }
}
