using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet_Proporties : MonoBehaviour {

	void OnTriggerStay(Collider other){
		if (other.gameObject.layer == LayerMask.NameToLayer ("Shootable")) {	//looks for shotable layer to destroy bullet
			Destroy (gameObject);
		}
	}
}
