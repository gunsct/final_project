using UnityEngine;
using System.Collections;

public class IngameUI : MonoBehaviour {
	public GameObject player;
	public GameObject hpBar;
	public GameObject shotBar;
	public GameObject timeBar;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		shotBar = GameObject.Find ("Shot Bar");
		hpBar = GameObject.Find ("Hp Bar");
		timeBar = GameObject.Find ("Time Bar");

		//해상도 고정
		Screen.SetResolution(Screen.width, Screen.height, true);

		StartCoroutine ("Frame1");
	}
	
	// Update is called once per frame
	void Update () {
		

		//백버튼
		if ( Application.platform == RuntimePlatform.Android )
		{
			if( Input.GetKeyDown( KeyCode.Escape ))
			{ 
				Application.LoadLevel(0);
			}
		}


	}

	IEnumerator Frame1(){//
		shotBar.GetComponent<UISlider>().sliderValue =  player.GetComponent<Player>().mp / player.GetComponent<Player>().maxMp;
		yield return new WaitForSeconds (0.1f);//
		StartCoroutine ("Frame1");
	}
}
