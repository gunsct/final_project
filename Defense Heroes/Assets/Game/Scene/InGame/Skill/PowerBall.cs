using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PowerBall : MonoBehaviour {
	private float dieTimer;

	public Vector3 startPos;
	public Vector3 endPos;

	private float timer;
	private float speed;
	private float vY;
	private float distanceY;

	private GameObject player;
	public GameObject particle;
	private GameObject iParticle;
	private bool bExplosion = false;

	public AudioClip aFly, aBoom;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

		dieTimer = 0.0f;
		timer = 0.0f;
		speed = 100.0f;
		vY = (startPos - endPos).y *(100 / speed);
		distanceY = (startPos - endPos).y;

		audio = GetComponent<AudioSource>();
		audio.PlayOneShot (aFly, 0.3f);


		StartCoroutine ("Move2");
	}

	// Update is called once per frame
	void Update () {
		dieTimer += Time.deltaTime;
		if (dieTimer >= 3.0f) {
			if(iParticle)
				Destroy (iParticle.gameObject);
			Destroy (this.gameObject);
		}

	}

	IEnumerator Move2(){
		timer += 0.01f;
		float ratio = timer / (distanceY / vY);
		float dh = (0.4f * ratio * (1.0f - ratio));
		if (timer >= (distanceY / vY) / 2.0f)
			dh = -dh;
		this.transform.Translate ((endPos - startPos).x / speed,(endPos - startPos).y / speed, (endPos - startPos).z / speed);
		this.transform.position = new Vector3(transform.position.x, transform.position.y + dh, transform.position.z);
		yield return new WaitForSeconds (0.01f);
		StartCoroutine ("Move2");
	}

	//on Trigger
	void OnTriggerEnter(Collider col){
		if(col.tag.Equals ("Block") && bExplosion == false){
			iParticle = (GameObject)Instantiate (particle, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) , Quaternion.identity);
			audio.PlayOneShot (aBoom, 0.2f);
			bExplosion = true;
		}

		if (col.tag.Equals ("Enemy")) {
			col.gameObject.GetComponent<Enemy> ().GetShot (player.GetComponent<Player> ().fullDmg);
		}
	}
}
