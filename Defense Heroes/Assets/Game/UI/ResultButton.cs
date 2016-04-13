using UnityEngine;
using System.Collections;

public class ResultButton : MonoBehaviour {
	// Use this for initialization
	public GameObject ma;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void MainButton(){
		Application.LoadLevel (0);
	}
	void RestartButton(){
		Application.LoadLevel (3);
	}

	void UpdataButton(){
		ma.GetComponent<PlayerInfo>().SavePacketType ("updatascore");
		ma.GetComponent<Client>().sc.InitInfo ();
		ma.GetComponent<Client>().ClickUpdataScore ();
	}
}
