using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class enemyHealth : MonoBehaviour
{
    public bool isAlive = true;
    private float currentHealth;
    public float maxHealth = 10;

    private NavMeshAgent myNavAgent;
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip deadSFX;
    public AudioClip damageSFX;

    public Image healthImage;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeathBar();
        myNavAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateHeathBar()
    {
        if (currentHealth > 1)
            healthImage.fillAmount = currentHealth / maxHealth;
        else
            healthImage.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("PlayerBullet"))
        {
            if (isAlive)
                TakeDamage();
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage()
    {
        Debug.Log("Enemy took damage.");
        UpdateHeathBar();
        audioSource.clip = damageSFX;
        audioSource.Play();
        audioSource.loop = false;

        animator.SetTrigger("isHit");

        currentHealth--; ;
        if (currentHealth < 1)
            makeDead();
    }

    public void makeDead()
    {
        isAlive = false;
        GetComponent<Enemy_Controller>().enabled = false;
        audioSource.clip = deadSFX;
        audioSource.Play();
        audioSource.loop = true;
        animator.SetBool("isDead", true);
        Destroy(gameObject, 2);
        myNavAgent.isStopped = true;
    }
}
