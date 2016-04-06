using UnityEngine;
using System.Collections;

public class Warrior : Enemy {

	// Use this for initialization
	void Start () {
		attack = this.GetComponent<Enemy> ().attack;
	}
	
	// Update is called once per frame
	void Update () {
		if(attack)
			this.GetComponent<Animation> ().CrossFade ("attack");
	}
}
