using UnityEngine;
using System.Collections;

public class MainButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitButton(){
		Application.Quit ();
	}

	public void PlayButton(){
		Application.LoadLevel(1);
	}

	public void UpgradeButton(){
		Debug.Log("버튼을 터치 이벤트!!");
	}

	public void RankButton(){
		Debug.Log("버튼을 터치 이벤트!!");
	}
}
