using UnityEngine;
using System.Collections;

public class ShootLaser : MonoBehaviour {
	private LineRenderer laser;
	public Camera aimCamera;
	public GameObject shootButton;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//나중에 버튼 누를때 씀

		if(shootButton.GetComponent<IngameButton>().bShoot == true){
			//라인 렌더러 설정
			laser = GetComponent<LineRenderer>();
			laser.SetColors (Color.white ,Color.black);
			laser.SetWidth (0.5f, 0.5f);
			//라인 렌더러 시작,끝
			laser.SetPosition(0,transform.position);//시작

			Ray aim = aimCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));//카메라 정면으로

			Raycasting (aim);//레이 날림
		}
	}

	void Raycasting(Ray _ray){
		RaycastHit hitObj;

		if(Physics.Raycast(_ray, out hitObj, Mathf.Infinity)){
			Debug.DrawRay(_ray.origin, hitObj.point, Color.green);//가시화
			laser.SetPosition(1,hitObj.transform.position);//끝
		}

		else{
			Debug.DrawRay(_ray.origin, _ray.direction *100, Color.red);//가시화
		}
	}
}
