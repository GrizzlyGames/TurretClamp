using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyTurretController : MonoBehaviour {
	Animator myAnim;
	Rigidbody myRB;

	UnityEngine.AI.NavMeshAgent myNavAgent;

	public GameObject  Player;

	RaycastHit shootHit;	

	float rayTimer;
	float nextRayFire;

	Vector3 prevLocation;

	public bool chase = false;

	enemyHealth health;

	public AudioClip walkFX;
	float walkTimer = .15f;
	float walkTimerFire;

	void Start () {
		myAnim = GetComponent<Animator>();
		myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		health = GetComponent<enemyHealth> ();
		myRB = GetComponent<Rigidbody> ();
		prevLocation = transform.position;
	}
	void Update () {
		if (myNavAgent.remainingDistance != 0) {									
			myAnim.SetBool ("isRunning",true); 
			if(Time.time>walkTimerFire){
				AudioSource.PlayClipAtPoint(walkFX,transform.position,.6f);

				walkTimerFire = Time.time+walkTimer;
			}

		} else {
			myAnim.SetBool ("isRunning", false);
		}
}
	void OnTriggerStay(Collider other){							
		rayTimer =Random.Range (.6f,1f);
		if (other.tag == "Player") {					
			Vector3 rayDirection = other.gameObject.transform.position - transform.position;
			Ray enemytoplayerRay;
			if (Time.time > nextRayFire) {
				if (Physics.Raycast (transform.position, rayDirection, out shootHit, 5)) {
					if (shootHit.collider.tag == "Player") {
						chase = true;
					} else {
						chase = false;
					}
					if (chase) {
						if (shootHit.distance > 4f) {
							myNavAgent.destination = transform.position;
						} else if (shootHit.distance > 2f && shootHit.distance <= 4f) {
							prevLocation = transform.position;
							myNavAgent.destination = Player.transform.position;
							myNavAgent.speed = .4f;
						} else if (shootHit.distance > .8f && shootHit.distance <= 2f) {
							myNavAgent.destination = Player.transform.position;
							myNavAgent.speed = .8f;
						} else if (shootHit.distance <= .8f) {
							prevLocation = 1f * Vector3.Normalize(transform.position - Player.transform.position) + Player.transform.position;
							myNavAgent.speed = 1f;
							myNavAgent.destination = prevLocation;
						}
					} else {
					}
					nextRayFire = Time.time + rayTimer;
				}
			}
		} 
	}
}