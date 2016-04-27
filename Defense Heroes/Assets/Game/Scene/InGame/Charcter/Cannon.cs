using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Cannon : MonoBehaviour {
	private float hp;
	private float atkspd;

	public GameObject particle;
	public GameObject stone;

	public AudioClip aAttack, aDie;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		this.transform.LookAt(GameObject.Find("MainTower(Clone)").transform.position);

		atkspd = 10.0f;
		hp = 60.0f;
		audio = GetComponent<AudioSource>();

		StartCoroutine ("AutoAttack");
	}
	
	// Update is called once per frame
	void Update () {
		if (hp <= 0.0f) {
			audio.PlayOneShot (aDie, 0.4f);
			Instantiate (particle, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}

	public void GetShot(float _dmg){
		//맞을경우 체력 감ㅗ
		hp -= _dmg;
	}

	IEnumerator AutoAttack(){
		audio.PlayOneShot (aAttack, 0.2f);
		Instantiate (stone, new Vector3(this.transform.position.x, this.transform.position.y+2.0f, this.transform.position.z - 1.0f), Quaternion.identity);

		yield return new WaitForSeconds (atkspd);
		StartCoroutine ("AutoAttack");
	}
}
