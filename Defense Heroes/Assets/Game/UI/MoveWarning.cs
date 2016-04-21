using UnityEngine;
using System.Collections;

public class MoveWarning : MonoBehaviour {
	private Vector3 position;

	private GameObject target;
	private Camera mainAimCamera;
	private Camera subAimCamere;

	private float x, y;
	private float timer;
	// Use this for initialization
	void Start () {
		x = y = timer = 0.0f;
		StartCoroutine ("Move");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		//if (timer >= 0.2f) {//8방향,정중앙일때 위치
			position = mainAimCamera.WorldToViewportPoint (target.transform.position);
			x = subAimCamere.ViewportToWorldPoint (position).x;
			y = subAimCamere.ViewportToWorldPoint (position).y;
			Debug.Log(x+" "+y);
			if (x > 1.9f && y > 0.0f) {
				x = 1.9f;
				y = 0.0f;
			}
			else if (x > 1.9f && y < -1.7f) {
				x = 1.9f;
				y = -1.7f;
			}
			else if (x < -1.0f && y > 0.0f) {
				x = -1.0f;
				y = 0.0f;
			}
			else if (x < -1.0f && y < -1.7f) {
				x = -1.0f;
				y = -1.7f;
			}
			else if (x >= -1.0f && x <= 1.9f && y > 0.0f)
				y = 0.0f;
			else if (x >= -1.0f && x <= 1.9f && y < -1.7f)
				y = -1.7f;
			else if (y >= -1.7f && y <= 0.0f && x > 1.9f)
				x = 1.9f;
			else if (y >= -1.7f && y <= 0.0f && x < -1.0f)
				x = -1.0f;

			Debug.Log(x+" "+y);
			this.transform.position = new Vector3 (x, y, 0.0f);
			Debug.Log(x+" "+y);
		//}

		if (timer >= 3.0f)
			Destroy (this.gameObject);
	}
	public void init(GameObject _target, Camera _mainAimCamera, Camera _subAimCamere){
		target = _target;
		mainAimCamera = _mainAimCamera;
		subAimCamere = _subAimCamere;
	}

	/*IEnumerator Move(){
		timer += 0.05f;

		if (timer >= 0.2f) {//8방향,정중앙일때 위치
			position = mainAimCamera.WorldToViewportPoint (target.transform.position);
			x = subAimCamere.ViewportToWorldPoint (position).x;
			y = subAimCamere.ViewportToWorldPoint (position).y;
			Debug.Log(x+" "+y);
			if (x > 1.9f && y > 0.0f) {
				x = 1.9f;
				y = 0.0f;
			}
			else if (x > 1.9f && y < -1.7f) {
				x = 1.9f;
				y = -1.7f;
			}
			else if (x < -1.0f && y > 0.0f) {
				x = -1.0f;
				y = 0.0f;
			}
			else if (x < -1.0f && y < -1.7f) {
				x = -1.0f;
				y = -1.7f;
			}
			else if (x >= -1.0f && x <= 1.9f && y > 0.0f)
				y = 0.0f;
			else if (x >= -1.0f && x <= 1.9f && y < -1.7f)
				y = -1.7f;
			else if (y >= -1.7f && y <= 0.0f && x > 1.9f)
				x = 1.9f;
			else if (y >= -1.7f && y <= 0.0f && x < -1.0f)
				x = -1.0f;

			Debug.Log(x+" "+y);
			this.transform.position = new Vector3 (x, y, 0.0f);
			Debug.Log(x+" "+y);
		}

		if (timer >= 3.0f)
			Destroy (this.gameObject);

		yield return new WaitForSeconds (0.05f);
		StartCoroutine ("Move");
	}*/
}
