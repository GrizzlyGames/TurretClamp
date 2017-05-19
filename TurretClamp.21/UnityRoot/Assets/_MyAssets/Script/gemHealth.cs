using UnityEngine;
using System.Collections;

public class gemHealth : MonoBehaviour {

	public float fullHealth;	
	private float currentHealth;		
	public float pickUpSpeed=2f;	//initial velocity of gem pickup

	public GameObject gemParticle;	//particles
	public GameObject gemPickup;
	public GameObject itemSpawn;

	public AudioClip breakFX;
	public AudioClip shotFX;

    private Animator animator;

    private void Start () {
		animator = GetComponent<Animator> ();
		currentHealth = fullHealth;	
	}

	private void addDamage(){
		currentHealth--;
		Instantiate (gemParticle, transform.position,Quaternion.Euler (new Vector3 (-90,0,0)));
		AudioSource.PlayClipAtPoint (shotFX, transform.position, .2f);
		if (currentHealth <= 0) {
			makeDead();
		}
	}

    private void makeDead(){	
		AudioSource.PlayClipAtPoint (breakFX , transform.position, .8f);	//destroys game object
		Collider[] theColliders = GetComponents<Collider> ();		//fills an array of colliders of the game objct so we cn disable them
		for(int i = 0; i< theColliders.Length; i++){
			theColliders [i].enabled = false;				// disables the colliders
		}
		animator.SetBool ("isBroken",true);
		Instantiate (gemPickup, itemSpawn.transform.position,itemSpawn.transform.rotation);
	}

    private void OnTriggerEnter(Collider other){
		animator.SetTrigger ("isShake");
		if (other.tag == "PlayerBullet") {			
			addDamage();
		}

	}

}
