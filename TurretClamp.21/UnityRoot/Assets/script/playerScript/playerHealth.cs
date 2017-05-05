using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class playerHealth : MonoBehaviour {

	public float fullHealth;
	float currentHealth;
	UnityEngine.AI.NavMeshAgent myNavAgent;
	public GameObject backoff;
	bool slowTrigger = false;
	bool deadTrigger = false;
	public bool deadBool = false;

	public GameObject springParticle;
	public GameObject fireParticle;

	Animator myAnim;
	Rigidbody myRB;
	WeaponSwitch_Controller weaponSwitch;

	public Slider playerHealthSlider;
	public AudioClip LoseFX;
	public AudioClip playerDamageFX;


	// Use this for initialization
	void Start () {

		currentHealth = fullHealth;
		playerHealthSlider.maxValue = fullHealth * 2;
		playerHealthSlider.value = currentHealth;
		myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		weaponSwitch = GetComponent<WeaponSwitch_Controller> ();
		myRB = GetComponent<Rigidbody> ();
		myAnim = GetComponent<Animator> ();
		deadBool = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void addDamage(float damage){
		if (deadBool == false) {
			AudioSource.PlayClipAtPoint (playerDamageFX, transform.position, 1f);



			currentHealth -= damage;
			print ("health is" + currentHealth);

			playerHealthSlider.value = currentHealth;

			if (currentHealth <= 0) {
				//gameObject.GetComponent(SphereCollider).o;
				makeDead ();

			}
		} else if (deadBool) {
			
		}
	}
	public void makeDead(){
		deadBool = true;

		myAnim.SetBool ("isDead",true);
		AudioSource.PlayClipAtPoint (LoseFX, transform.position, 1f); 
		DestroyObject (weaponSwitch.singleClone);
		DestroyObject (weaponSwitch.doubleClone);
		DestroyObject (weaponSwitch.plasmaClone);
		Destroy (gameObject,10f);



	}
	public void slowDown(float reduce){
		if (!slowTrigger) {
			myNavAgent.speed -= reduce;
			slowTrigger=true;
		}
	}
	public void speedUp(float reduce){
		if (slowTrigger) {
			myNavAgent.speed += reduce;
			slowTrigger=false;
		}
	}
	public void springFX(){
		Instantiate (springParticle, transform.position,Quaternion.Euler (new Vector3 (-90,0,0)));
	}
	public void fireFX(){
		var fire = (GameObject)Instantiate (fireParticle, transform.position,Quaternion.Euler (new Vector3 (-90,0,0)));
		fire.transform.SetParent (gameObject.transform);
	}

	void OnCollisionEnter(Collision other){		
		if (other.collider.tag == "enemyBullet") {
			addDamage (1);
			Destroy (other.gameObject);
		}
	}
}
