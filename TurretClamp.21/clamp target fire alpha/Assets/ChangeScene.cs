using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public bool quitBool ;

	void Start () {
		quitBool = false;


	}
	void Update () {
		if (quitBool == true) {
			Application.Quit ();
			quitBool = false;
		} else {
			quitBool = false;
		}



	}

	public void ChangeToScene (int ChangingScene){
			SceneManager.LoadScene (ChangingScene);
	}
	public void Quitter(){
		quitBool = true;
	}
}
