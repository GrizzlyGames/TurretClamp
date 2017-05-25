using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour {

    public Transform weaponTransform;
    public GameObject[] weaponsGO;

    private void Start()
    {
        ChangeWeapon(0);
    }
    private void Update()
    {
        AimWeapon();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "BaseDouble")
        {
            ChangeWeapon(1);
            Destroy(other.gameObject);
        }
            
        else if (other.transform.name == "BasePlasma")
        {
            ChangeWeapon(2);
            Destroy(other.gameObject);
        }            
    }
    private void AimWeapon()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            if (!hit.collider.CompareTag("Blocked"))
            {
                weaponTransform.LookAt(hit.point);
            }
        }
    }
    private void ChangeWeapon(int index)
    {
        for (int i = 0; i < weaponsGO.Length; i++)
        {
            if (i != index)
                weaponsGO[i].SetActive(false);
            else
                weaponsGO[i].SetActive(true);
        }
    } 
}
