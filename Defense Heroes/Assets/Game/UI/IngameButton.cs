using UnityEngine;
using System.Collections;

public class IngameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MenuButton(){
		//백버튼
		Application.LoadLevel (0);
	}
}
