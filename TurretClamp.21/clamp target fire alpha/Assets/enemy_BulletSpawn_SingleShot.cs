using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_BulletSpawn_SingleShot : MonoBehaviour {

	public GameObject BulletPrefab;
	public Transform bulletSpawn;
	float timeSpawn= 2.0f;	//lifeTime of bullet
	float speed = 6f;
	enemyHealth myhealth;
	enemyTurretController moveCode;
	float fireTimer;
	float nextFireTime;

	// Use this for initialization
	void Start () {
		moveCode = GetComponentInParent<enemyTurretController> ();
		myhealth = GetComponentInParent<enemyHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		fireTimer = Random.Range (.5f, 1f);
		if(!myhealth.deathBool){
			if (moveCode.chase) {
				if (Time.time > nextFireTime) {
					var bullet = (GameObject)Instantiate (BulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
					bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * speed;
					Destroy (bullet, 1f);
					nextFireTime = Time.time + fireTimer;
				}
			}
		}

	}
}
