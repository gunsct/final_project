using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {
	public enum sType{POWER, METEOR, LIGHTNING};

	public float hp, maxHp;
	public float mp, maxMp, reMp;
	public float interval, refillTime;
	public float speed;
	public float dmg;

	public float maxFull, maxMeteor, maxLightning;
	public float coolFull, coolMeteor, coolLightning;
	public float fullDmg;
	public int fullCnt;
	public float splash;
	public float meteorDmg;
	public float durationDmg;
	public float lightingDmg;
	public int point;
	public int score;

	public int corucnt;
	public bool die;

	public GameObject head;
	public bool bShake;
	public GameObject shootButton;//버튼 변수 가져오려고

	public AudioClip Lose;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		maxHp = PlayerInfo.getInstance.LoadMaxHp ();
		hp = maxHp;
		mp = 0.0f;
		maxMp = PlayerInfo.getInstance.LoadMaxMp ();
		reMp = PlayerInfo.getInstance.LoadReMp ();
		interval = 0.01f;
		speed = PlayerInfo.getInstance.LoadSpeed ();
		dmg = PlayerInfo.getInstance.LoadDMG ();

		maxFull = 5.0f;
		maxMeteor = 40.0f;
		maxLightning= 20.0f;
		coolFull = coolMeteor = coolLightning = 0.0f;
		fullCnt = 0;

		fullDmg = PlayerInfo.getInstance.LoadFullDMG ();
		meteorDmg = PlayerInfo.getInstance.LoadMeteorDmg ();
		lightingDmg = PlayerInfo.getInstance.LoadLightingDmg ();
		durationDmg = PlayerInfo.getInstance.LoadDurationDmg ();

		splash = PlayerInfo.getInstance.LoadSplash ();
		point = PlayerInfo.getInstance.LoadPoint ();
		score = 0;//playerpref으 교체

		corucnt = 0;
		die = false;

		bShake = false;
		shootButton = GameObject.Find ("Button");

		audio = GetComponent<AudioSource>();

		StartCoroutine ("ManageMp");
		StartCoroutine ("ManageCoolTime");
		StartCoroutine ("ShakeHead");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void InitInfo(){
		
	}

	/***************************************************************
	 * @brief 발사 여부에 따른 마나 감소와 충전 관리
	 * 코루틴 속도를 공격속도, 회복속도에 따라 제어
	 * @param float $interval 코루틴으로 이 함수 처리 간격
	 * @param float $mp 마나
	 * @param float $speed 공격속도
	 * @param float $refillTime 회복속도
	***************************************************************/
	void MpState(){//발사상태면 마나 까이고 아니면 충전
		if (shootButton.GetComponent<IngameButton> ().bShoot == true) {
			interval = speed;
			if (mp > 0.0f) {
				mp -= 1.0f;
			}
		} 

		else {
			interval = refillTime;
			if (mp < maxMp) {
				mp += reMp;
			}
		}
	}

	void HpState(){
		}

	void CoolTimeState(){
		if(coolFull < maxFull)
			coolFull += 0.1f;
		if (coolFull >= maxFull && fullCnt < 3) {
			fullCnt++;
			coolFull = 0.0f;
		}
		if(coolMeteor < maxMeteor)
			coolMeteor += 0.1f;
		if(coolLightning < maxLightning)
			coolLightning += 0.1f;
	}
	/***************************************************************
	 * @brief 정해진 간격에 따라 MpState() 처리
	***************************************************************/
	IEnumerator ManageMp(){//
		MpState ();//타격받은 대상 처리
		yield return new WaitForSeconds (interval);//
		StartCoroutine ("ManageMp");
	}

	IEnumerator ManageCoolTime(){
		CoolTimeState();
		yield return new WaitForSeconds (0.1f);//
		StartCoroutine ("ManageCoolTime");
	}
	
	IEnumerator ShakeHead(){
		if (bShake) {
			head.transform.position = new Vector3 (this.transform.position.x + Random.Range (-0.05f, 0.05f), this.transform.position.y + Random.Range (0.35f, 0.45f),
				this.transform.position.z + Random.Range (-0.05f, 0.05f));

			if(hp <= 0.0f){
				if (corucnt == 0) {
					audio.PlayOneShot (Lose, 1.0f);
					PlayerInfo.getInstance.SaveScorePoint (score, point);
					die = true;
				}
				corucnt++;
				//Application.LoadLevel (2);
			}
		} else {
			head.transform.position = new Vector3 (this.transform.position.x + 0.0f, this.transform.position.y + 0.4f, this.transform.position.z + 0.0f);
		}
		bShake = false;
		yield return new WaitForSeconds (0.1f);//
		StartCoroutine ("ShakeHead");
	}
			
}
