using UnityEngine;
using System.Collections;

public class shootPlasmaBlast : MonoBehaviour {

	public float range =3f;		//range of plasmaShot
	public float damage = .1f;	//damage of plasmaShot

	Ray shootRay;		//ray that the plasma follows
	RaycastHit shootHit;	//wherever the ray makes contact
	int shootableMask;	//mask that the raycast is looking for
	LineRenderer gunLine;	//rendered line aloong raycast

	// Use this for initialization
	void Awake () {
	
		shootableMask = LayerMask.GetMask ("Shootable");
		gunLine = GetComponent<LineRenderer> ();

		shootRay.origin = transform.position;		//setup for the ray and the gunline
		shootRay.direction = transform.forward;
		gunLine.SetPosition (0, transform.position);

		if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {	//shoots the ray and checks it shootHit
			//hat an enemy goes here
			gunLine.SetPosition (1, shootHit.point);
		} else
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
