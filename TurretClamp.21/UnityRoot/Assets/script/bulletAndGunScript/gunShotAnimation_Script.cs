using UnityEngine;
using System.Collections;

public class gunShotAnimation_Script : MonoBehaviour {

	Animator myAnim;	//animation of guns

	// Use this for initialization
	void Start () {

		myAnim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Fire2")) {		//rightClick and change animation status
			myAnim.SetBool ("isFiring", true);
		} else if (Input.GetButtonUp ("Fire2")) {
			myAnim.SetBool ("isFiring", false);
		}
	
	}
}
