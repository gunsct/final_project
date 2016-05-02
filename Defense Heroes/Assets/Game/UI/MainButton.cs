using UnityEngine;
using System.Collections;

public class MainButton : MonoBehaviour {
	private GameObject infomanager;
	private GameObject logout;
	private GameObject sysmsg;

	// Use this for initialization
	void Start () {
		infomanager = GameObject.Find ("InfoManager");
		logout = GameObject.Find ("LogOut");
		sysmsg = GameObject.Find ("SystemMsg");
	}
	
	// Update is called once per frame
	void Update () {
		//로그인 아웃 띄우기
		if (PlayerInfo.getInstance.LoadBLog () == 1) {
			logout.SetActive (true);
		}
		if (PlayerInfo.getInstance.LoadBLog () == 0) {
			logout.SetActive (false);
		}
	}

	/***************************************************************
	 * @brief 백버튼 누르면 프로그램 종료
	***************************************************************/
	public void ExitButton(){
		PlayerInfo.getInstance.SaveBLog (0);
		Application.Quit ();
	}

	/***************************************************************
	 * @brief 게임 시작을 위해 게임씬으로 전환
	***************************************************************/
	public void PlayButton(){
		Application.LoadLevel("Stage");
	}

	/***************************************************************
	 * @brief 업그레이드 위 시작을 위해 상점씬으로 전환
	***************************************************************/
	public void UpgradeButton(){
		Application.LoadLevel("Store");
	}

	public void LogOutButton(){
		PlayerInfo.getInstance.SaveBLog (0);
		sysmsg.GetComponent<UILabel> ().text = "";

	}
	/***************************************************************
	 * @brief 랭킹씬으로 전환
	***************************************************************/
}
