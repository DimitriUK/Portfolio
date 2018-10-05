using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class PlayerMobile : MonoBehaviour
{
    NavMeshAgent agent;
    Animator playerAnim;
    public GameObject model;
    public GameObject moveCanvas;
    public GameObject uiAligner;
    public GameObject followCube;
    public GameObject rotateCanvas;
    public GameObject arrowLastPos;

    public bool isRunning;
    public bool isWalking;
    public bool isSneaking;
    public bool isCrawling;

    public bool isHoldingLMB;
    public bool arrowSpawned;   

    public bool goingToArrow;
    public bool twoFingerTouch;

    public float holdingLMBTimer;

    //This script is a player controller script designed for mobile use.

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerAnim = model.GetComponent<Animator>();
        isSneaking = true;
    }
     
    void Update()
    {
        RaycastHit hit;
        Debug.Log(holdingLMBTimer);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            followCube.transform.position = hit.point;
        }

        if (Input.touchCount == 2)
        {
            twoFingerTouch = true;
        }
        else if (Input.touchCount == 0)
        {
            StartCoroutine(TwoFingerTimer());
        }

        if (!isHoldingLMB && !twoFingerTouch)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    Vector3 uiPos = hit.point;
                    uiPos.y = 0.02f;
                    agent.destination = hit.point;
                    GameObject moveUI = Instantiate(moveCanvas, uiPos, uiAligner.transform.rotation) as GameObject;
                    holdingLMBTimer = 0;
                }
            }
        }

        if (holdingLMBTimer > 0.1f)
        {
            isHoldingLMB = true;
        }
        else
        {
            isHoldingLMB = false;
        }

        if (isHoldingLMB && !twoFingerTouch)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Vector3 uiPos = hit.point;
                uiPos.y = 0.02f;
                if (!arrowSpawned)
                {
                    GameObject rotateUI = Instantiate(rotateCanvas, uiPos, uiAligner.transform.rotation) as GameObject;
                    arrowLastPos = rotateUI;
                    arrowSpawned = true;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    agent.destination = arrowLastPos.transform.position;
                    goingToArrow = true;
                }
            }
        }

        if (!twoFingerTouch && Input.GetMouseButton(0))
        {
            holdingLMBTimer += Time.deltaTime;
        }
        else if (!twoFingerTouch && !Input.GetMouseButton(0))
        {
            isHoldingLMB = false;
            holdingLMBTimer = 0;
            arrowSpawned = false;
        }

        if (agent.remainingDistance < 0.01f && goingToArrow == true)
        {
            var rot = arrowLastPos.transform.rotation;
            rot.x = 0;
            rot.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 10);
        }

        if (!agent.pathPending && goingToArrow == true)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    StartCoroutine(StopTurning());
                }

            }
        }

        if (agent.remainingDistance > 0)
        {
            if (isRunning)
            {
                playerAnim.SetBool("Running", true);
                playerAnim.SetBool("Walking", false);
                playerAnim.SetBool("Sneaking", false);
                playerAnim.SetBool("Crawling", false);
                agent.speed = 5;
            }
            else if (isWalking)
            {
                playerAnim.SetBool("Running", false);
                playerAnim.SetBool("Walking", true);
                playerAnim.SetBool("Sneaking", false);
                playerAnim.SetBool("Crawling", false);
                agent.speed = 2;
            }
            else if (isSneaking)
            {
                playerAnim.SetBool("Running", false);
                playerAnim.SetBool("Walking", false);
                playerAnim.SetBool("Sneaking", true);
                playerAnim.SetBool("Crawling", false);
                agent.speed = 1;
            }
            else if (isCrawling)
            {
                playerAnim.SetBool("Running", false);
                playerAnim.SetBool("Walking", false);
                playerAnim.SetBool("Sneaking", false);
                playerAnim.SetBool("Crawling", true);
                agent.speed = 0.5f;
            }
        }
        else
        {
            playerAnim.SetBool("Running", false);
            playerAnim.SetBool("Walking", false);
            playerAnim.SetBool("Sneaking", false);
            playerAnim.SetBool("Crawling", false);
        }
    }

    public IEnumerator StopTurning()
    {
        yield return new WaitForSeconds(1);
        goingToArrow = false;
        StopAllCoroutines();
    }

    public IEnumerator TwoFingerTimer()
    {
        yield return new WaitForSeconds(0.3f);
        twoFingerTouch = false;
    }
}
