using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {

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
				Application.Quit ();
			}
		}
	}
}
//유아이는 ngui를 쓰지않을까 생각됨