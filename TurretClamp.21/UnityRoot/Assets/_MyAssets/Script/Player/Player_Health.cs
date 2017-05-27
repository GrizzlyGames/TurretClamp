using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Player_Health : MonoBehaviour {

    public float fullHealth;
    public float currentHealth;
	
	public GameObject backoff;
	bool slowTrigger = false;
	bool deadTrigger = false;
	public bool isAlive = true;

	public GameObject springParticle;
	public GameObject fireParticle;

    private NavMeshAgent myNavAgent;

    private Animator animator;
    public Image healthImage;

	public AudioClip LoseFX;
	public AudioClip playerDamageFX;

    private void Start () {
		currentHealth = fullHealth / 3;
        UpdateHealthBar();
        myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}
	
    private void UpdateHealthBar()
    {
        healthImage.fillAmount = currentHealth / fullHealth;
    }

	public void addDamage(float damage){
		if (isAlive) {
            currentHealth -= damage;
            UpdateHealthBar();
            AudioSource.PlayClipAtPoint (playerDamageFX, transform.position, 1f);			

			if (currentHealth < 1) {
                StartCoroutine(DeathDelay());
			}
		}
	}

    private IEnumerator DeathDelay()
    {
        isAlive = false;
        animator.SetBool("isDead", true);
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
		GameObject fire = Instantiate (fireParticle, transform.position,Quaternion.identity) as GameObject;
		fire.transform.SetParent (gameObject.transform);
	}

	private void OnCollisionEnter(Collision other){		
		if (other.collider.tag == "enemyBullet") {
			addDamage (1);
			Destroy (other.gameObject);
		}
	}
}
