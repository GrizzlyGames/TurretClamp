using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunpickupController : MonoBehaviour {

	public GameObject deadParticle;

	public bool Single;
	public bool Double;
	public bool Plasma;

	GameObject theSwitcher;

	WeaponSwitch_Controller switcher;

	public AudioClip pickUpFX;

	void Start () {

		theSwitcher = GameObject.FindGameObjectWithTag ("GunSpawn");
		switcher = theSwitcher.GetComponent<WeaponSwitch_Controller> ();

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "GunSpawn") {

			AudioSource.PlayClipAtPoint(pickUpFX,transform.position,.6f);
			if (Single) {

				switcher.inputSingle = true;
				switcher.inputDouble = false;
				switcher.inputPlasma = false;

			} else if (Double) {

				switcher.inputSingle = false;
				switcher.inputDouble = true;
				switcher.inputPlasma = false;


			} else if (Plasma) {

				switcher.inputSingle = false;
				switcher.inputDouble = false;
				switcher.inputPlasma = true;


			}

			Instantiate (deadParticle, transform.position, transform.rotation);

			Destroy (this.transform.parent.gameObject);


		}

	}

}
