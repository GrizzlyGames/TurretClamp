using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info_Button : MonoBehaviour {

	public GameObject page;

	public void SwitchPage(){

		Debug.Log ("switch go" + page.active);
		if (page.active)
			page.SetActive (false);
		else
			page.SetActive (true);
	}
}
