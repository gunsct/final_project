using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float hp;
	public float dmg;
	public float fullDmg;
	public float splash;
	public float speed;
	// Use this for initialization
	void Start () {
		hp = 0.0f;
		dmg = 1.0f;
		fullDmg = 0.0f;
		splash = 0.0f;
		speed = 0.1f;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
