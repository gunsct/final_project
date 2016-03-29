using UnityEngine;
using System.Collections;

public class IngameUI : MonoBehaviour {
	public float sec = 0.0f;
	private int waveTime;
	private string tScore, tPoint, tEHp;
	public float eHp = 0.0f;

	private GameObject player;
	private GameObject hpBar;
	private GameObject shotBar;
	private GameObject timeBar;
	private GameObject score;
	private GameObject point;
	private GameObject enemyHp;
	private GameObject shootPoint;

	// Use this for initialization
	void Start () {
		tScore = tPoint = "";
		player = GameObject.Find ("Player");
		shotBar = GameObject.Find ("Shot Bar");
		hpBar = GameObject.Find ("Hp Bar");
		timeBar = GameObject.Find ("Time Bar");
		score = GameObject.Find ("Score");
		point = GameObject.Find ("Point");
		enemyHp = GameObject.Find ("EnemyHp");
		shootPoint = GameObject.Find ("ShootPoint");

		//해상도 고정
		Screen.SetResolution(Screen.width, Screen.height, true);

		StartCoroutine ("Frame1");
	}
	
	// Update is called once per frame
	void Update () {
		//백버튼
		if ( Application.platform == RuntimePlatform.Android )
		{
			if( Input.GetKeyDown( KeyCode.Escape ))
			{ 
				Application.LoadLevel(0);
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
		shotBar.GetComponent<UISlider>().sliderValue =  player.GetComponent<Player>().mp / player.GetComponent<Player>().maxMp;
		timeBar.GetComponent<UISlider>().sliderValue = sec / gameObject.GetComponent<Map> ().waveTime;

		if (sec == gameObject.GetComponent<Map> ().waveTime && gameObject.GetComponent<Map> ().stageNum == 0) {
			sec = 0;
			gameObject.GetComponent<Map> ().waveTime += 30;
		}

		if (sec == gameObject.GetComponent<Map> ().waveTime && gameObject.GetComponent<Map> ().stageNum == 1) {
			sec = 0;
			gameObject.GetComponent<Map> ().waveTime += 60;
		}

		tScore = "SCORE : " + player.GetComponent<Player> ().score;
		tPoint = "POINT   : " + player.GetComponent<Player> ().point;
		tEHp = "HP:" + eHp;

		score.GetComponent<UILabel> ().text = tScore;
		point.GetComponent<UILabel> ().text = tPoint;

		if (shootPoint.GetComponent<ShootLaser> ().shotOn == true) {
			enemyHp.SetActive (true);
			enemyHp.GetComponent<UILabel> ().text = tEHp;
		}
		else 
			enemyHp.SetActive (false);

		//Debug.Log (sec +" "+ gameObject.GetComponent<Map> ().waveTime);

		yield return new WaitForSeconds (0.1f);//
		StartCoroutine ("Frame1");
	}
}
