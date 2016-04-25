using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Stone : MonoBehaviour {
	public float hp;
	public float dmg;

	private Vector3 startPos;
	private Vector3 endPos;

	private float timer;
	private float speed;
	private float vY;
	private float distanceY;

	public GameObject particle;
	private GameObject player;

	public AudioClip aAttack;
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
		speed = 300.0f;
		vY = (startPos - endPos).y *(100.0f / speed);
		distanceY = (startPos - endPos).y;

		audio = GetComponent<AudioSource>();
		StartCoroutine ("Move");
	}
	
	// Update is called once per frame
	void Update () {
		if (hp <= 0.0f) {
			audio.PlayOneShot (aAttack, 0.5f);
			Instantiate (particle, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}

	public void GetShot(float _dmg){
		//맞을경우 체력 감ㅗ
		hp -= _dmg;
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.tag.Equals("Player")){
			player.GetComponent <Player> ().hp -= dmg;
			player.GetComponent <Player> ().bShake = true;

			hp = 0.0f;
		}
			
	}

	IEnumerator Move(){
		timer += 0.01f;
		float ratio = timer / (distanceY / vY);
		float dh = (0.4f * ratio * (1.0f - ratio));

		if (timer >= (distanceY / vY) / 2.0f)
			dh = -dh;
		
		this.transform.Translate ((endPos - startPos).x / speed,(endPos - startPos).y / speed, (endPos - startPos).z / speed);
		this.transform.position = new Vector3(transform.position.x, transform.position.y + dh, transform.position.z);
		yield return new WaitForSeconds (0.01f);
		StartCoroutine ("Move");
	}
}
