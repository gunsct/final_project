using UnityEngine;
using System.Collections;

public class AStar2 {

	// Use this for initialization
	public static priorityQ closedList;
	public static ArrayList path;
	private static Node start, end, hNode;
	private static Map map;
	private static int cntLine;

	public static ArrayList Path(Vector3 _start, Vector3 _end){
		closedList = new priorityQ ();
		map = new Map ();
		cntLine = (int)Mathf.Sqrt (map.map2.Length);

		for (int i = 0; i < cntLine; i++) {
			for (int j = 0; j < cntLine; j++) {
				if (map.map2 [i, j].pos == _start) {
					start = map.map2 [i, j];
					closedList.Push (start);
				}
				if (map.map2 [i, j].pos == _end) {
					end = map.map2 [i, j];
				}
			}
		}

		//pushq 루프
		while (!closedList.Contain (end)) {
			PushQ (closedList.Start ());
		}

		if (closedList.Contain (end)) {
			hNode = end;
			while (hNode != start) {
				path.Add (hNode);
				hNode = end.parent;
			}
			path.Add (start);
		}
		return path;
	}

	public static void FCostCal(Node _pNode,Node _cNode){//부모자식 정의 후 f,g,h계산하고 Q에 넣음
		float gc = _pNode.gCost + Mathf.Abs(_cNode.pos.x - _pNode.pos.x) +  Mathf.Abs(_cNode.pos.z - _pNode.pos.z);
		float hc = (_cNode.pos - end.pos).magnitude;
		float fc = _cNode.gCost + _cNode.hCost;

		if (_cNode.fCost > fc || _cNode.fCost == 1.0f) {
			_cNode.gCost = gc;
			_cNode.hCost = hc;
			_cNode.fCost = fc;
			_cNode.parent = _pNode;
			closedList.Push (_cNode);
		}
	}

	public static void PushQ(Node _node){//8방 노드를 전부 계산해서 큐에 넣는다.
		for (int i = 0; i < cntLine; i++) {
			for (int j = 0; j < cntLine; j++) {
				if (map.map2 [i, j].pos == _node.pos) {
					if (map.map2 [i - 1, j - cntLine] != null && map.map2 [i - 1, j - cntLine].type != Node.sType.BLOCK) {
						FCostCal (map.map2 [i, j], map.map2 [i - 1, j - cntLine]);
					}
					if (map.map2 [i, j - cntLine] != null && map.map2 [i - 1, j - cntLine].type != Node.sType.BLOCK) {
						FCostCal (map.map2 [i, j], map.map2 [i, j - cntLine]);
					}
					if (map.map2 [i + 1, j - cntLine] != null && map.map2 [i - 1, j - cntLine].type != Node.sType.BLOCK) {
						FCostCal (map.map2 [i, j], map.map2 [i + 1, j - cntLine]);
					}
					if (map.map2 [i - 1, j] != null && map.map2 [i - 1, j - cntLine].type != Node.sType.BLOCK) {
						FCostCal (map.map2 [i, j], map.map2 [i - 1, j]);
					}
					if (map.map2 [i + 1, j] != null && map.map2 [i - 1, j - cntLine].type != Node.sType.BLOCK) {
						FCostCal (map.map2 [i, j], map.map2 [i + 1, j]);
					}
					if (map.map2 [i - 1, j + cntLine] != null && map.map2 [i - 1, j - cntLine].type != Node.sType.BLOCK) {
						FCostCal (map.map2 [i, j], map.map2 [i - 1, j + cntLine]);
					}
					if (map.map2 [i, j + cntLine] != null && map.map2 [i - 1, j - cntLine].type != Node.sType.BLOCK) {
						FCostCal (map.map2 [i, j], map.map2 [i, j + cntLine]);
					}
					if (map.map2 [i + 1, j + cntLine] != null && map.map2 [i - 1, j - cntLine].type != Node.sType.BLOCK) {
						FCostCal (map.map2 [i, j], map.map2 [i + 1, j + cntLine]);
					}
				}
			}
		}
	}
}
