using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {
	//사운드도 추가하고 바꾸는건 상속받아 하자
	public float lange = 6.0f;
	private float maxHp;
	public float hp;
	public float dmg;
	public float atkSpd;
	public int point;
	public int score;
	public bool attack = false;

	public bool bDuration = false;
	private int durationCnt = 0;

	private float dieTimer = 0.0f;
	private GameObject parent;
	private GameObject player;
	private GameObject manager;

	public GameObject particle, particleBurn;

	public AudioClip aAttack, aDie, aCry;
	AudioSource audio;
	private int cryTimer = 0;

	// Use this for initialization
	void Start () {
		lange = 6.0f;
		maxHp = 50;
		parent = this.transform.parent.gameObject;
		player = GameObject.Find ("Player");//오브젝트 찾아서 연결
		manager = GameObject.Find ("GameManager");
		//particle = GameObject.Find ("Explosion");
		audio = GetComponent<AudioSource>();

		StartCoroutine ("AutoAttack");
		StartCoroutine ("DurationDmg");
	}

	// Update is called once per frame
	void Update () {
		dieTimer += Time.deltaTime;
		//정해진 타입으로 수치 설정 및 이동 로직에서 길찾기 가져다 쓸것
		if(dieTimer < 145 && !parent){//60초(도착) 이전에 리더 없어지면 다사라짐  
			Destroy (this.gameObject);
		}
		if (dieTimer > 149 && hp <= 0) {
			Destroy (this.gameObject);
		}

		if (bDuration == true) {
			particleBurn.SetActive (true);
		}
		if (bDuration == false) {
			particleBurn.SetActive (false);
		}

		if (hp <= 0) {
			player.GetComponent <Player> ().point += point;
			player.GetComponent <Player> ().score += score;

			//오브젝트 끄고 무리 수량 감소 트릭을 잘 생각하자 충돌없어지진 않고 체력==0하면 되려나

			//안보이게 한뒤 충돌 레이어 바꿈
			//this.gameObject.GetComponent<MeshRenderer>().enabled = false;
			this.gameObject.layer = 8;

			parent.GetComponent<LeaderCtr> ().flockCount--;
			Debug.Log (parent.GetComponent<LeaderCtr> ().flockCount);

			Instantiate (particle, this.transform.position, Quaternion.identity);
			//particle.transform.position = this.transform.position;
			//particle.gameObject.SetActive (true);

			audio.PlayOneShot (aDie, 0.5f);
			this.gameObject.SetActive (false);
			//this.GetComponentInParent<LeaderCtr> ().flockCount--;
			//Destroy (this.gameObject, 0.0f);
		}
	}

	/***************************************************************
	 * @brief 피격시 체력, 점수, 포인트 관리 해당 클래스들에게 값 전달
	 * @param int $hp 체력
	 * @prarm int $point 포인트
	 * @param int $score 점수
	***************************************************************/
	public void GetShot(float _dmg){
		//맞을경우 체력 감ㅗ
		hp -= _dmg;
		manager.GetComponent<IngameUI> ().eHp = hp;
	}

	void OnDisable(){
		Destroy (this.gameObject, 60.0f);
	}

	void Distance(){
		if (Vector3.Distance (player.transform.position, this.transform.position) <= lange) {
			attack = true;
			Vector3 lookPlayer = Quaternion.LookRotation(player.transform.position-this.transform.position).eulerAngles;
			lookPlayer.x = 0;
			lookPlayer.z = 0;
			this.transform.rotation = Quaternion.Euler (lookPlayer);
		} else
			attack = false;
	}

	IEnumerator AutoAttack(){
		cryTimer++;
		Distance ();
		if (attack) {
			player.GetComponent <Player> ().hp -= dmg;
			player.GetComponent <Player> ().bShake = true;
			audio.PlayOneShot (aAttack, 0.1f);
			yield return new WaitForSeconds (atkSpd);
		}

		if (!attack && cryTimer == Random.Range (3, 5)) {
			audio.PlayOneShot (aCry, 0.1f);
			cryTimer = 0;
		}

		yield return new WaitForSeconds (atkSpd);
		StartCoroutine ("AutoAttack");
	}

	IEnumerator DurationDmg(){
		if (bDuration == true) {
			hp -= player.GetComponent<Player> ().durationDmg;
			if (durationCnt >= 10) {
				durationCnt = 0;
				bDuration = false;
			}
		
			durationCnt++;
		}
		yield return new WaitForSeconds (1.0f);
		StartCoroutine ("DurationDmg");
	}
}
