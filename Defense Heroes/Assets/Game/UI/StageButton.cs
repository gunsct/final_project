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
		PlayerInfo.getInstance.SaveStage (0);
		Application.LoadLevel (1);
	}

	void GoStage2(){
		PlayerInfo.getInstance.SaveStage (1);
		Application.LoadLevel (1);
	}
	void Back(){
		Application.LoadLevel (0);
	}
}
