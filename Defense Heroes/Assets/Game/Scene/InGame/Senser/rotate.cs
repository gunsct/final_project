using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	public GameObject aim;
	public GameObject gyro;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {//메인카메라 레이캐스팅된 위치와 헤드 위치차 고려해서 따라회전하는 방식
		Quaternion toRotation = Quaternion.LookRotation( aim.GetComponent<ShootLaser>().aimPos - this.transform.position);
		transform.rotation = Quaternion.Slerp( transform.rotation, toRotation, 1.0f*Time.fixedDeltaTime );
	}
}
