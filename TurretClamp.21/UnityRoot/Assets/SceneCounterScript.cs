using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneCounterScript : MonoBehaviour {

	GameObject thePlayer;
	Player_Health thePlayerHealth;
	public GameObject[] enemyCounterArray;
	public Text enemyCounterText;
	public float finalenemyCount;

	public Transform pauseCanvas;
	public AudioClip pauseAudio;

	public Texture2D CursorTexture;
	public Texture2D NoPointTexture;
	CursorMode curMode = CursorMode.Auto;
	public Vector2 hotSpot =Vector2.zero;



	// Use this for initialization
	void Start () {
		
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		thePlayerHealth = thePlayer.GetComponent<Player_Health > ();
		Time.timeScale = 1;

	}
	
	// Update is called once per frame
	void Update () {


		Ray camtoNavRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;


		if (Physics.Raycast (camtoNavRay, out hit, 10)) {

			if (hit.collider.tag != "Blocked") {
				Cursor.SetCursor (CursorTexture, hotSpot, curMode);

			} else {
				
				Cursor.SetCursor (NoPointTexture, hotSpot, curMode);
			}
		}




		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			AudioSource.PlayClipAtPoint (pauseAudio, transform.position, 1f);
			pauseGame ();
		}
		enemyCounterArray = GameObject.FindGameObjectsWithTag("Enemy");
		finalenemyCount = enemyCounterArray.Length - 1;
		enemyCounterText.text =finalenemyCount.ToString ("00");


		if (finalenemyCount <=0){
			SceneManager.LoadScene (2);
		}
	}
	public void ChangeToScene (int ChangingScene){
		SceneManager.LoadScene (ChangingScene);
	}

	public void pauseGame(){
		if (pauseCanvas.gameObject.activeInHierarchy == false) {
			pauseCanvas.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
			pauseCanvas.gameObject.SetActive (false);
		}
	}
}
