using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PowerBall : MonoBehaviour {
	private float dieTimer;

	public Vector3 startPos;
	public Vector3 endPos;

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
		this.transform.Translate ((endPos - startPos) / 50.0f);
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
