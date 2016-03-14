using UnityEngine;
using System.Collections;

public class Gyroscpe : MonoBehaviour {
	private Gyroscope gyro;

	// Use this for initialization
	void Start () {
		//자이로 켜줌
		gyro = Input.gyro;
		gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		//10초에 1번 돔
		Invoke("gyroupdate",0.1f);
	}

	//아래 세팅은 카메라 본체가 회전하면서 관찰하는 것으로 세팅할 것임.
	void gyroupdate(){
		// 기본 자이로는 오브젝트 관찰 기준이라 카메라 기준으로 변경
		//쿼터니언 하나 만들고
		Quaternion transquat = Quaternion.identity; 
		transquat.w = gyro.attitude.w;

		//x, y축의 값을 반대로 뒤집음
		transquat.x = -gyro.attitude.x; 
		transquat.y = -gyro.attitude.y; 
		transquat.z = gyro.attitude.z;

		// 변경된 쿼터니언을 안드로이드 자이로 기본 축 수정과 함께 카메라에 적용. 스크립트는 카메라에.
		transform.rotation = Quaternion.Euler (90, 0, 0) * transquat;

		//각도제한 그런데 자이로가 90-360-270-360 구조로 되어있음
		if (90.0f > transform.localEulerAngles.x && transform.localEulerAngles.x > 60.0f)
			transform.eulerAngles = new Vector3 (60.0f, transform.localEulerAngles.y, transform.localEulerAngles.z);

		if (270.0f < transform.localEulerAngles.x && transform.localEulerAngles.x < 240.0f)
			transform.eulerAngles = new Vector3 (240.0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}
}