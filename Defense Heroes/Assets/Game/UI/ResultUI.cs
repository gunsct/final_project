using UnityEngine;
using System.Collections;

public class ResultUI : MonoBehaviour {
	private int score, point;
	private string tScore, tPoint;

	private GameObject oScore;
	private GameObject oPoint;

	// Use this for initialization
	void Start () {
		score = PlayerInfo.getInstance.LoadScore ();
		point = PlayerInfo.getInstance.LoadPoint ();

		oScore = GameObject.Find ("Score");
		oPoint = GameObject.Find ("Point");

		StartCoroutine ("Frame");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Frame(){
		tScore = "SCORE : " + score;
		tPoint = "POINT : " + point;

		oScore.GetComponent<UILabel> ().text = tScore;
		oPoint.GetComponent<UILabel> ().text = tPoint;

		yield return new WaitForSeconds (0.1f);//
		StartCoroutine ("Frame");
	}
}
