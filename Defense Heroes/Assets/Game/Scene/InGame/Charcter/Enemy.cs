using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private int type;//1성기사,2전사,3마법사 이런식 타입에 따라 아래 수치 바뀔것
	private float lange;
	public float hp;
	public float dmg;
	public int point;
	public int score;

	private GameObject player;
	private GameObject manager;
	// Use this for initialization
	void Start () {
		//랜덤으로 타입 정해주고
		dmg = 0.0f;
		hp = 30.0f;
		point = 10;
		score = 50;

		player = GameObject.Find ("Player");//오브젝트 찾아서 연결
		manager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void Update () {
		//정해진 타입으로 수치 설정 및 이동 로직에서 길찾기 가져다 쓸것
	}

	/***************************************************************
	 * @brief 피격시 체력, 점수, 포인트 관리 해당 클래스들에게 값 전달
	 * @param int $hp 체력
	 * @prarm int $point 포인트
	 * @param int $score 점수
	***************************************************************/
	void GetShot(){
		//맞을경우 체력 감ㅗ
		hp -= player.GetComponent <Player> ().dmg;
		manager.GetComponent<IngameUI> ().eHp = hp;

		if (hp <= 0) {
			player.GetComponent <Player> ().point += point;
			player.GetComponent <Player> ().score += score;

			Destroy (this.gameObject, 0.0f);
		}
	}
}
