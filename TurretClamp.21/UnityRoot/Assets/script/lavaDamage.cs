using UnityEngine;
using System.Collections;

public class lavaDamage : MonoBehaviour {

	public float damage;
	public float damageRate;
	public float reducePlayerSpeed;		//lava reduces players speed by this

	float nextDamage;
	bool playerInRange = false;

	GameObject thePlayer;
	playerHealth thePlayerHealth;
	UnityEngine.AI.NavMeshAgent playerNavMesh;		//nav mesh caller

	// Use this for initialization
	void Start () {

		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		thePlayerHealth = thePlayer.GetComponent<playerHealth > ();
		playerNavMesh = thePlayer.GetComponent<UnityEngine.AI.NavMeshAgent> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInRange)	//bool 
			lavaAttack ();
	
	}
	void OnTriggerEnter(Collider other){		//set player in range and slows down player by calling the player healths slowdown function
		if (other.tag == "Player") {
			playerInRange = true;
			thePlayerHealth.slowDown(reducePlayerSpeed);
		}
		
	}
	void OnTriggerExit(Collider other){		//speeds up player with speed up function in playerhealth
		if (other.tag == "Player") {
			playerInRange = false;
			thePlayerHealth.speedUp(reducePlayerSpeed);
		}
		
	}
	void lavaAttack(){						//adds damage
		if (nextDamage <= Time.time) {
			thePlayerHealth.addDamage(damage);
			thePlayerHealth.fireFX();

			nextDamage = Time.time +damageRate;
		}
	}
}
