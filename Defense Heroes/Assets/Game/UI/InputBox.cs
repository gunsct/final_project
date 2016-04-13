using UnityEngine;
using System.Collections;

public class InputBox : MonoBehaviour {
	public GameObject tId;
	public GameObject tPassword;
	public GameObject infomanager;
	public GameObject sysmsg;
	// Use this for initialization
	void Start () {
		//PlayerInfo.getInstance.SaveBLog (0);
	}
	
	// Update is called once per frame
	void Update () {
		//로그인상태면 창 숨기고 아니면 띄우고
		if (PlayerInfo.getInstance.LoadBLog () == 1) {
			this.transform.position = new Vector3 (0.0f, 0.0f, 1000.0f);
		} 
		if(PlayerInfo.getInstance.LoadBLog () == 0){
			this.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		}
	}

	void Login(){
		PlayerInfo.getInstance.SavePacketType ("login");
		PlayerInfo.getInstance.SaveLogInfo (tId.GetComponent<UILabel> ().text, tPassword.GetComponent<UILabel> ().text);
		Debug.Log (tId.GetComponent<UILabel> ().text + " " + tPassword.GetComponent<UILabel> ().text);

		infomanager.GetComponent<Client> ().ui.InitInfo ();
		infomanager.GetComponent<Client> ().ClickLogin ();
	}

	void SignUp(){
		PlayerInfo.getInstance.SavePacketType ("signup");
		PlayerInfo.getInstance.SaveLogInfo (tId.GetComponent<UILabel> ().text, tPassword.GetComponent<UILabel> ().text);
		Debug.Log (tId.GetComponent<UILabel> ().text + " " + tPassword.GetComponent<UILabel> ().text);
		infomanager.GetComponent<Client> ().ui.InitInfo ();
		infomanager.GetComponent<Client> ().ClickSignUp ();
	}

	void LookRank(){
		if (PlayerInfo.getInstance.LoadBLog() == 1) {
			Application.LoadLevel(4);
		}
	}

	/*void UpdataRank(){
		PlayerInfo.getInstance.SavePacketType ("updatascore");

		infomanager.GetComponent<Client> ().sc.InitInfo ();
		infomanager.GetComponent<Client> ().ClickUpdataScore ();
	}*/
}
