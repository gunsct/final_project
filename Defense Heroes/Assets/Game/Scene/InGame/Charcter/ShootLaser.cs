using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ShootLaser : MonoBehaviour {
	public bool shotOn;
	private float shotSpeed;

	public RaycastHit hitObj;
	private LineRenderer laser;

	public Camera aimCamera;
	public GameObject shootButton;
	private GameObject player;
	public GameObject shootEffet;
	public GameObject shotedEffet;

	public AudioClip audioClip;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		StartCoroutine ("ShootSpeed");//버튼누르면 반복

		//라인 렌더러 설정
		laser = GetComponent<LineRenderer> ();
		laser.SetWidth (0.5f, 0.5f);

		shotOn = false;

		player = GameObject.Find ("Player");//오브젝트 찾아서 연결

		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//나중에 버튼 누를때 씀
		shotSpeed = player.GetComponent<Player>().speed;//이 스크립트가 붙어있어야가능할거 아니라면 게임오브젝트로 연결해줘야되
		LaserRender();
	}

	void LaserRender(){
		if (shootButton.GetComponent<IngameButton> ().bShoot == true && player.GetComponent<Player>().mp > 0.0f) {//mp가 있고 버튼 누르면 발사
			laser.enabled = true;//레이저 보이게

			//라인 렌더러 시작,끝
			laser.SetPosition (0, transform.position);//레이저 시작점
			Ray aim = aimCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));//카메라 정면으로

			Raycasting (aim);//레이 날림
			audio.PlayOneShot(audioClip, 0.3f);
			audio.loop = true;

			shootEffet.SetActive (true);
		} 

		else {//발사 중지
			shotOn = false;
			laser.enabled = false;//레이저 감춤
			audio.loop = false;

			shootEffet.SetActive (false);
			shotedEffet.SetActive(false);
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
			laser.SetPosition(1,hitObj.point);//레이저 맞는 부분

			shotedEffet.SetActive(true);
			shotedEffet.transform.position = hitObj.point;
			shotOn = true;
		}

		else{
			shotedEffet.SetActive(false);
			shotOn = false;
		}
	}


	//******************************************************************************
	//설명 : 레이저 맞은 대상 처리
	//리턴값 : 
	//매개변수 :
	//매개변수명 : 
	//2016.03.10
	//******************************************************************************
	void ShotPocess(RaycastHit _hitObj){//
		if (_hitObj.transform.tag.Equals ("Enemy")) {
			_hitObj.transform.SendMessage ("GetShot", 0.0f);
		}
	}

	IEnumerator ShootSpeed(){//버튼 누를시 반복 처리하는곳
		if (shootButton.GetComponent<IngameButton> ().bShoot == true) {
			laser.SetColors (new Color (Random.value, Random.value, Random.value, 1.0f), 
				new Color (Random.value, Random.value, Random.value, 1.0f));
			laser.SetWidth (Random.Range(0.2f,0.8f), Random.Range(0.2f,0.8f));
		}

		if(shotOn == true)
			ShotPocess (hitObj);//타격받은 대상 처리
		
		yield return new WaitForSeconds (shotSpeed);//
		StartCoroutine ("ShootSpeed");
	}
		
}
