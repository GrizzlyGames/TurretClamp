using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class playerHealth : MonoBehaviour {

	public float fullHealth;
	float currentHealth;
	NavMeshAgent myNavAgent;
	public GameObject backoff;
	bool slowTrigger = false;
	bool deadTrigger = false;
	public bool deadBool = false;

	public GameObject springParticle;
	public GameObject fireParticle;

	Animator myAnim;
	Rigidbody myRB;
	WeaponSwitch_Controller weaponSwitch;

    public Image healthImage;

	public AudioClip LoseFX;
	public AudioClip playerDamageFX;


    private void Start () {
		currentHealth = fullHealth;
        UpdateHealthBar();
        myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		weaponSwitch = GetComponent<WeaponSwitch_Controller> ();
		myRB = GetComponent<Rigidbody> ();
		myAnim = GetComponent<Animator> ();
		deadBool = false;
	}
	
    private void UpdateHealthBar()
    {
        healthImage.fillAmount = currentHealth / fullHealth;
    }

	public void addDamage(float damage){
		if (deadBool == false) {
			AudioSource.PlayClipAtPoint (playerDamageFX, transform.position, 1f);

			currentHealth -= damage;
			print ("health is" + currentHealth);
            UpdateHealthBar();

			if (currentHealth <= 0) {
                StartCoroutine(DeathDelay());
			}
		}
	}

    private IEnumerator DeathDelay()
    {
        deadBool = true;
        myAnim.SetBool("isDead", true);
        AudioSource.PlayClipAtPoint(LoseFX, transform.position, 1f);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
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

	private void OnCollisionEnter(Collision other){		
		if (other.collider.tag == "enemyBullet") {
			addDamage (1);
			Destroy (other.gameObject);
		}
	}
}
