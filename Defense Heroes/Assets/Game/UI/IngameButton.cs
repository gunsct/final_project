﻿using UnityEngine;
using System.Collections;

public class IngameButton : MonoBehaviour {
	public bool bShoot;
	private GameObject player;
	private GameObject shotPoint;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		shotPoint = GameObject.Find ("ShootPoint");
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
}
