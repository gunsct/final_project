using UnityEngine;
using System.Collections;

public class LeaderCtr : MonoBehaviour {
	public int flockCount;
	private float dieTimer = 0.0f;
	// Use this for initialization
	void Start () {
		flockCount = transform.GetComponentsInChildren<UnityFlock>().Length;
	}
	
	// Update is called once per frame
	void Update () {
		dieTimer += Time.deltaTime;
		//무리의 수가 0이되면 리더와 무리 삭제
		if (flockCount == 0) {
			Destroy (this.gameObject);
		}
		if(dieTimer >= 149.0f){
			Destroy (this.gameObject);
		}
	}
}
