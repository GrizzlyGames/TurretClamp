using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

	public float duration;	//lifeTime of object

	void Start () {
        Destroy(gameObject, duration);      //destroy object
    }
    
}
