using UnityEngine;
using System.Collections;

public class StageButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GoStage1(){
		Application.LoadLevel (1);
	}

	void Back(){
		Application.LoadLevel (0);
	}
}
