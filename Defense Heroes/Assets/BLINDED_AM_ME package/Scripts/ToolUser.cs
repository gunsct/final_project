using UnityEngine;
using System.Collections;

public class ToolUser : MonoBehaviour {

	public Material capMaterial;
	int cnt = 0;
	// Use this for initialization
	void Start () {

		
	}
	
	void Update(){

		//if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {

			GameObject victim = hit.collider.gameObject;
			if (cnt == 0) {
				GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.right, capMaterial);
				GameObject[] pieces2 = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.up, capMaterial);
				GameObject[] pieces3 = BLINDED_AM_ME.MeshCut.Cut (victim, victim.transform.position, victim.transform.forward, capMaterial);

				GameObject[] pieces4 = BLINDED_AM_ME.MeshCut.Cut (pieces [1], transform.position, transform.up, capMaterial);
				GameObject[] pieces5 = BLINDED_AM_ME.MeshCut.Cut (pieces [1], pieces [1].transform.position, pieces [1].transform.forward, capMaterial);

				GameObject[] pieces6 = BLINDED_AM_ME.MeshCut.Cut (pieces2 [1], pieces2 [1].transform.position, pieces2 [1].transform.forward, capMaterial);
				GameObject[] pieces7 = BLINDED_AM_ME.MeshCut.Cut (pieces4 [1], pieces4 [1].transform.position, pieces4 [1].transform.forward, capMaterial);
			
				if (!pieces [1].GetComponent<Rigidbody> () && !pieces2 [1].GetComponent<Rigidbody> () && !pieces3 [1].GetComponent<Rigidbody> ()) {
					pieces [1].AddComponent<Rigidbody> ();
					pieces [1].AddComponent<MeshCollider> ();
					pieces [1].GetComponent<MeshCollider> ().convex = true;

					pieces2 [1].AddComponent<Rigidbody> ();
					pieces2 [1].AddComponent<MeshCollider> ();
					pieces2 [1].GetComponent<MeshCollider> ().convex = true;

					pieces3 [1].AddComponent<Rigidbody> ();
					pieces3 [1].AddComponent<MeshCollider> ();
					pieces3 [1].GetComponent<MeshCollider> ().convex = true;

					pieces4 [1].AddComponent<Rigidbody> ();
					pieces4 [1].AddComponent<MeshCollider> ();
					pieces4 [1].GetComponent<MeshCollider> ().convex = true;

					pieces5 [1].AddComponent<Rigidbody> ();
					pieces5 [1].AddComponent<MeshCollider> ();
					pieces5 [1].GetComponent<MeshCollider> ().convex = true;

					pieces6 [1].AddComponent<Rigidbody> ();
					pieces6 [1].AddComponent<MeshCollider> ();
					pieces6 [1].GetComponent<MeshCollider> ().convex = true;

					pieces7 [1].AddComponent<Rigidbody> ();
					pieces7 [1].AddComponent<MeshCollider> ();
					pieces7 [1].GetComponent<MeshCollider> ().convex = true;

				}
				cnt++;
			}
		}
		//}
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
