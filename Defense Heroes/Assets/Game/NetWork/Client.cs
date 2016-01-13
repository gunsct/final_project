using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
//만약의 2인 정도의 협동모드가 들어갈때 사용될 예정
//서버에서 맵과 에너미 스폰, 타이머를 정해서 클라이언트에게 전달해주고
//클라이언트에서는 플레이어 스탯 정보, 매시간 보는 방향, 발사 순간, 성곽 체력, 제거되는 에너미 번호(양쪽 모두 없어지는게 보여야하기때문)