using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	//사운드도 추가하고 바꾸는건 상속받아 하자
	public float lange = 6.0f;
	public float hp;
	public float dmg;
	public float atkSpd;
	public int point;
	public int score;
	public bool attack = false;

	private GameObject parent;
	private GameObject player;
	private GameObject manager;
	// Use this for initialization
	void Start () {
		lange = 6.0f;
		parent = this.transform.parent.gameObject;
		player = GameObject.Find ("Player");//오브젝트 찾아서 연결
		manager = GameObject.Find ("GameManager");

		StartCoroutine ("AutoAttack");
	}

	// Update is called once per frame
	void Update () {
		//정해진 타입으로 수치 설정 및 이동 로직에서 길찾기 가져다 쓸것
		if (!parent) {
			Destroy (this.gameObject);
		}
	}

	/***************************************************************
	 * @brief 피격시 체력, 점수, 포인트 관리 해당 클래스들에게 값 전달
	 * @param int $hp 체력
	 * @prarm int $point 포인트
	 * @param int $score 점수
	***************************************************************/
	public void GetShot(){
		//맞을경우 체력 감ㅗ
		hp -= player.GetComponent <Player> ().dmg;
		manager.GetComponent<IngameUI> ().eHp = hp;

		if (hp == 0) {
			player.GetComponent <Player> ().point += point;
			player.GetComponent <Player> ().score += score;

			//오브젝트 끄고 무리 수량 감소 트릭을 잘 생각하자 충돌없어지진 않고 체력==0하면 되려나

			//안보이게 한뒤 충돌 레이어 바꿈
			//this.gameObject.GetComponent<MeshRenderer>().enabled = false;
			this.gameObject.layer = 8;

			parent.GetComponent<LeaderCtr> ().flockCount--;
			Debug.Log (parent.GetComponent<LeaderCtr> ().flockCount);

			this.gameObject.SetActive (false);
			//this.GetComponentInParent<LeaderCtr> ().flockCount--;
			//Destroy (this.gameObject, 0.0f);
		}
	}

	void OnDisable(){
		Destroy (this.gameObject, 60.0f);
	}

	void Distance(){
		if (Vector3.Distance (player.transform.position, this.transform.position) <= lange) {
			attack = true;
		} else
			attack = false;
	}

	IEnumerator AutoAttack(){
		Distance ();
		if (attack) {
			player.GetComponent <Player> ().hp -= dmg;
		}

		yield return new WaitForSeconds (atkSpd);
		StartCoroutine ("AutoAttack");
	}
}
