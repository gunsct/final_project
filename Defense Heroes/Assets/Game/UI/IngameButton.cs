using UnityEngine;
using System.Collections;

public class IngameButton : MonoBehaviour {
	public bool bShoot;
	private GameObject player;
	private GameObject shotPoint;
	private GameObject[] button;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		shotPoint = GameObject.Find ("ShootPoint");
		bShoot = false;

		button = new GameObject[3];
		button [0] = GameObject.Find ("FullButton");
		button [1] = GameObject.Find ("MeteorButton");
		button [2] = GameObject.Find ("LightningButton");

		StartCoroutine ("CheckButton");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Player> ().fullCnt > 0) {
			button [0].transform.Rotate (new Vector3 (0.0f, 0.0f, 540.0f) * Time.deltaTime, Space.World);
		} else {
			button [0].transform.rotation = Quaternion.identity;
		}

		if (player.GetComponent<Player> ().coolMeteor >= player.GetComponent<Player> ().maxMeteor) {
			button [1].transform.Rotate (new Vector3 (0.0f, 0.0f, 540.0f) * Time.deltaTime, Space.World);
		} else {
			button [1].transform.rotation = Quaternion.identity;
		}

		if (player.GetComponent<Player> ().coolLightning >= player.GetComponent<Player> ().maxLightning) {
			button [2].transform.Rotate (new Vector3 (0.0f, 0.0f, 540.0f) * Time.deltaTime, Space.World);
		}else {
			button [2].transform.rotation = Quaternion.identity;
		}
	}

	/*********************************************
	 * @brief 메뉴버튼을 눌렀을때 돌아가기, 계속하기
	 * @param
	*********************************************/
	void MenuButton(){
		//백버튼
		Application.LoadLevel ("Stage");
	}

	/*********************************************
	 * @brief 공격 버튼 누를시 상태 전환
	 * @param bool $bShoot 버튼 상태
	*********************************************/
	void ShootButtonOn(){//버튼누르고있으면 발사
		if(player.GetComponent<Player>().die == false)
		bShoot = true;

	}

	void ShootButtonOff(){//버튼떼면 중지
		bShoot = false;
	}

	void PowerShot(){
		if (player.GetComponent<Player> ().die == false)
			shotPoint.SendMessage ("PowerShot");
	}

	void Meteor(){
		if(player.GetComponent<Player>().die == false)
			shotPoint.SendMessage ("Meteor");
	}

	void Lightning(){
		if(player.GetComponent<Player>().die == false)
			shotPoint.SendMessage ("Lightning");
	}

	IEnumerator CheckButton(){
		if (player.GetComponent<Player> ().fullCnt > 0) {
			button [0].transform.Rotate (new Vector3 (0.0f, 0.0f, 5.0f) * Time.deltaTime, Space.World);
		} else {
		}

		if (player.GetComponent<Player> ().coolMeteor >= player.GetComponent<Player> ().maxMeteor) {
			button [1].transform.Rotate (new Vector3 (0.0f, 0.0f, 5.0f) * Time.deltaTime, Space.World);
		} else {
		}

		if (player.GetComponent<Player> ().coolLightning >= player.GetComponent<Player> ().maxLightning) {
			button [2].transform.Rotate (new Vector3 (0.0f, 0.0f, 5.0f) * Time.deltaTime, Space.World);
		} else {
		}
		yield return new WaitForSeconds (0.1f);
	}
}
