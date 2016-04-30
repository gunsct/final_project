using UnityEngine;
using System.Collections;

public class InputBox : MonoBehaviour {
	public GameObject tId;
	public GameObject tPassword;
	public GameObject infomanager;
	public GameObject sysmsg;

	public GameObject Lid;
	public GameObject Lpass;
	public GameObject bLogin;
	public GameObject bSignUp;
	public GameObject bOk;
	public GameObject bBack;
	public GameObject bMenuButton;

	private int state = 0;
	// Use this for initialization
	void Start () {
		tId.GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadId ();
		tPassword.GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadPassword ();
	}
	
	// Update is called once per frame
	void Update () {
		//로그인상태면 창 숨기고 아니면 띄우고
		if (PlayerInfo.getInstance.LoadBLog () == 1) {
			bMenuButton.SetActive (true);
			this.transform.position = new Vector3 (0.0f, 0.0f, 1000.0f);
		} 
		if(PlayerInfo.getInstance.LoadBLog () == 0){
			bMenuButton.SetActive (false);
			this.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		}

		switch (state) {
		case 0:
			Lid.SetActive (false);
			Lpass.SetActive (false);
			bOk.SetActive (false);
			bBack.SetActive (false);

			bLogin.SetActive (true);
			bSignUp.SetActive (true);
			break;

		case 1:
			Lid.SetActive (true);
			Lpass.SetActive (true);
			bOk.SetActive (true);
			bBack.SetActive (true);

			bLogin.SetActive (false);
			bSignUp.SetActive (false);
			break;

		case 2:
			Lid.SetActive (true);
			Lpass.SetActive (true);
			bOk.SetActive (true);
			bBack.SetActive (true);

			bLogin.SetActive (false);
			bSignUp.SetActive (false);
			break;
		}
	}

	void Login(){
		state = 1;
	}

	void SignUp(){
		state = 2;
	}

	void Ok(){
		switch (state) {
		case 1:
			PlayerInfo.getInstance.SavePacketType ("login");
			PlayerInfo.getInstance.SaveLogInfo (tId.GetComponent<UILabel> ().text, tPassword.GetComponent<UILabel> ().text);
			Debug.Log (tId.GetComponent<UILabel> ().text + " " + tPassword.GetComponent<UILabel> ().text);
			infomanager.GetComponent<Client> ().ui.InitInfo ();
			infomanager.GetComponent<Client> ().ClickLogin ();
			break;
		case 2:
			PlayerInfo.getInstance.SavePacketType ("signup");
			PlayerInfo.getInstance.SaveLogInfo (tId.GetComponent<UILabel> ().text, tPassword.GetComponent<UILabel> ().text);
			Debug.Log (tId.GetComponent<UILabel> ().text + " " + tPassword.GetComponent<UILabel> ().text);
			infomanager.GetComponent<Client> ().ui.InitInfo ();
			infomanager.GetComponent<Client> ().ClickSignUp ();
			break;
		}
	}

	void Back(){
		state = 0;
	}

	void LookRank(){
		if (PlayerInfo.getInstance.LoadBLog() == 1) {
			Application.LoadLevel("Rank");
		}
	}

	/*void UpdataRank(){
		PlayerInfo.getInstance.SavePacketType ("updatascore");

		infomanager.GetComponent<Client> ().sc.InitInfo ();
		infomanager.GetComponent<Client> ().ClickUpdataScore ();
	}*/
}
