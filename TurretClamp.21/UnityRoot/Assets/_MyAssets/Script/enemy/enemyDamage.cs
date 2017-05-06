using UnityEngine;
using System.Collections;

public class enemyDamage : MonoBehaviour {

	public float damage;	//damage enemy does to player
	public float damageRate;	//rate at wich enemy damages player
	public float pushBackForce;		//force applyed to player when contact

	float nextDamage;		//check if enemy can damage player	
	bool playerInRangeofSpike = false;		//check if player in range

	GameObject thePlayer;
	playerHealth thePlayerHealth;	//refernce to the players health code

	public bool springFX = false;	//particles
	public bool fireFX = false;


	// Use this for initialization
	void Start () {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		thePlayerHealth = thePlayer.GetComponent<playerHealth > ();
		nextDamage = Time.time;

	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInRangeofSpike)		//checks player in range bool to actvate attack
			Attack ();


	
	}
	void OnTriggerEnter(Collider other){		//colides with player tag = player in range
		if (other.tag == "Player") {
			playerInRangeofSpike = true;
		}

	}
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			playerInRangeofSpike = false;
		}

	}
	void Attack(){					//what happens when enemy attacks



		if (nextDamage <= Time.time) {		//rate of fire stuff, also particle triggers
			thePlayerHealth.addDamage(damage);

			if(springFX){
				thePlayerHealth.springFX ();
			}
			if(fireFX){
				thePlayerHealth.fireFX();
			}


			nextDamage = Time.time +damageRate;
			pushBack(thePlayer.transform);
		}
	}
	void pushBack(Transform pushedObject){	//applys force to player

		Vector3 pushDirection = new Vector3 ((pushedObject.position.x - transform.position.x), 0, (pushedObject.position.z - transform.position.z));
		pushDirection *= pushBackForce;
		Rigidbody pushedRB = pushedObject.GetComponent<Rigidbody> ();
		pushedRB.velocity = Vector3.zero;
		pushedRB.AddForce (pushDirection, ForceMode.Impulse);
	}







}
