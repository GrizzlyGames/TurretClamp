using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGunController : MonoBehaviour {

	enemyTurretController moveCode;
	Vector3 playerPosition;
	void Start () {
		moveCode = GetComponentInParent<enemyTurretController> ();
	}
	void Update () {
		if (moveCode.chase) {
			transform.LookAt (moveCode.Player.transform.position);
		}
	}
}
