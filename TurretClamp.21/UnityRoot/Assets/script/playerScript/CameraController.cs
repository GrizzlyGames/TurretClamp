using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float rotationSpeed = 40f;
	public Transform target;
	public float smoothing = 5f;	//controls how smoth the  stops and starts are


	public float MaxSpeed = 30.0f;
	private float CurrentSpeed = 0.0f;
	public float Acceleration = 1.0f; //per second

	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - target.position; 
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") > 0) {
			if (CurrentSpeed < MaxSpeed) {
				CurrentSpeed += Acceleration;
			}
			transform.Rotate (Vector3.up * CurrentSpeed * Time.deltaTime);

			//	transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);
		} else if (Input.GetAxis ("Horizontal") < 0) {

			if (CurrentSpeed > MaxSpeed * (-1.0f)) {
				CurrentSpeed -= Acceleration;
			}
			transform.Rotate (Vector3.up * CurrentSpeed * Time.deltaTime);


			//	transform.Rotate (Vector3.up * -rotationSpeed * Time.deltaTime);

		} else {
			CurrentSpeed = Mathf.Lerp (CurrentSpeed, 0, Time.deltaTime);
			transform.Rotate (Vector3.up * CurrentSpeed * Time.deltaTime);
		
		}


	
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);  
	
	}
}
