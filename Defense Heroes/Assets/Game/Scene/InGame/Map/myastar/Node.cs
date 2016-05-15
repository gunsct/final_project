using UnityEngine;
using System.Collections;
using System;

public class Node : IComparable  {
	//타입,위치, 총합비용,처음부터 현재노드까지 비용, 현재부터 종점까지 휴리스틱 비용, 장애물인지 판별
	public enum sType { NONE, PATH, BLOCK, CASTLE, SPAWN, MINITOWER, MAINTOWER, CASTLELONG, CASTLECORNER, DOOR };

	public sType type;//노드의 타입
	public Vector3 pos;
	public float fCost, gCost, hCost;
	public bool block;

	public Node(){
	}
	//생성자에서 위치 받기
	public Node(Vector3 _pos){
		type = sType.NONE;
		fCost = 1.0f;
		gCost = 0.0f;
		hCost = 0.0f;
		block = false;
		pos = _pos;
	}

	//priorityqueue에서 총합비용 f = g+h를 기준으로 정렬하기 위한 비교
	public int CompareTo(object obj)
	{
		Node node = (Node)obj;
		if (fCost < node.fCost)
			return -1;
		if (fCost > node.fCost)
			return 1;

		return 0;
	}

}
