using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {
	float timer;
	float rimit;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
		rimit = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= rimit) {
			Destroy (this.gameObject);
			//this.gameObject.SetActive (false);
			//timer = 0.0f;
		}
	}
}
