﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player_Manager))]
public class Player_Weapon : MonoBehaviour
{
    public Transform weaponTransform;

    private int weaponIndex = 0;
    public GameObject[] weaponsGO;
    public Animator[] weaponAnimator;
    private Player_Manager player;

    private void Start()
    {
        player = GetComponent<Player_Manager>();
        ChangeWeapon(0);
    }
    private void Update()
    {
        Aim();
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
		if (other.transform.name == "Gun-Single")
		{
			ChangeWeapon(0);
			Destroy(other.gameObject);
		}
        if (other.transform.name == "Gun-Double")
        {
            ChangeWeapon(1);
            Destroy(other.gameObject);
        }

        else if (other.transform.name == "Gun-Plasma")
        {
            ChangeWeapon(2);
            Destroy(other.gameObject);
        }
    }
    private void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            weaponTransform.LookAt(hit.point);
        }
    }

    public GameObject BulletPrefab;
    public Transform bulletSpawnLeft;
    public Transform bulletSpawnCenter;
    public Transform bulletSpawnRight;
    public float bulletSpeed = 6f;    //speed of bullet

    public AudioClip shotSFX;
    public AudioSource audioSource;

    private void Shoot()
    {
        weaponAnimator[weaponIndex].SetTrigger("shoot");
        audioSource.clip = shotSFX;
        audioSource.Play();

        switch (weaponIndex)
        {
            case 0:
                GameObject bullet1 = Instantiate(BulletPrefab, bulletSpawnCenter.position, bulletSpawnCenter.rotation) as GameObject;
                if (player.isDug)
                    bullet1.GetComponent<bulletProperties>().speed = 4;
                else
                    bullet1.GetComponent<bulletProperties>().speed = 2;
                break;
            case 1:
                GameObject bullet2 = Instantiate(BulletPrefab, bulletSpawnLeft.position, bulletSpawnCenter.rotation) as GameObject;
                GameObject bullet3 = Instantiate(BulletPrefab, bulletSpawnRight.position, bulletSpawnCenter.rotation) as GameObject;
                if (player.isDug)
                {
                    bullet2.GetComponent<bulletProperties>().speed = 4;
                    bullet3.GetComponent<bulletProperties>().speed = 4;
                }
                else
                {
                    bullet2.GetComponent<bulletProperties>().speed = 2;
                    bullet3.GetComponent<bulletProperties>().speed = 2;
                }
                break;
            case 2:
                GameObject bullet4 = Instantiate(BulletPrefab, bulletSpawnCenter.position, bulletSpawnCenter.rotation) as GameObject;
                bullet4.GetComponent<bulletProperties>().speed = 6;
                break;
        }
    }
    private void ChangeWeapon(int index)
    {
        weaponIndex = index;
        for (int i = 0; i < weaponsGO.Length; i++)
        {
            if (i != weaponIndex)
                weaponsGO[i].SetActive(false);
            else
                weaponsGO[i].SetActive(true);
        }
    }
}
