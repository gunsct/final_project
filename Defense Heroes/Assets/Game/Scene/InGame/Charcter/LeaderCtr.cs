using UnityEngine;
using System.Collections;

public class LeaderCtr : MonoBehaviour {
	public int flockCount;
	// Use this for initialization
	void Start () {
		flockCount = transform.GetComponentsInChildren<UnityFlock>().Length;
	}
	
	// Update is called once per frame
	void Update () {
		//무리의 수가 0이되면 리더와 무리 삭제
		if (flockCount == 0) {
			foreach (Transform child in transform) {
				Destroy (child.gameObject);
			}
			Destroy (this.gameObject);
		}
	}
}
