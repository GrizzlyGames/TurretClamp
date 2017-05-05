using UnityEngine;
using System.Collections;

public class BulletSpawn_PlasmaShot : MonoBehaviour {
	
	public GameObject BulletPrefab;
	public Transform bulletSpawn;

	public float speed = 6f;	//speed of bullet
	public float TimerFire = 1f;	//time between next bullet instantiation
	public float spawnTime= 0.5f;	//lifetime of bullet
	float nextTimerFire;	//when the next bullet is fired

	public AudioClip bulletFX;
	// Use this for initialization
	void Start () {

		nextTimerFire = Time.time + TimerFire;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButton ("Fire2")) {	//get rightCLick down

			if(Time.time > nextTimerFire){	//checks if you can fire with timer
				AudioSource.PlayClipAtPoint(bulletFX,transform.position,30f);
				Fire();
				nextTimerFire = Time.time+TimerFire;
			}
		}
		
	}
	
	void Fire(){	//instantiates bullet for an amount of spawntime
		var bullet = (GameObject)Instantiate (BulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * speed;
		
		Destroy (bullet, spawnTime);
		
	}
	
	
	
	
}