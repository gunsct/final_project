using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float hp, maxHp;
	public float mp, maxMp, reMp;
	public float interval, refairTime;
	public float speed;
	public float dmg;
	public float fullDmg;
	public float splash;
	public int point;
	public int score;

	public GameObject shootButton;//버튼 변수 가져오려고


	// Use this for initialization
	void Start () {
		hp = 100.0f;
		mp = 0.0f;
		maxMp = 100.0f;
		reMp = 1.0f;
		interval = 0.01f;
		speed = 0.1f;
		dmg = 1.0f;
		fullDmg = 0.0f;
		splash = 0.0f;
		point = score = 0;

		shootButton = GameObject.Find ("Button");
		StartCoroutine ("ManageMp");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void MpState(){//발사상태면 마나 까이고 아니면 충전
		if (shootButton.GetComponent<IngameButton> ().bShoot == true) {
			interval = speed;
			if (mp > 0.0f) {
				mp -= 1.0f;
			}
		} 

		else {
			interval = refairTime;
			if (mp < maxMp) {
				mp += reMp;
			}
		}
	}

	void HpState(){
		}

	IEnumerator ManageMp(){//
		MpState ();//타격받은 대상 처리
		yield return new WaitForSeconds (interval);//
		StartCoroutine ("ManageMp");
	}
}
