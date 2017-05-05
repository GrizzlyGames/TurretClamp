using UnityEngine;
using System.Collections;

public class gemHealth : MonoBehaviour {

	//health stuff
	public float fullHealth;	
	float currentHealth;		
	public float pickUpSpeed=2f;	//initial velocity of gem pickup

	public GameObject gemParticle;	//particles
	public GameObject gemPickup;
	public GameObject itemSpawn;

	public AudioClip breakFX;
	public AudioClip shotFX;

	//animator stuff;
	Animator myAnim;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator> ();
		currentHealth = fullHealth;


	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
	public void addDamage(float damage){	//add damage called on collision
		currentHealth -= damage;
		Instantiate (gemParticle, transform.position,Quaternion.Euler (new Vector3 (-90,0,0)));
		AudioSource.PlayClipAtPoint (shotFX, transform.position, .2f);

		if (currentHealth <= 0) {
			makeDead();

		}
	}
	public void makeDead(){	
		AudioSource.PlayClipAtPoint (breakFX , transform.position, .8f);	//destroys game object
		Collider[] theColliders = GetComponents<Collider> ();		//fills an array of colliders of the game objct so we cn disable them
		for(int i = 0; i< theColliders.Length; i++){
			theColliders [i].enabled = false;				// disables the colliders
		}

		myAnim.SetBool ("isBroken",true);
		var gem = (GameObject)Instantiate (gemPickup, itemSpawn.transform.position,itemSpawn.transform.rotation);		//instance gem item
			//gem.GetComponent<Rigidbody> ().velocity = gem.transform.up * pickUpSpeed;

	}

	void OnTriggerEnter(Collider other){		//on colllision activate damage
		myAnim.SetTrigger ("isShake");
		//myAnim.SetTrigger ("isShake");
		if (other.tag == "BulletStandard") {
			
			addDamage (1);
			print ("hit!");
		}

	}

}
