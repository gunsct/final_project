using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FlyEnemy : MonoBehaviour {
	public float maxHp = 20.0f;
	private float hp;
	public float dmg;
	public int point;
	public int score;

	private GameObject player;
	public GameObject mainTower;
	private GameObject manager;

	public GameObject particle;

	public AudioClip aAttack, aDie, aCry;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		hp = maxHp;
		//mainTower = GameObject.Find ("MainTower(Clone)");
		player = GameObject.Find ("Player");//오브젝트 찾아서 연결
		manager = GameObject.Find ("GameManager");
		//particle = GameObject.Find ("Explosion");
		audio = GetComponent<AudioSource>();

		StartCoroutine ("Cry");
	}
	
	// Update is called once per frame
	void Update () {
		DistanceSpeed ();
		if (hp <= 0) {
			player.GetComponent <Player> ().point += point;
			player.GetComponent <Player> ().score += score;

			Instantiate (particle, this.transform.position, Quaternion.identity);

			audio.PlayOneShot (aDie, 0.5f);
			Destroy (this.gameObject);
		}
	}

	public void GetShot(float _dmg){
		//맞을경우 체력 감ㅗ
		hp -= _dmg;
		manager.GetComponent<IngameUI> ().eHp = hp;
	}

	void OnTriggerEnter(Collider col){
		if (col.tag.Equals ("Player")) {
			player.GetComponent <Player> ().hp -= dmg;
			player.GetComponent <Player> ().bShake = true;
			audio.PlayOneShot (aAttack, 0.4f);
		}
	}

	void DistanceSpeed(){
		if (Vector3.Distance (mainTower.transform.position, this.transform.position) <= 14.0f) {
			this.GetComponent<VehicleFollowing> ().speed = 10.0f;
		} else
			this.GetComponent<VehicleFollowing> ().speed = 5.0f;
	}

	IEnumerator Cry(){
		audio.PlayOneShot (aCry, 0.1f);

		yield return new WaitForSeconds (8.0f);
		StartCoroutine ("Cry");
	}
}
