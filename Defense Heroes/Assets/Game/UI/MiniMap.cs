using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	public GUITexture redRect;
	private Camera miniCam;
	private Vector3 viewPortPos;

	// Use this for initialization
	void Start() {
		miniCam = (Camera)GameObject.Find ("MiniMap Camera").GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		viewPortPos = miniCam.WorldToViewportPoint(this.transform.position);
		redRect.transform.position = new Vector2(viewPortPos.x -0.5f, viewPortPos.y - 0.5f);
	}
}