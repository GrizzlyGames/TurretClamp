﻿using UnityEngine;
using System.Collections;

public class BulletSpawn_SingleShot : MonoBehaviour {

	public GameObject BulletPrefab;
	public Transform bulletSpawn;
	public float timeSpawn= 2.0f;	//lifeTime of bullet
	public float speed = 6f;	//speed of bullet
	bool isDug = false;		//check to see if player is dug in
	public AudioClip bulletFX;
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {	//check middleClick

			if(isDug){		//check to see if player is dug in
			isDug = false;
			}else if(!isDug){
				isDug = true;
			}
		}

		if (Input.GetButtonDown ("Fire2")) {	//check rightClick
			Fire();
			AudioSource.PlayClipAtPoint(bulletFX,transform.position,1f);
		
		}
	
	}

	void Fire(){		//Instantiates bullet for time of timeSpawn
		
		var bullet = (GameObject)Instantiate (BulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		if (isDug) {																							//changes velocity of bullet if player is dug in or not
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * speed;
		} else {
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * Random.Range (speed - 5, speed - 3);
		}

		Destroy (bullet, timeSpawn);

	}




}
