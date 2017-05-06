using UnityEngine;
using System.Collections;

public class attachment_nodeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Ray camtoNavRay = Camera.main.ScreenPointToRay (Input.mousePosition);		//tells the game where the ray originates and where it hits
		RaycastHit hit;

		if (Physics.Raycast (camtoNavRay, out hit, 10)) {	//makes ray and then watches it hit

			if(hit.collider.tag !="noAim"){		//if what it hits is a no aim tag

				transform.LookAt(hit.point);
			}
		}
	
	}
}
