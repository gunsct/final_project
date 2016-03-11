using UnityEngine;
using System.Collections;

public class ShootLaser : MonoBehaviour {
	private LineRenderer laser;
	public Camera aimCamera;
	public GameObject shootButton;
	RaycastHit hitObj;

	// Use this for initialization
	void Start () {
		//라인 렌더러 설정
		laser = GetComponent<LineRenderer> ();
		laser.SetColors (Color.white, Color.black);
		laser.SetWidth (0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		//나중에 버튼 누를때 씀
		LaserRender();

	}


	void LaserRender(){
		if (shootButton.GetComponent<IngameButton> ().bShoot == true) {//발사
			laser.enabled = true;//레이저 보이게
			//라인 렌더러 시작,끝
			laser.SetPosition (0, transform.position);//레이저 시작점

			Ray aim = aimCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));//카메라 정면으로

			Raycasting (aim);//레이 날림
		} else {//발사 중지
			laser.enabled = false;//레이저 감춤
		}
	}

	//******************************************************************************
	//설명 : 레이캐스팅 후 레이저 가시화
	//리턴값 : 
	//매개변수 :
	//매개변수명 : 
	//2016.03.10
	//******************************************************************************
	void Raycasting(Ray _ray){
		

		if(Physics.Raycast(_ray, out hitObj, Mathf.Infinity)){
			Debug.DrawRay(_ray.origin, hitObj.point, Color.green);//가시화
			laser.SetPosition(1,hitObj.point);//레이저 맞는 부분

			StartCoroutine ("ShootSpeed");//버튼누르면 반복
		}

		else{
			StopCoroutine ("ShootSpeed");//버튼 떼면 반복 중단
			Debug.DrawRay(_ray.origin, _ray.direction *100, Color.red);//가시화
		}
	}


	//******************************************************************************
	//설명 : 레이저 맞은 대상 처리
	//리턴값 : 
	//매개변수 :
	//매개변수명 : 
	//2016.03.10
	//******************************************************************************
	void ShotPocess(RaycastHit _hitObj){
		if (_hitObj.transform.tag.Equals ("Enemy")) {
			_hitObj.transform.SendMessage ("GetShot", 0.0f);
		}

	}

	IEnumerator ShootSpeed(){//버튼 누를시 반복 처리하는곳
		ShotPocess (hitObj);//타격받은 대상 처리
		yield return new WaitForSeconds (GetComponent<Player>().speed);
		//StartCoroutine ("ShootSpeed");
	}
		
}
