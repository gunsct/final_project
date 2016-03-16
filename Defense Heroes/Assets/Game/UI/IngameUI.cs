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

		Debug.Log (sec +" "+ gameObject.GetComponent<Map> ().waveTime);

		yield return new WaitForSeconds (0.1f);//
		StartCoroutine ("Frame1");
	}
}
