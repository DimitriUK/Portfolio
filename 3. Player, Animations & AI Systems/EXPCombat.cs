using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RootMotion.FinalIK;

public class EXPCombat : MonoBehaviour
{

    public Sprite combatCursor;
    public Sprite defCursor;
    public Image cursor;

    public AimIK aimIK;

    public GameObject targetPoint;
    public GameObject currentTarget;

    public Camera gunCam;
    public Transform AIM_TRANSFORM;

    public LayerMask obstacleMask;

    public Animator anim;
    public AudioSource gunAS;
    public AudioClip m4One;

    public EXPAnim expAnim;

    [Header("Combat System")]
    public float engageTime = 0.7f;
    public float adjuster;
    public bool isDefensive;
    public bool isAdjusting;
    public bool isEngaging;
    public GameObject bullet;
    public GameObject barrel;
    public GameObject muzzleFlash;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        FindClosestEnemy();

        Vector3 dirToTarget = (currentTarget.transform.position - AIM_TRANSFORM.position).normalized;

        float distToTarget = Vector3.Distance(AIM_TRANSFORM.position, currentTarget.transform.position);

        if (distToTarget < 50 && expAnim.isCombat && isDefensive)
        {
            if (!Physics.Raycast(AIM_TRANSFORM.position, dirToTarget, distToTarget, obstacleMask))
            {
                isAdjusting = true;
                targetPoint.transform.position = Vector3.Lerp(targetPoint.transform.position, currentTarget.transform.GetChild(0).transform.position, Time.deltaTime * 2);
                if (!isEngaging)
                {
                    StartCoroutine(ShootRifle());
                }
                isEngaging = true;
            }
            else
            {
                isAdjusting = false;
                isEngaging = false;
                StopAllCoroutines();
            }
        }
        else
        {
            isAdjusting = false;
            isEngaging = false;
            StopAllCoroutines();
        }
    }

    public void Update()
    {
        aimIK.solver.IKPositionWeight = adjuster;
        Cursor.visible = false;
        cursor.transform.position = Input.mousePosition;

        if (isAdjusting)
        {
            adjuster += 0.03f;
        }
        else
        {
            adjuster -= 0.04f;
        }

        if (adjuster < 0)
        {
            adjuster = 0;
        }
        else if (adjuster > 1)
        {
            adjuster = 1;
        }

        if (currentTarget != null)
        {
            if (currentTarget.GetComponent<Enemy>().health <= 0)
            {
                currentTarget = null;
            }
        }
    }

    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
                currentTarget = closestEnemy.gameObject;
            }
        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position); //Show nearest enemies, keeping enabled for testing purposes (Disable when complete)
    }

    public void FireWeapon()
    {
        StopAllCoroutines();
        StartCoroutine(ShootRifle());
    }

    public IEnumerator ShootRifle()
    {
        yield return new WaitForSeconds(engageTime);

        {
            if (currentTarget != null)
            {
                anim.SetTrigger("ShootOnce");
                GameObject rifleBullet = Instantiate(bullet, barrel.transform.position, Quaternion.identity) as GameObject;
                rifleBullet.transform.parent = barrel.transform;
                rifleBullet.transform.position = barrel.transform.position;
                rifleBullet.transform.rotation = barrel.transform.rotation;
                rifleBullet.transform.parent = null;
                GameObject gunFlash = Instantiate(muzzleFlash, barrel.transform.position, barrel.transform.rotation) as GameObject;
                gunAS.PlayOneShot(m4One);
                StartCoroutine(ShootRifle());
                //anim.SetLayerWeight(2, 0);
            }
            if (currentTarget == null)
            {
                isEngaging = false;
                isAdjusting = false;
            }
        }
    }

    public void SwitchDefenseMode()
    {
        isDefensive = !isDefensive;
    }
}
