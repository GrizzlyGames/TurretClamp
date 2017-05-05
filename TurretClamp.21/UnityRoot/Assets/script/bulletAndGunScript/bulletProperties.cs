using UnityEngine;
using System.Collections;

public class bulletProperties : MonoBehaviour {
	public float damage = 1f;	//damage the bullet does
	public GameObject ricochetFX;	//ricochet effect

	void OnTriggerStay(Collider other){
		if (other.gameObject.layer == LayerMask.NameToLayer ("Shootable")) {	//looks for shotable layer to destroy bullet
			Instantiate (ricochetFX, transform.position, Quaternion.Euler (new Vector3 (-90, 0, 0)));
			Destroy (gameObject);
		} else if (other.gameObject.tag == "BulletStandard") {
			Instantiate (ricochetFX, transform.position, Quaternion.Euler (new Vector3 (-90, 0, 0)));
			Destroy (gameObject);
			}
		}
	}



	

