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

public class pLogOut{//로그아웃 패킷 받으면 걍 세션 종료
	public string type{ get; set; }
	public void InitInfo(){
		type = PlayerInfo.getInstance.LoadPacketType ();
	}
}

public class pScore{//이미 로그인 되어있는 상태이므로 점수만 주ㄴ php쪽 세션정보로 처리하게 바꿀 예정 이하 로그인은 모두 같음 , 로그아웃도 만듬
	public string type{ get; set; }
	public string id{ get; set;}
	public int score{ get; set;}

	public void InitInfo ()
	{//lookscore, updatascore
		type = PlayerInfo.getInstance.LoadPacketType();
		id = PlayerInfo.getInstance.LoadId();
		score = PlayerInfo.getInstance.LoadScore();
	}
}

public class pStore{
	public string type{ get; set; }
	public string id{ get; set;}
	public string hp{ get; set;}
	public string nomal{ get; set;}
	public string powerball{ get; set;}
	public string stome{ get; set;}
	public string metear{ get; set;}
	public string point{ get; set;}

	public void InitInfo ()
	{//lookscore, updatascore
		type = PlayerInfo.getInstance.LoadPacketType();
		id = PlayerInfo.getInstance.LoadId();
		hp = PlayerInfo.getInstance.LoadHpLevel ().ToString();
		nomal = PlayerInfo.getInstance.LoadNormalLevel ().ToString();
		powerball = PlayerInfo.getInstance.LoadPowerBallLevel ().ToString();
		stome = PlayerInfo.getInstance.LoadStomeLevel ().ToString();
		metear = PlayerInfo.getInstance.LoadMetearLevel ().ToString();
		point = PlayerInfo.getInstance.LoadPoint ().ToString();
	}
}
public class Client : MonoBehaviour {
	private static Client instance;

	public GameObject resultText;
	private string[] dbStr;
	public pUserInfo ui;
	public pLogOut lo;
	public pScore sc;
	public pStore st;
	private string packet;
	// Use this for initialization
	void Start () {
		resultText = GameObject.Find ("SystemMsg"); 
		ui = new pUserInfo ();
		sc = new pScore ();
		st = new pStore ();
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

	public void ClickLogout() {
		packet = JsonMapper.ToJson(lo);
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
	public void ClickDownloadStore(){
		packet = JsonMapper.ToJson (st);
		Debug.Log (packet);
		StartCoroutine (DatabaseInsert ());
	}
	public void ClickUploadStore(){
		packet = JsonMapper.ToJson (st);
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
			if(resultText.GetComponent<UILabel>().text.Contains("download")){
				dbStr = resultText.GetComponent<UILabel> ().text.Split (' ');

				PlayerInfo.getInstance.SaveHpStore (100 +int.Parse(dbStr [1]) * 100, 100.0f + int.Parse(dbStr [1]) * 20.0f, int.Parse(dbStr [1]));
				PlayerInfo.getInstance.SaveNormalStore (200 + int.Parse(dbStr [2]) * 150, 1.5f + int.Parse(dbStr [2]) * 0.1f, int.Parse(dbStr [2]));
				PlayerInfo.getInstance.SavePowerBallStore (250 + int.Parse(dbStr [3]) * 200, 10.0f + int.Parse(dbStr [3]) * 0.5f, int.Parse(dbStr [3]));
				PlayerInfo.getInstance.SaveStomeStore (500 + int.Parse(dbStr [4]) * 400, 15.0f + int.Parse(dbStr [4]) * 1.0f, int.Parse(dbStr [4]));
				PlayerInfo.getInstance.SaveMetearStore (1000 + int.Parse(dbStr [5]) * 700, 40.0f + int.Parse(dbStr [5]) * 5.0f, int.Parse(dbStr [5]));
				PlayerInfo.getInstance.SavePoint (int.Parse(dbStr [6]));
				resultText.GetComponent<UILabel> ().text = "";
				dbStr = null;
			}
			if (resultText.GetComponent<UILabel> ().text.Contains ("없음")) {
				resultText.GetComponent<UILabel> ().text = "";
				PlayerInfo.getInstance.SaveHpStore (100, 100.0f, 0);
				PlayerInfo.getInstance.SaveNormalStore (200, 1.5f, 0);
				PlayerInfo.getInstance.SavePowerBallStore (250, 10.0f, 0);
				PlayerInfo.getInstance.SaveStomeStore (500, 15.0f, 0);
				PlayerInfo.getInstance.SaveMetearStore (1000, 40.0f, 0);
				PlayerInfo.getInstance.SavePoint (0);
			}
			if (resultText.GetComponent<UILabel> ().text.Contains ("ok")){
				Debug.Log(resultText.GetComponent<UILabel> ().text);
			}
		} else {
			resultText.GetComponent<UILabel>().text = webSite.error;
		}
	}

	IEnumerator Delay(){
		PlayerInfo.getInstance.SaveBLog (1);
		Debug.Log (PlayerInfo.getInstance.LoadBLog());
		yield return new WaitForSeconds (1.0f);
		resultText.GetComponent<UILabel> ().text = "";

		PlayerInfo.getInstance.SavePacketType ("download");
		st.InitInfo ();
		ClickDownloadStore ();
	}
	public int m_data{ set; get; }
}
