using UnityEngine;
using System.Collections;
using LitJson;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class pUserInfo{
	public string type{ get; set; }
	public string id{ get; set;}
	public string password{ get; set;}

	public void InitInfo ()
	{//join,login
		type = PlayerInfo.getInstance.LoadPacketType();
		id = PlayerInfo.getInstance.LoadId();
		password = PlayerInfo.getInstance.LoadPassword();
	}
}

public class pScore{
	public string type{ get; set; }
	public string id{ get; set;}
	public int score{ get; set;}

	public void InitInfo ()
	{//lookscore, updatascore
		type = PlayerInfo.getInstance.LoadPacketType();;
		id = PlayerInfo.getInstance.LoadId();
		score = PlayerInfo.getInstance.LoadScore();
	}
}

public class Client : MonoBehaviour {
	private static Client instance;

	public GameObject resultText;
	public pUserInfo ui;
	public pScore sc;
	private string packet;
	// Use this for initialization
	void Start () {
		resultText = GameObject.Find ("SystemMsg"); 
		ui = new pUserInfo ();
		sc = new pScore ();
		//PlayerInfo.getInstance.SaveBLog (0);
	}

	// Update is called once per frame
	void Update () {
	}

	public static Client getInstance
	{
		get{
			if(instance == null){
				instance = new Client ();
			}
			return instance;
		}
	}

	public void ClickLogin() {
		packet = JsonMapper.ToJson(ui);
		Debug.Log (packet);
		StartCoroutine ( DatabaseInsert () );
	}
	//LookScore
	public void ClickSignUp(){
		packet = JsonMapper.ToJson (ui);
		Debug.Log (packet);
		StartCoroutine (DatabaseInsert ());
	}

	public void ClickLookRank(){
		packet = JsonMapper.ToJson (sc);
		Debug.Log (packet);
		StartCoroutine (DatabaseInsert ());
	}
	public void ClickUpdataScore(){
		packet = JsonMapper.ToJson (sc);
		Debug.Log (packet);
		StartCoroutine (DatabaseInsert ());
	}

	// INSERT(HTTP)
	IEnumerator DatabaseInsert(){
		string url = "http://gunsct.cafe24.com/Rank.php";
		// FORM 객체 생성- POST
		WWWForm sendData = new WWWForm ();
		// POST 파라미터 설정
		sendData.AddField ("packet", packet);
		// 통신객체 생성
		WWW webSite = new WWW (url, sendData);
		// 대기
		yield return webSite;       // 웹사이트에서 응답이 다운로드될 때까지 기다림
		// 에러처리
		if (webSite.error == null) {
			resultText.GetComponent<UILabel>().text = webSite.text;
			if(resultText.GetComponent<UILabel>().text.Contains("환영")){
				StartCoroutine ("Delay");
			}
		} else {
			resultText.GetComponent<UILabel>().text = webSite.error;
		}
	}

	IEnumerator Delay(){
		yield return new WaitForSeconds (1.0f);
		PlayerInfo.getInstance.SaveBLog (1);
		Debug.Log (PlayerInfo.getInstance.LoadBLog());
		resultText.GetComponent<UILabel> ().text = "";
	}
	public int m_data{ set; get; }
}
