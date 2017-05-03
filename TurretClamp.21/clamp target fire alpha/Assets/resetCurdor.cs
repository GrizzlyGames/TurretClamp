using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCurdor : MonoBehaviour {


	public Texture2D CursorTexture;
	CursorMode curMode = CursorMode.Auto;
	public Vector2 hotSpot =Vector2.zero;


	// Use this for initialization
	void Start () {
		Cursor.SetCursor (CursorTexture, hotSpot, curMode);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
