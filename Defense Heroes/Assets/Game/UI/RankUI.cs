using UnityEngine;
using System.Collections;

public class RankUI : MonoBehaviour {
	public GameObject ma;
	// Use this for initialization
	void Start () {
		ma.GetComponent<PlayerInfo>().SavePacketType ("lookrank");
		ma.GetComponent<Client>().sc.InitInfo ();
		ma.GetComponent<Client>().ClickLookRank ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BackButton(){
		Application.LoadLevel(0);
	}
}
