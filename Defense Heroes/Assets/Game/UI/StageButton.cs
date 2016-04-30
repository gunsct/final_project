using UnityEngine;
using System.Collections;

public class StageButton : MonoBehaviour {
	public GameObject[] stageButton;
	// Use this for initialization
	void Start () {
		//stageButton = new GameObject[9];
		StartCoroutine ("UnLock");
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

	void GoStage3(){
		PlayerInfo.getInstance.SaveStage (2);
		Application.LoadLevel (1);
	}

	void GoStage4(){
		PlayerInfo.getInstance.SaveStage (3);
		Application.LoadLevel (1);
	}

	void GoStage5(){
		PlayerInfo.getInstance.SaveStage (4);
		Application.LoadLevel (1);
	}

	void GoStage6(){
		PlayerInfo.getInstance.SaveStage (5);
		Application.LoadLevel (1);
	}

	void GoStage7(){
		PlayerInfo.getInstance.SaveStage (6);
		Application.LoadLevel (1);
	}

	void GoStage8(){
		PlayerInfo.getInstance.SaveStage (7);
		Application.LoadLevel (1);
	}

	void GoStage9(){
		PlayerInfo.getInstance.SaveStage (8);
		Application.LoadLevel (1);
	}

	void Back(){
		Application.LoadLevel ("Main");
	}

	IEnumerator UnLock(){//클리어 해야 다음게 풀림, 버튼 스크립트 껏다 켰다
		for(int i = 1; i<9;i++){
			Debug.Log (PlayerInfo.getInstance.LoadCLEStage ());
			if (i - 1 > PlayerInfo.getInstance.LoadCLEStage ()) {//lock
				stageButton [i].transform.FindChild ("lock").gameObject.SetActive (true);
				stageButton [i].GetComponent<UIButton> ().enabled = false;
				stageButton [i].GetComponent<UIButtonScale> ().enabled = false;
				stageButton [i].GetComponent<UIButtonOffset> ().enabled = false;
				stageButton [i].GetComponent<UIButtonSound> ().enabled = false;
				stageButton [i].GetComponent<UIButtonMessage> ().enabled = false;
			} else {//open
				stageButton [i].transform.FindChild ("lock").gameObject.SetActive (false);
				stageButton [i].GetComponent<UIButton> ().enabled = true;
				stageButton [i].GetComponent<UIButtonScale> ().enabled = true;
				stageButton [i].GetComponent<UIButtonOffset> ().enabled = true;
				stageButton [i].GetComponent<UIButtonSound> ().enabled = true;
				stageButton [i].GetComponent<UIButtonMessage> ().enabled = true;
			}
		}
		yield return new WaitForSeconds (0.1f);
	}
}
