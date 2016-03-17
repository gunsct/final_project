using UnityEngine;
using System.Collections;

public class MainButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/***************************************************************
	 * @brief 백버튼 누르면 프로그램 종료
	***************************************************************/
	public void ExitButton(){
		Application.Quit ();
	}

	/***************************************************************
	 * @brief 게임 시작을 위해 게임씬으로 전환
	***************************************************************/
	public void PlayButton(){
		Application.LoadLevel(1);
	}

	/***************************************************************
	 * @brief 업그레이드 위 시작을 위해 상점씬으로 전환
	***************************************************************/
	public void UpgradeButton(){
		Debug.Log("버튼을 터치 이벤트!!");
	}

	/***************************************************************
	 * @brief 랭킹씬으로 전환
	***************************************************************/
	public void RankButton(){
		Debug.Log("버튼을 터치 이벤트!!");
	}
}
