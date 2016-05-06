using UnityEngine;
using System.Collections;

public class ToolUser : MonoBehaviour {
	GameObject victim;
	public Material capMaterial;
	int cnt = 0;
	// Use this for initialization
	void Start () {

		
	}
	
	void Update(){

		//if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {

			victim = hit.collider.gameObject;
			if (cnt == 0) {
				StartCoroutine ("test");
				//Cut8Mesh (victim);
				cnt++;
			}
		}
	}

	void Cut8Mesh(GameObject victim){
		if (cnt <= 1) {
			GameObject[][] pieces = new GameObject[7][];
			for (int i = 0; i < 7; i++) {
				pieces [i] = new GameObject[2];
			}
			pieces [0] = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.right, capMaterial);
			pieces [1] = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.up, capMaterial);
			pieces [2] = BLINDED_AM_ME.MeshCut.Cut (victim, victim.transform.position, victim.transform.forward, capMaterial);
			pieces [3] = BLINDED_AM_ME.MeshCut.Cut (pieces [0] [1], transform.position, transform.up, capMaterial);
			pieces [4] = BLINDED_AM_ME.MeshCut.Cut (pieces [0] [1], pieces [0] [1].transform.position, pieces [0] [1].transform.forward, capMaterial);
			pieces [5] = BLINDED_AM_ME.MeshCut.Cut (pieces [1] [1], pieces [1] [1].transform.position, pieces [1] [1].transform.forward, capMaterial);
			pieces [6] = BLINDED_AM_ME.MeshCut.Cut (pieces [3] [1], pieces [3] [1].transform.position, pieces [3] [1].transform.forward, capMaterial);

			for (int i = 0; i < 7; i++) {
				pieces [i] [1].AddComponent<Rigidbody> ();
				pieces [i] [1].AddComponent<MeshCollider> ();
				pieces [i] [1].GetComponent<MeshCollider> ().convex = true;
			}
			for (int i = 0; i < 7; i++) {
				Destroy (pieces [i] [1], 3.0f);
			}
			cnt++;
		}
	}

	IEnumerator test(){
		if (cnt <= 1) {
			GameObject[][] pieces = new GameObject[7][];
			for (int i = 0; i < 7; i++) {
				pieces [i] = new GameObject[2];
			}
			pieces [0] = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.right, capMaterial);
			pieces [1] = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.up, capMaterial);
			pieces [2] = BLINDED_AM_ME.MeshCut.Cut (victim, victim.transform.position, victim.transform.forward, capMaterial);
			pieces [3] = BLINDED_AM_ME.MeshCut.Cut (pieces [0] [1], transform.position, transform.up, capMaterial);
			pieces [4] = BLINDED_AM_ME.MeshCut.Cut (pieces [0] [1], pieces [0] [1].transform.position, pieces [0] [1].transform.forward, capMaterial);
			pieces [5] = BLINDED_AM_ME.MeshCut.Cut (pieces [1] [1], pieces [1] [1].transform.position, pieces [1] [1].transform.forward, capMaterial);
			pieces [6] = BLINDED_AM_ME.MeshCut.Cut (pieces [3] [1], pieces [3] [1].transform.position, pieces [3] [1].transform.forward, capMaterial);

			for (int i = 0; i < 7; i++) {
				pieces [i] [1].AddComponent<Rigidbody> ();
				pieces [i] [1].AddComponent<MeshCollider> ();
				pieces [i] [1].GetComponent<MeshCollider> ().convex = true;
			}
			Cut8Mesh (victim);
			for (int i = 0; i < 7; i++) {
				Destroy (pieces [i] [1], 3.0f);
			}
			/*for (int i = 0; i < 7; i++) {
				Cut8Mesh (pieces [i] [1]);
			}*/
			cnt++;
		}
		yield return new WaitForSeconds (0.5f);
	}
		
	void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
		/*Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * 0.5f);*/

	}

}
