using UnityEngine;
using System.Collections;

public class ItemRotateScript : MonoBehaviour {
	public float rotateSpeed = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.up * rotateSpeed);		//rotates item constantly
	
	}
}
