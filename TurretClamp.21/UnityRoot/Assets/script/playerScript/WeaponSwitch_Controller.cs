using UnityEngine;
using System.Collections;

public class WeaponSwitch_Controller : MonoBehaviour {

	public GameObject singleGunPrefab;
	public GameObject doubleGunPrefab;
	public GameObject plasmaGunPrefab;

	public Transform gunSpawn;
	public GameObject singleClone;
	public GameObject doubleClone;
	public GameObject plasmaClone;
	bool isSingle;
	bool isDouble;
	bool isPlasma;
	public bool inputSingle;
	public bool inputDouble;
	public bool inputPlasma;



	// Use this for initialization
	void Start () {
		isSingle = true;  
		isDouble = false;
		isPlasma = false;
		inputSingle = true;
		inputDouble = false;
		inputPlasma = false;


		singleClone = (GameObject)Instantiate (singleGunPrefab, gunSpawn.position, gunSpawn.rotation);
		singleClone.transform.parent = gunSpawn;
	}
	
	// Update is called once per frame
	void Update () {
	


		if(inputSingle  && !isSingle){

			Destroy (doubleClone);
			Destroy (plasmaClone);
			singleClone = (GameObject)Instantiate (singleGunPrefab, gunSpawn.position, gunSpawn.rotation);
			singleClone.transform.parent = gunSpawn;


			isSingle =true;
			isDouble = false;
			isPlasma = false;
			}

		if(inputDouble && !isDouble){


			Destroy (singleClone);
			Destroy (plasmaClone);
			doubleClone = (GameObject)Instantiate (doubleGunPrefab, gunSpawn.position, gunSpawn.rotation);
			doubleClone.transform.parent = gunSpawn;


			isSingle = false;
			isDouble = true;
			isPlasma = false;
			}

		if(inputPlasma && !isPlasma){


			Destroy (doubleClone);
			Destroy (singleClone);
			plasmaClone = (GameObject)Instantiate (plasmaGunPrefab, gunSpawn.position, gunSpawn.rotation);
			plasmaClone.transform.parent = gunSpawn;


			isSingle = false;
			isDouble = false;
			isPlasma = true;
			}
		}
}
