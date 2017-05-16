using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyHealth : MonoBehaviour {

    public Image healthImage;
	public float fullHealth;
	float currentHealth;
	UnityEngine.AI.NavMeshAgent myNavAgent;
	Animator myAnim;
	Rigidbody myRB;

	public bool deathBool = false;

	public AudioClip LoseFX;
	public AudioClip enemyDamageFX;

	private void Start () {        
        currentHealth = fullHealth;
        healthImage.fillAmount = (float)currentHealth / (float)fullHealth;
        myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		myRB = GetComponent<Rigidbody> ();
		myAnim = GetComponent<Animator> ();
		
	}

    private void OnCollisionEnter(Collision other){		
		if (other.collider.tag == "BulletStandard"){
			addDamage (1);
			Destroy (other.gameObject);
		}
	}

	public void addDamage(float damage){

		if (deathBool == false) {
			AudioSource.PlayClipAtPoint (enemyDamageFX, transform.position, 1f);
			myAnim.SetTrigger ("isHit");
			currentHealth -= damage;
            healthImage.fillAmount = (float)currentHealth / (float)fullHealth;

            print ("health is" + currentHealth);
			if (currentHealth <= 0) {
				makeDead ();
			}
		}
	}

	public void makeDead(){
		deathBool = true;
		AudioSource.PlayClipAtPoint (LoseFX, transform.position, 1f);
		myAnim.SetBool ("isDead",true);
		Destroy (gameObject,1.7f);
		myNavAgent.Stop();
		deathBool = true;
	}
}
