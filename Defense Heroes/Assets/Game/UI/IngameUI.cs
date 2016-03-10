using UnityEngine;
using System.Collections;

public class IngameUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//해상도 고정
		Screen.SetResolution(Screen.width, Screen.height, true);

		//백버튼
		if ( Application.platform == RuntimePlatform.Android )
		{
			if( Input.GetKeyDown( KeyCode.Escape ))
			{ 
				Application.LoadLevel(0);
			}
		}
	}
}
