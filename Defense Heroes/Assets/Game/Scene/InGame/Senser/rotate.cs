using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	public GameObject aim;
	public GameObject gyro;
	private float turnSpeed;
	// Use this for initialization
	void Start () {
		turnSpeed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {//메인카메라 레이캐스팅된 위치와 헤드 위치차 고려해서 따라회전하는 방식
		Quaternion toRotation = Quaternion.LookRotation( aim.GetComponent<ShootLaser>().aimPos - this.transform.position);
		transform.rotation = Quaternion.Slerp( transform.rotation, toRotation, turnSpeed * Time.fixedDeltaTime );

		//각도 차가 5도 이하면 따라오는 속도 늘림
		float angle = Vector3.Angle(aim.GetComponent<ShootLaser>().aimPos - this.transform.position, this.transform.forward);

		if (angle <= 5.0f) {
			turnSpeed = 5.0f;
		}
	}
}
