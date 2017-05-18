using UnityEngine;
using System.Collections;

public class ClawPlayerController : MonoBehaviour {

	public GameObject dirtPrefab;
	Animator myAnim;
	Rigidbody myRB;
	UnityEngine.AI.NavMeshAgent myNavAgent;

	//for the gemhealth things
	int currentGems = 0;

	bool dug;		//bool to see if the player is in the ground
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

																										// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator>();
		myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		prevLocation = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		dug = false;
		myRB = GetComponent<Rigidbody> ();
	
		speedSwitch = false;
	}
	
																												// Update is called once per frame
	void Update () {
        if (healthScript.isAlive)
        {
            Ray camtoNavRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (!speedSwitch && myRB.velocity.magnitude < myNavAgent.speed)
            {                             //makes sure plyer doesnt walk back when hit
                speedSwitch = true;
            }

            if (speedSwitch)
            {
                if (myRB.velocity.magnitude >= myNavAgent.speed / 4)
                {
                    myNavAgent.destination = transform.position;
                    speedSwitch = false;
                }
            }


            //raycast stuff to make it follow
            if (myNavAgent.remainingDistance != 0)
            {
                myAnim.SetBool("isRunning", true);
                if (Time.time > nextrockTimerFire)
                {
                    AudioSource.PlayClipAtPoint(walkFX, transform.position, 1f);
                    Instantiate(rockParticle, transform.position, transform.rotation);
                    nextrockTimerFire = Time.time + rockTimer;
                }




            }
            else
            {
                myAnim.SetBool("isRunning", false);
                //prevLocation = transform.position;
                //print (prevLocation);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {                       //makes the player back stack when you hit the s key
                myNavAgent.destination = prevLocation;

            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                myNavAgent.destination = hitLocation;

            }

            if (Input.GetButtonDown("Fire1") && !dug)
            {                                                           // hit leftClick, make ray for player to follow

                if (Physics.Raycast(camtoNavRay, out hit, 10))
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

                        myNavAgent.Resume();

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
                    myAnim.SetBool("isDug", false); //makes sure the animator know ou are digging out
                    AudioSource.PlayClipAtPoint(digUpFX, transform.position, 10f);

                }
                else
                {       //if not dug in, start digging
                    Destroy(markerClone);
                    Destroy(preMarkerClone);
                    dug = true;
                    myAnim.SetBool("isDug", true);
                    myAnim.SetBool("isRunning", false);

                    AudioSource.PlayClipAtPoint(digDownFX, transform.position, 10f);

                    myNavAgent.destination = transform.position;        //sets the positoin to the players transform, so the plyer stops
                    myNavAgent.Stop();
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

	public void AddGems(int value){//gem stuff
		currentGems +=value;
		print(currentGems);

	}
}
