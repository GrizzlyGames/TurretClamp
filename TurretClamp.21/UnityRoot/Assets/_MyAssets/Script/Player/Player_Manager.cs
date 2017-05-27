using UnityEngine;
using System.Collections;
using UnityEngine.AI;
[RequireComponent(typeof(Player_Health))]
public class Player_Manager : MonoBehaviour
{
    public GameObject dirtPrefab;
    private GameObject dirtInstance;

    public AudioSource moveAudioSource;
    private Animator animator;
    private Rigidbody rb;
    private NavMeshAgent myNavAgent;

    private int currentGems = 0;
    public bool isDug = false;

    //marker gameobjects
    private Vector3 prevLocation;   //saves location of markers
    private Vector3 hitLocation;

    public GameObject greenMarkerPrefab;
    private GameObject greenMarkerInstance;

    public GameObject redMarkerPrefab;
    private GameObject redMarkerInstance;

    //rock stuff
    public GameObject rockParticle;
    private float rockTimer = 0;

    //audio
    private float walkAudioTimer = 0;
    public AudioClip digUpFX;
    public AudioClip digDownFX;
    public AudioClip boopFX;

    private Player_Health healthScript;

    private void Start()
    {
        healthScript = GetComponent<Player_Health>();
        animator = GetComponent<Animator>();
        myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        prevLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (healthScript.isAlive)
        {
            // Makes sure plyer doesnt walk back when hit
            if (rb.velocity.magnitude < myNavAgent.speed && rb.velocity.magnitude >= myNavAgent.speed * 0.25f)
            {
                myNavAgent.destination = transform.position;
            }

            #region Movement Logic
            if (myNavAgent.remainingDistance != 0) // Is moving
            {
                animator.SetBool("isRunning", true);
                #region Walk Audio
                walkAudioTimer += Time.deltaTime;
                if (walkAudioTimer >= 0.15f)
                {
                    Debug.Log("Walk audio timer: " + walkAudioTimer);
                    walkAudioTimer = 0;
                    if (!moveAudioSource.isPlaying)
                        moveAudioSource.Play();
                }
                #endregion
                #region Rock Particle EFX
                rockTimer += Time.deltaTime;
                float tmpCondition = Random.Range(0.2f, 0.5f);
                if (rockTimer >= tmpCondition)
                {
                    rockTimer = 0;
                    Instantiate(rockParticle, transform.position, transform.rotation);
                }
                #endregion
            }
            else // Is not moving
            {
                animator.SetBool("isRunning", false);
                if (!moveAudioSource.isPlaying)
                    moveAudioSource.Stop();
            }
            #endregion
            #region S Key logic
            if (Input.GetKeyDown(KeyCode.S))
            {
                myNavAgent.destination = prevLocation;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                myNavAgent.destination = hitLocation;
            }
            #endregion
            #region Movement Target
            if (Input.GetButtonDown("Fire1") && !isDug)
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

                        Destroy(greenMarkerInstance);           //desroys old markers and then makes new ones
                        Destroy(redMarkerInstance);
                        greenMarkerInstance = (GameObject)Instantiate(greenMarkerPrefab, hitLocation, transform.rotation);
                        redMarkerInstance = (GameObject)Instantiate(redMarkerPrefab, prevLocation, transform.rotation);
                        myNavAgent.isStopped = false;
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(boopFX, transform.position, 1);
                    }
                }
            }
            #endregion
            #region Dig Input Logic
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                if (isDug)
                {
                    isDug = false;
                    Destroy(dirtInstance);
                    animator.SetBool("isDug", false); //makes sure the animator know ou are digging out
                    AudioSource.PlayClipAtPoint(digUpFX, transform.position, 10f);
                }
                else
                {
                    Destroy(greenMarkerInstance);
                    Destroy(redMarkerInstance);
                    isDug = true;
                    animator.SetBool("isDug", true);
                    animator.SetBool("isRunning", false);
                    AudioSource.PlayClipAtPoint(digDownFX, transform.position, 1);
                    myNavAgent.destination = transform.position;        //sets the positoin to the players transform, so the plyer stops
                    myNavAgent.isStopped = true;
                    dirtInstance = (GameObject)Instantiate(dirtPrefab, transform.position, transform.rotation);
                }
            }
            #endregion
        }
        else
            myNavAgent.speed = 0;
    }

    public void AddGems(int value)
    {
        currentGems += value;
        print("Gems: " + currentGems);
    }
}
