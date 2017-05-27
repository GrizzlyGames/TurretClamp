using UnityEngine;
using System.Collections;

public class bulletProperties : MonoBehaviour
{
    public int speed;
    public float damage = 1f;   //damage the bullet does
    public GameObject ricochetFX;   //ricochet effect

    private void Start()
    {
        Destroy(this.gameObject, 1);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable") || other.gameObject.tag == "PlayerBullet")
        {   //looks for shotable layer to destroy bullet
            Instantiate(ricochetFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Destroy(gameObject);
        }
    }
}





