using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Lightning : MonoBehaviour {
	private float dieTimer;
	private float duration;
	private int objCnt;

	private GameObject player;

	public AudioClip aAttack,aDown;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

		objCnt = 0;
		dieTimer = 0.0f;
		duration = 0.0f;

		audio = GetComponent<AudioSource>();
		audio.PlayOneShot (aDown, 0.4f);
		audio.PlayOneShot (aAttack, 0.4f);
	}
	
	// Update is called once per frame
	void Update () {
		dieTimer += Time.deltaTime;
		duration += Time.deltaTime;

		if (dieTimer >= 8.0f)
			Destroy (this.gameObject);
	}

	//on Trigger
	void OnTriggerStay(Collider col){
		if (col.tag.Equals ("Enemy") && duration >= 0.5f) {
			col.gameObject.GetComponent<Enemy> ().GetShot (player.GetComponent<Player> ().lightingDmg);
			duration = 0.0f;
		}
	}
}
