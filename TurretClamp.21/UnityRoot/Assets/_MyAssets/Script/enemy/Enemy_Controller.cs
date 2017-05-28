using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy_Controller : MonoBehaviour
{
    public Animator weaponAnimator;
    public Transform weaponTransform;
    private Animator animator;
    private NavMeshAgent myNavAgent;
    private GameObject player;
    private Vector3 prevLocation;
    private enemyHealth health;
    public AudioClip walkFX;

    float walkTimer = .15f;
    float walkTimerFire;

    private bool isShooting = false;

    private float distance = 0; // Distance from player

    public GameObject BulletPrefab;
    public Transform bulletSpawn;
    private float speed = 6f;
    private bool canSeePlayer = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        myNavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        health = GetComponent<enemyHealth>();
        prevLocation = transform.position;

        StartCoroutine(AIDelay());
    }
    private void Update()
    {
        if (myNavAgent.remainingDistance != 0)
        {
            animator.SetBool("isRunning", true);
            if (Time.time > walkTimerFire)
            {
                AudioSource.PlayClipAtPoint(walkFX, transform.position, .6f);
                walkTimerFire = Time.time + walkTimer;
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 rayDirection = player.transform.position - this.transform.position;
        RaycastHit hitInfo;
        // Debug.DrawRay(transform.position, rayDirection, Color.red);
        if (Physics.Raycast(transform.position, rayDirection, out hitInfo, 4))
        {
            // Debug.Log(hitInfo.transform.name);
            if (hitInfo.transform.CompareTag("Player"))
            {
               // Debug.Log("Enemy can see player.");
                canSeePlayer = true;
                weaponTransform.LookAt(player.transform.position); // Look at player 
            }
            else
                canSeePlayer = false;
        }
    }
    // Use raycast to check line of sight to player, if can't see player, don't chase.

    private IEnumerator AIDelay()
    {
        float rayTimer = Random.Range(.6f, 1f);
        yield return new WaitForSeconds(rayTimer);
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        // Debug.Log("Enemy distance from player: " + distance);
        if (distance < 4)
        {
            if (canSeePlayer)
            {
                if (!isShooting)
                    StartCoroutine(ShootDelay());
                if (distance > 2f)
                {
                    prevLocation = transform.position;
                    myNavAgent.destination = player.transform.position;
                    myNavAgent.speed = .4f;
                }
                else
                {
                    if (distance > .8f)
                    {
                        myNavAgent.destination = player.transform.position;
                        myNavAgent.speed = .8f;
                    }
                    else
                    {
                        prevLocation = 1f * Vector3.Normalize(transform.position - player.transform.position) + player.transform.position;
                        myNavAgent.speed = 1f;
                        myNavAgent.destination = prevLocation;
                    }
                }
            }            
        }
        else
        {
            myNavAgent.destination = transform.position;
        }
        StartCoroutine(AIDelay());
    }
    private IEnumerator ShootDelay()
    {
        isShooting = true;
        float fireTimer = Random.Range(.5f, 1f);
        yield return new WaitForSeconds(fireTimer);
        weaponAnimator.SetTrigger("shoot");
        GameObject bullet = Instantiate(BulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;
        Destroy(bullet, 1f);
        isShooting = false;
    }
}