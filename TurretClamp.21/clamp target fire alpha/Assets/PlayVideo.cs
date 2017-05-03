using System.Collections;
using UnityEngine;

public class PlayVideo : MonoBehaviour {

	[SerializeField] MovieTexture movie;
	Renderer myRenderer;


	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<Renderer> ();
		movie.Stop ();
		myRenderer.material.mainTexture = movie;
		movie.loop=true;
		movie.Play ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
