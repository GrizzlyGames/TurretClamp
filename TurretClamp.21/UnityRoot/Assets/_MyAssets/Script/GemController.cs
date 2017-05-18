using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour {

	public int gemValue;
	public GameObject collectParticle;
	public AudioClip pickUpSound;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Instantiate (collectParticle, transform.position, Quaternion.Euler (new Vector3 (-90,0,0)));
			other.gameObject.GetComponent<ClawPlayerController> ().AddGems (gemValue);
			other.gameObject.GetComponent<Player_Health> ().addDamage (-3);
			AudioSource.PlayClipAtPoint(pickUpSound,transform.position,.5f);
			Destroy (transform.root.gameObject);
			 
		}

	}

}
