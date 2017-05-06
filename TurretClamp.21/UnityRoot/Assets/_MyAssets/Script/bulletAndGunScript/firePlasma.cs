using UnityEngine;
using System.Collections;

public class firePlasma : MonoBehaviour {

	public float timeBetweenBullets = 0.15f;	//time btween bullet shots
	public GameObject projectile;	//prefab of plasma

	float nextBullet = 0f; //when the next bullet cn be spawned

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Fire2") > 0 && nextBullet < Time.time) {		//rightClick and to see if you can fire a shot
			nextBullet = Time.time +timeBetweenBullets;
			Instantiate(projectile, transform.position, transform.rotation);
		}
	
	}
}
