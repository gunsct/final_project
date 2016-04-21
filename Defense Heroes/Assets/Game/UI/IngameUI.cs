using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class IngameUI : MonoBehaviour {
	public float sec = 0.0f;
	private int waveTime;
	private string tScore, tPoint, tEHp, tBallCnt;
	public float eHp = 0.0f;
	private int sceneTime;

	private GameObject mapManager;
	private GameObject player;

	private GameObject hpBar;
	private GameObject shotBar;
	private GameObject timeBar;
	private GameObject fullBar;
	private GameObject meteorBar;
	private GameObject lightningBar;

	private GameObject score;
	private GameObject point;
	private GameObject ballCnt;
	private GameObject enemyHp;

	private GameObject shootPoint;

	public Camera mainAimCamera;
	public Camera subAimCamere;
	public GameObject pointSpawn;
	public GameObject spriteParent;

	private int corucnt;

	public AudioClip Win;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		tScore = tPoint = "";
		player = GameObject.Find ("Player");
		shotBar = GameObject.Find ("Shot Bar");
		hpBar = GameObject.Find ("HP Bar");
		timeBar = GameObject.Find ("Time Bar");
		fullBar = GameObject.Find ("Full Bar");
		meteorBar = GameObject.Find("Meteor Bar");
		lightningBar = GameObject.Find("Lightning Bar");

		score = GameObject.Find ("Score");
		point = GameObject.Find ("Point");
		ballCnt = GameObject.Find ("FullBallCnt");
		enemyHp = GameObject.Find ("EnemyHp");


		shootPoint = GameObject.Find ("ShootPoint");
		mapManager = GameObject.Find ("MapManager");
		sceneTime = 0;

		corucnt = 0;
		//해상도 고정
		Screen.SetResolution(Screen.width, Screen.height, true);

		audio = GetComponent<AudioSource>();

		StartCoroutine ("Frame1");
	}
	
	// Update is called once per frame
	void Update () {
		//백버튼
		if ( Application.platform == RuntimePlatform.Android )
		{
			if( Input.GetKeyDown( KeyCode.Escape ))
			{ 
				Application.LoadLevel (0);
			}
		}

	}
		
	/***************************************************************
	 * @brief 0.1초 간격으로 플레이중에 나오는 UI들을 관리, 랜더링해줌
	 * @param Gameobject $shotBar 마나 게이지
	 * @param Gameobject $timeBar 타임바
	 * @param Gameobject $score	스코어
	 * @param Gameobject $point 포인트
	 * @param Gameobject $enemyHp 적 체력 표시
	 * @param float $sec 타이머용
	 * @param float $waveTime 타이머용 웨이브 타임
	 * @param int $eHp 적 체력, 적 클래스에서 피격시 바뀜
	 * @param Sting $tScore 스코어 텍스트
	 * @param Sting $tPoint 포인트 텍스트
	 * @param Sting $tEHp 적 체력 텍스트
	***************************************************************/ 
	IEnumerator Frame1(){//
		sec += 0.1f;
		hpBar.GetComponent<UISlider>().sliderValue = player.GetComponent<Player>().hp / player.GetComponent<Player>().maxHp;
		shotBar.GetComponent<UISlider>().sliderValue = player.GetComponent<Player>().mp / player.GetComponent<Player>().maxMp;
		timeBar.GetComponent<UISlider>().sliderValue = sec / mapManager.GetComponent<Map> ().waveTime;
		fullBar.GetComponent<UISlider> ().sliderValue = player.GetComponent<Player> ().coolFull / player.GetComponent<Player> ().maxFull;
		meteorBar.GetComponent<UISlider> ().sliderValue = player.GetComponent<Player> ().coolMeteor / player.GetComponent<Player> ().maxMeteor;
		lightningBar.GetComponent<UISlider> ().sliderValue = player.GetComponent<Player> ().coolLightning / player.GetComponent<Player> ().maxLightning;


		/*if (sec == mapManager.GetComponent<Map> ().waveTime && mapManager.GetComponent<Map> ().stageNum == 0) {
			sec = 0;
			mapManager.GetComponent<Map> ().waveTime += 30;
		}

		if (sec == mapManager.GetComponent<Map> ().waveTime && mapManager.GetComponent<Map> ().stageNum == 1) {
			sec = 0;
			mapManager.GetComponent<Map> ().waveTime += 60;
		}*/

		tScore = "SCORE : " + player.GetComponent<Player> ().score;
		tPoint = "POINT   : " + player.GetComponent<Player> ().point;
		tEHp = "HP:" + eHp;
		tBallCnt = "" + player.GetComponent<Player>().fullCnt;

		score.GetComponent<UILabel> ().text = tScore;
		point.GetComponent<UILabel> ().text = tPoint;
		ballCnt.GetComponent<UILabel> ().text = tBallCnt;

		if (shootPoint.GetComponent<ShootLaser> ().shotOn == true) {
			enemyHp.SetActive (true);
			enemyHp.GetComponent<UILabel> ().text = tEHp;
		} else {
			enemyHp.SetActive (false);
			enemyHp.GetComponent<UILabel> ().text = "";
		}

		if (player.GetComponent<Player> ().hp <= 0.0f) {
			sceneTime++;
		}

		if(sec >= 330.0f){
			sceneTime++;
			if (corucnt == 0) {
				audio.PlayOneShot (Win, 1.0f);
				if(PlayerInfo.getInstance.LoadCLEStage() < PlayerInfo.getInstance.LoadStage())
					PlayerInfo.getInstance.SaveClearStage (PlayerInfo.getInstance.LoadCLEStage ());
				PlayerInfo.getInstance.SaveScorePoint (player.GetComponent<Player> ().score, player.GetComponent<Player> ().point);
			}
			corucnt++;
		}
		if (sceneTime == 30) {
			Application.LoadLevel (2);
		}
		//Debug.Log (sec +" "+ gameObject.GetComponent<Map> ().waveTime);
		yield return new WaitForSeconds (0.1f);//
		StartCoroutine ("Frame1");
	}

	public void PointSpawn(GameObject _spawn){
		GameObject iPointSpawn = (GameObject)Instantiate (pointSpawn, new Vector3 (1.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject;
		iPointSpawn.GetComponent<MoveWarning> ().init (_spawn, mainAimCamera, subAimCamere);
		iPointSpawn.transform.parent = spriteParent.transform;
	}
}
