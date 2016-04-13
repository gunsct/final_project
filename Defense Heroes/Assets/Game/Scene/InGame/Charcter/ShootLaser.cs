using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ShootLaser : MonoBehaviour {
	public bool shotOn;
	private float shotSpeed;

	private GameObject obj;
	public RaycastHit hitObj;
	private LineRenderer laser;
	public Vector3 hitPos;

	public Camera aimCamera;
	public GameObject shootButton;
	private GameObject player;
	public GameObject shootEffect;
	public GameObject shotedEffect;

	public GameObject powerBall;
	public GameObject meteor;
	public GameObject lightning;

	public AudioClip audioClip;
	AudioSource audio;
	private int soundCnt = 0;

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
	/*********************************************
	 * @brief 프레임마다 LaserRender()를 호출
	 * @param
	*********************************************/
	void Update () {
		//나중에 버튼 누를때 씀
		shotSpeed = player.GetComponent<Player>().speed;
		LaserRender();
	}

	/***************************************************************
	 * @brief 버튼이 on, mp가 있다면 레이저를 발사, 랜더링하고 발사,피격 
	 * 이펙트와 사운드를 관리함.
	 * @param LineRenderer $laser 라인랜더러 컴포넌트 제어용
	 * @param AudioSource $auio 발사 사운드 제어용
	 * @param GameObject $shootEffet 발사점 오라 이펙트
	 * @param GameObject $shotedEffect 피격점 이펙트
	 * @param bool $shotOn 피격 여부 제어
	***************************************************************/
	void LaserRender(){
		Ray skillAim = aimCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));//카메라 정면으로
		if (Physics.Raycast (skillAim, out hitObj, Mathf.Infinity, ((-1) - (1 << 8)))) {
			hitPos = hitObj.point;
		}

		if (shootButton.GetComponent<IngameButton> ().bShoot == true && player.GetComponent<Player>().mp > 0.0f) {//mp가 있고 버튼 누르면 발사
			laser.enabled = true;//레이저 보이게

			//라인 렌더러 시작,끝
			laser.SetPosition (0, transform.position);//레이저 시작점
			Ray aim = aimCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));//카메라 정면으로

			Raycasting (aim);//레이 날림
			//
			//audio.loop = true;

			shootEffect.SetActive (true);
		} 

		else {//발사 중지
			shotOn = false;
			laser.enabled = false;//레이저 감춤
			audio.loop = false;

			shootEffect.SetActive (false);
			shotedEffect.SetActive(false);
		}
	}

	/***************************************************************
	 * @brief LaserRender()의 연장으로 실제로 맞는 오브젝트 정보를 
	 * 가지고 있고 이펙트와 피격 상태를 제어함.
	 * @param RaycastHit $hitObj 레이저를 맞는 오브젝트
	 * ***************************************************************/
	void Raycasting(Ray _ray){
		if(Physics.Raycast(_ray, out hitObj, Mathf.Infinity,((-1) - (1 << 8)) )){
			hitPos = hitObj.point;
			laser.SetPosition(1,hitPos);//레이저 맞는 부분

			shotedEffect.SetActive(true);
			shotedEffect.transform.position = hitObj.point;
			shotOn = true;

		}

		else{
			shotedEffect.SetActive(false);	
			shotOn = false;
		}
	}
		
	/***************************************************************
	 * @brief 피격당한 대상이 적일때 적의 피격처리 함수 호출
	 * @param RaycastHit $hitObj 레이저를 맞는 오브젝트
	 ****************************************************************/
	void ShotPocess(RaycastHit _hitObj){
		if (_hitObj.transform.tag.Equals ("Enemy")) {
			_hitObj.transform.GetComponent<Enemy> ().GetShot (player.GetComponent <Player> ().dmg);
			//_hitObj.transform.SendMessage ("GetShot", 0.0f);
		}
	}


	/***************************************************************
	 * @brief 발사속도에 맞춰 이펙트 전환 및 타격대상 함수호출
	 * @param float $shotSpeed 발사속도
	 ****************************************************************/
	IEnumerator ShootSpeed(){//버튼 누를시 반복 처리하는곳
		if (shootButton.GetComponent<IngameButton> ().bShoot == true) {
			laser.SetColors (new Color (Random.value, Random.value, Random.value, 1.0f), 
				new Color (Random.value, Random.value, Random.value, 1.0f));
			laser.SetWidth (Random.Range(0.2f,0.8f), Random.Range(0.2f,0.8f));

			if (soundCnt >= 10) {
				audio.PlayOneShot (audioClip, 0.05f);
				soundCnt = 0;
			}
		}

		if(shotOn == true)
			ShotPocess (hitObj);//타격받은 대상 처리
		
		soundCnt++;
		yield return new WaitForSeconds (shotSpeed);//
		StartCoroutine ("ShootSpeed");
	}
		

	public void PowerShot(){
		if (player.GetComponent<Player> ().fullCnt > 0) {
			GameObject iFull = (GameObject)Instantiate (powerBall, this.transform.position, Quaternion.identity);
			iFull.GetComponent<PowerBall> ().startPos = iFull.transform.position;
			iFull.GetComponent<PowerBall> ().endPos = hitPos;

			player.GetComponent<Player> ().coolFull = 0.0f;
			player.GetComponent<Player> ().fullCnt--;
		}
	}

	public void Meteor(){
		if (player.GetComponent<Player> ().coolMeteor >= player.GetComponent<Player> ().maxMeteor) {
			GameObject iMeteor = (GameObject)Instantiate (meteor, new Vector3 (player.transform.position.x, player.transform.position.y + 10.0f, player.transform.position.z), Quaternion.identity);
			iMeteor.GetComponent<Meteor> ().startPos = iMeteor.transform.position;
			iMeteor.GetComponent<Meteor> ().endPos = hitPos;
			//해당 스크립트 hitPos전달
			player.GetComponent<Player> ().coolMeteor = 0.0f;
		}
	}

	public void Lightning(){
		if (player.GetComponent<Player> ().coolLightning >= player.GetComponent<Player> ().maxLightning) {
			GameObject iLightning = (GameObject)Instantiate (lightning, hitPos, Quaternion.identity);

			//해당 스크립트 hitPos전달
			player.GetComponent<Player> ().coolLightning = 0.0f;
		}
	}
}
