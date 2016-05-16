using UnityEngine;
using System.Collections;

public class priorityQ : MonoBehaviour {
	//노드받는 배열,길이,contain, push,remove, sort있어야되 sort는 f = g+h 로 최단 거리 이건 노드쪽에서 해결해야함
	public ArrayList nodes = new ArrayList();

	public int Length(){
		return nodes.Count;
	}

	//contains가 !null true / null false이므로 bool형
	public bool Contain(Node node){
		return nodes.Contains (node);
	}

	//추가한뒤에 정렬
	public void Push(Node node){
		nodes.Add (node);
		nodes.Sort ();
	}

	//삭제하고 정렬
	public void Remove(Node node){
		nodes.Remove (node);
		nodes.Sort ();
	}

	public Node Start(){
		if(Length() > 0){//노드가 존재할때 반환하고 없으면 null
			return (Node)nodes[0];
		}
		if (Length () > 1) {
			return (Node)nodes [1];
		}
		return null;
	}
}
