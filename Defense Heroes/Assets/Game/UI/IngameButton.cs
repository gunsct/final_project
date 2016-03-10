using UnityEngine;
using System.Collections;

public class IngameButton : MonoBehaviour {
	public bool bShoot;

	// Use this for initialization
	void Start () {
		bShoot = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MenuButton(){
		//백버튼
		Application.LoadLevel (0);
	}

	void ShootButton(){
		bShoot = true;
	}

	void OnEnable(){
		bShoot = false;
	}
}
