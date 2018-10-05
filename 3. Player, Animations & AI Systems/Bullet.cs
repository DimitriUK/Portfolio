using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

public class Bullet : MonoBehaviour {

    public Rigidbody rb;
    public float power;
    public GameObject deathUnit;

	// Use this for initialization
	void Start ()
    {
        rb.AddForce(transform.forward * power);
	}	

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ragdoll" || collision.gameObject.tag == "Enemy")
        {
            collision.transform.root.transform.GetChild(2).GetComponent<Enemy>().health -= Random.Range(40, 90); // Body Shot

            if (collision.transform.root.transform.GetChild(2).GetComponent<Enemy>().health <= 0)
            {
                collision.transform.root.transform.GetChild(1).GetComponent<PuppetMaster>().pinWeight = 0;
                StartCoroutine(HandleDeath());               
            }
            deathUnit = collision.gameObject;
            GetComponent<CapsuleCollider>().enabled = false;          
        }
    }

    public IEnumerator HandleDeath()
    {       
        yield return new WaitForSeconds(1);
        deathUnit.transform.root.transform.GetChild(1).GetComponent<PuppetMaster>().state = PuppetMaster.State.Dead;
        gameObject.SetActive(false);
    }
}
