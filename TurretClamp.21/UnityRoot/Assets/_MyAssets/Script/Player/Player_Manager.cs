using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class Player_Manager : MonoBehaviour {

	public GameObject dirtPrefab;

	private Animator animator;
    private Rigidbody rigidbody;
    private NavMeshAgent myNavAgent;

	int currentGems = 0;

	bool dug = false;	//bool to see if the player is in the ground
	bool speedSwitch = false;	//used to check if the player is pushed back

	GameObject dirt;
																					//marker gameobjects
	Vector3 prevLocation;	//saves location of markers
	Vector3 hitLocation;

	public GameObject Marker;	//gme objets for markers
	public GameObject preMarker;

	GameObject markerClone;			//the marker clone instantiations
	GameObject preMarkerClone;

	//bool markerSwitch = false;

																																//rock stuff
	public GameObject rockParticle;
	float rockTimer = .15f;
	float nextrockTimerFire;

	//audio
	public AudioClip digUpFX;
	public AudioClip digDownFX;
	public AudioClip walkFX;
	public AudioClip boopFX;

    public Player_Health healthScript;

    private void Start () {
		animator = GetComponent<Animator>();
		myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		prevLocation = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		rigidbody = GetComponent<Rigidbody> ();
	}
    private void Update () {
        if (healthScript.isAlive)
        {
            if (!speedSwitch && rigidbody.velocity.magnitude < myNavAgent.speed)
            {                             //makes sure plyer doesnt walk back when hit
                speedSwitch = true;
            }

            if (speedSwitch)
            {
                if (rigidbody.velocity.magnitude >= myNavAgent.speed / 4)
                {
                    myNavAgent.destination = transform.position;
                    speedSwitch = false;
                }
            }

            //raycast stuff to make it follow
            if (myNavAgent.remainingDistance != 0)
            {
                animator.SetBool("isRunning", true);
                if (Time.time > nextrockTimerFire)
                {
                    AudioSource.PlayClipAtPoint(walkFX, transform.position, 1f);
                    Instantiate(rockParticle, transform.position, transform.rotation);
                    nextrockTimerFire = Time.time + rockTimer;
                }
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                myNavAgent.destination = prevLocation;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                myNavAgent.destination = hitLocation;

            }
            if (Input.GetButtonDown("Fire1") && !dug)
            {                                                           // hit leftClick, make ray for player to follow
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10))
                {
                    if (hit.collider.tag != "Blocked")
                    {
                        prevLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                        myNavAgent.destination = hit.point;
                        hitLocation = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                        Destroy(markerClone);           //desroys old markers and then makes new ones
                        Destroy(preMarkerClone);
                        markerClone = (GameObject)Instantiate(Marker, hitLocation, transform.rotation);
                        preMarkerClone = (GameObject)Instantiate(preMarker, prevLocation, transform.rotation);
                        myNavAgent.isStopped = false;
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(boopFX, transform.position, 2f);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {       //dig in ground code

                if (dug)
                {       //if dug into the ground, come out of ground
                    dug = false;
                    Destroy(dirt);
                    animator.SetBool("isDug", false); //makes sure the animator know ou are digging out
                    AudioSource.PlayClipAtPoint(digUpFX, transform.position, 10f);

                }
                else
                {       //if not dug in, start digging
                    Destroy(markerClone);
                    Destroy(preMarkerClone);
                    dug = true;
                    animator.SetBool("isDug", true);
                    animator.SetBool("isRunning", false);

                    AudioSource.PlayClipAtPoint(digDownFX, transform.position, 10f);

                    myNavAgent.destination = transform.position;        //sets the positoin to the players transform, so the plyer stops
                    myNavAgent.isStopped = true;
                    dirt = (GameObject)Instantiate(dirtPrefab, transform.position, transform.rotation);
                    if (Time.time > nextrockTimerFire)
                    {
                        Instantiate(rockParticle, transform.position, transform.rotation);
                        nextrockTimerFire = Time.time + rockTimer;
                    }
                }
            }            
        }
        else
            myNavAgent.speed = 0;
    }

	public void AddGems(int value){ //gem stuff
		currentGems +=value;
		print("Gems: " + currentGems);
	}
}
