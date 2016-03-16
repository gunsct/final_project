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

	void MenuButton(){
		//백버튼
		Application.LoadLevel (0);
	}

	void ShootButtonOn(){//버튼누르고있으면 발사
		bShoot = true;

	}

	void ShootButtonOff(){//버튼떼면 중지
		bShoot = false;
	}
}
