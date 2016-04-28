using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Stone : MonoBehaviour {
	public float hp;
	public float dmg;
	public int cutCnt;
	private bool die = false;

	private Vector3 startPos;
	private Vector3 endPos;

	private float timer;
	private float speed;
	private float vY;
	private float distanceY;
	private float angle;
	public float mAngle;
	public GameObject particle = null;
	private GameObject player;

	public AudioClip aAttack = null;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		startPos = this.transform.position;

		endPos = GameObject.Find ("MainTower(Clone)").transform.position;
		endPos = new Vector3 (endPos.x, endPos.y + 5.0f, endPos.z);

		hp = 10.0f;
		dmg = 10.0f;
		timer = 0.0f;
		speed = 400.0f;
		vY = (startPos - endPos).y *(100.0f / speed);
		distanceY = (startPos - endPos).y;
		angle = Random.Range (20, 30);

		audio = GetComponent<AudioSource>();
		//StartCoroutine ("Move");
	}
	
	// Update is called once per frame
	void Update () {
		timer += 0.01f;
		float ratio = timer / (distanceY / vY);
		float dh = (mAngle * ratio * (1.0f - ratio));

		if (timer >= (distanceY / vY) / 2.0f)
			dh = -dh;
		//캐논->타워 포물선 이동 및 자동 회전
		this.transform.Translate ((endPos - startPos).x / speed,(endPos - startPos).y / speed, (endPos - startPos).z / speed);
		this.transform.position = new Vector3(transform.position.x, transform.position.y + dh, transform.position.z);
		//this.transform.Rotate (new Vector3 (0.0f, 0.0f, az) * Time.deltaTime);
		this.transform.FindChild("rock").Rotate (new Vector3 (angle, angle, angle) * Time.deltaTime);

		if (hp <= 0.0f && die == false) {
			die = true;
			audio.PlayOneShot (aAttack, 0.5f);
			Instantiate (particle, this.transform.position, Quaternion.identity);
		}

	}

	public void GetShot(float _dmg){
		//맞을경우 체력 감ㅗ
		hp -= _dmg;
		GameObject.Find("GameManager").GetComponent<IngameUI> ().eHp = hp;
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.tag.Equals("Player")){
			player.GetComponent <Player> ().hp -= dmg;
			player.GetComponent <Player> ().bShake = true;

			Destroy (this.gameObject);
		}
			
	}

	IEnumerator Move(){
		if (cutCnt == 0) {
			timer += 0.01f;
			float ratio = timer / (distanceY / vY);
			float dh = (0.1f * ratio * (1.0f - ratio));

			if (timer >= (distanceY / vY) / 2.0f)
				dh = -dh;
		
			this.transform.Translate ((endPos - startPos).x / speed, (endPos - startPos).y / speed, (endPos - startPos).z / speed);
			this.transform.position = new Vector3 (transform.position.x, transform.position.y + dh, transform.position.z);
			yield return new WaitForSeconds (0.01f);
			StartCoroutine ("Move");
		} else {
			this.transform.Translate (new Vector3 (0.0f, 0.0f, 0.0f));
		}
			
	}
}
