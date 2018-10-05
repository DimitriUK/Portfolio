using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EXPAnim : MonoBehaviour
{

    [Header("Core Systems")]
    public EXPPlayer EXPPlayerController;
    private Animator anim;
    private Animator holoAnim;
    public Animator soldierAnim;
    public Animator soldierHoloAnim;
    private NavMeshAgent agent;

    [Header("Immersion/Navgation System")]
    public GameObject moveMarker;
    public GameObject rotMarker;
    public GameObject moveHologram;
    public Transform target;

    [Header("Stance Systems")]
    public bool isAtEase;
    public bool isTactical;
    public bool isRunning;
    public bool isCrouching;
    public bool isProned;
    public bool isCombat;
    public bool inCover;
    public bool turnOn;

    [Header("UI Elements")]
    public Image[] Activators;
    public Text[] SubNames;
    public Sprite atEaseGuy;
    public Sprite combatLogo;
    public Image atEaseButton;
    public Text atEaseText;      

    [Header("Blend System")]
    public float animWeight;
    public bool animWeightCheck;

    [Header("Identification Colours")]
    private Color white = Color.white;
    public Color activeColour;
    public Color defaultColour;
   
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        holoAnim = GameObject.Find("_MOVE_MARKER_01/Soldier01MOVE").GetComponent<Animator>();
        moveHologram.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance;
        distance = Vector3.Distance(agent.transform.position, moveMarker.transform.position);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PRONE_TO_IDLE") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("PRONE_TO_CROUCH") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("CROUCH_TO_IDLE") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("CROUCH_TO_PRONE") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("IDLE_TO_CROUCH") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("IDLE_TO_PRONE"))
        {
            for (int i = 0; i < 5; i++)
            {
                Activators[i].GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            if (isCombat)
            {
                for (int i = 0; i < 3; i++)
                {
                    Activators[i].GetComponent<Button>().interactable = true;
                }

                if (isProned)
                {
                    Activators[4].GetComponent<Button>().interactable = false;
                }
                else
                {
                    Activators[4].GetComponent<Button>().interactable = true;
                }


                Activators[5].GetComponent<Button>().interactable = true;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Activators[i].GetComponent<Button>().interactable = false;
                }
                Activators[4].GetComponent<Button>().interactable = true;
                Activators[5].GetComponent<Button>().interactable = false;
            }
        }
        if (!isCombat)
        {
            Activators[4].color = activeColour;

            for (int i = 0; i < 3; i++)
            {
                Activators[i].color = defaultColour;
            }

            atEaseButton.sprite = atEaseGuy;
            atEaseText.text = "AT EASE";
        }
        else
        {
            Activators[4].color = activeColour;
            atEaseButton.sprite = combatLogo;
            atEaseText.text = "COMBAT";

            if (isTactical)
            {
                Activators[3].color = activeColour;

                for (int i = 0; i < 2; i++)
                {
                    Activators[i].color = defaultColour;
                }


                holoAnim.SetBool("isStanding", true);
                holoAnim.SetBool("isCrouching", false);
                holoAnim.SetBool("isProned", false);
            }
            if (isRunning)
            {
                Activators[0].color = defaultColour;
                Activators[1].color = defaultColour;
                Activators[2].color = activeColour;
                Activators[3].color = defaultColour;
                holoAnim.SetBool("isStanding", true);
                holoAnim.SetBool("isCrouching", false);
                holoAnim.SetBool("isProned", false);
            }
            if (isCrouching)
            {
                Activators[0].color = defaultColour;
                Activators[1].color = activeColour;
                Activators[2].color = defaultColour;
                Activators[3].color = defaultColour;
                holoAnim.SetBool("isStanding", false);
                holoAnim.SetBool("isCrouching", true);
                holoAnim.SetBool("isProned", false);
            }
            if (isProned)
            {
                Activators[0].color = activeColour;

                for (int i = 1; i < 3; i++)
                {
                    Activators[i].color = defaultColour;
                }

                holoAnim.SetBool("isStanding", false);
                holoAnim.SetBool("isCrouching", false);
                holoAnim.SetBool("isProned", true);
            }
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            if (!anim.GetBool("isRunning"))
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", true);
            }
        }
        if (!agent.hasPath && agent.remainingDistance < 1)
        {
            anim.SetBool("isWalking", false);
        }

        if (!isCombat)
        {
            if (animWeightCheck == false)
            {
                animWeight += 0.005f;
                anim.SetLayerWeight(1, animWeight);
            }

            if (animWeight > 0.95)
            {
                animWeightCheck = true;
            }
        }
        else if (isCombat)
        {
            if (animWeightCheck == false)
            {
                if (!isRunning)
                {
                    animWeight -= 0.005f;
                    anim.SetLayerWeight(1, animWeight);
                }
            }

            if (animWeight < 0)
            {
                animWeight = 0;
                animWeightCheck = true;
            }
        }

        if (!turnOn)
        {
            anim.SetBool("isTurningLeft", false);
            anim.SetBool("isTurningRight", false);
        }

        if (distance < 1)
        {
            moveHologram.SetActive(false);
        }

        if (turnOn && distance < 1)
        {
            if (agent.velocity.magnitude < 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, moveMarker.transform.rotation, Time.deltaTime * 3);
                moveHologram.SetActive(false);
                agent.updateRotation = true;
            }

            Vector3 twoAngs = rotMarker.transform.position - transform.position;
            float rotLeft = Vector3.Angle(twoAngs, transform.forward);
        }
    }

    private void OnAnimatorMove()
    {
        agent.speed = (anim.deltaPosition / Time.deltaTime).magnitude;
    }

    public void ToggleCombatMode()
    {
        isCombat = !isCombat;
        animWeightCheck = false;

        if (!isCombat)
        {
            StopAllCoroutines();
            anim.SetBool("isCombat", false);
            anim.SetBool("isCrouching", false);
            anim.SetBool("isProning", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isTactical", false);
            isTactical = false;
            isRunning = false;
            isCrouching = false;
            isProned = false;
            isCombat = false;
        }
        else
        {
            anim.SetBool("isCombat", true);
            isTactical = true;
            isCombat = true;
        }
    }

    public void Tactical()
    {
        anim.SetBool("isCrouching", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isTactical", false);
        anim.SetBool("isProning", false);
        isTactical = true;
        isRunning = false;
        isCrouching = false;
        isProned = false;
    }
    public void Running()
    {
        anim.SetBool("isRunning", true);
        anim.SetBool("isCrouching", false);
        anim.SetBool("isTactical", false);
        anim.SetBool("isProning", false);
        isTactical = false;
        isRunning = true;
        isCrouching = false;
        isProned = false;
    }
    public void Crouching()
    {
        anim.SetBool("isCrouching", true);
        anim.SetBool("isTactical", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isProning", false);
        isTactical = false;
        isRunning = false;
        isCrouching = true;
        isProned = false;
    }
    public void Proning()
    {
        anim.SetBool("isProning", true);
        anim.SetBool("isTactical", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isCrouching", false);
        isTactical = false;
        isRunning = false;
        isCrouching = false;
        isProned = true;
    }
}
