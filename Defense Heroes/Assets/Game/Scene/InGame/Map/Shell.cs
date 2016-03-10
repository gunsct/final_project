using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {
	public enum sType { NONE, PATH, BLOCK, CASTLE, SPAWN, MINITOWER, MAINTOWER, CASTLELONG, CASTLECORNER, DOOR };
	public sType type;//셀의 타입
	public float xPos, yPos, zPos;//좌표

	public int heuristicCost; //이 셀부터 목적지까지 거리
	public int finalCost; //시작점부터 이 셀까지 거리 + 이 셀부터 목적지까지 거리
	public Shell parent; //이전 셀

	// Use this for initialization
	void Start () {
		type = sType.NONE;
		xPos = yPos = zPos = 0;
		heuristicCost = 0;
		finalCost = 0;
		parent = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
