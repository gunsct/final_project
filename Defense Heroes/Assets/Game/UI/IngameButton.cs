using UnityEngine;
using System.Collections;

public class IngameButton : MonoBehaviour {
	public bool bShoot;

	// Use this for initialization
	void Start () {
		bShoot = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*********************************************
	 * @brief 메뉴버튼을 눌렀을때 돌아가기, 계속하기
	 * @param
	*********************************************/
	void MenuButton(){
		//백버튼
		Application.LoadLevel (0);
	}

	/*********************************************
	 * @brief 공격 버튼 누를시 상태 전환
	 * @param bool $bShoot 버튼 상태
	*********************************************/
	void ShootButtonOn(){//버튼누르고있으면 발사
		bShoot = true;

	}

	void ShootButtonOff(){//버튼떼면 중지
		bShoot = false;
	}
}
