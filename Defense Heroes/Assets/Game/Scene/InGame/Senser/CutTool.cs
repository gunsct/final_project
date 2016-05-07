using UnityEngine;
using System.Collections;

public class CutTool : MonoBehaviour {
	public Material capMaterial;
	GameObject victim;
	int cnt = 0;
	// Use this for initialization
	void Start () {
	}

	void Update(){
		//에임쪽으로 
		//this.transform.LookAt(GameObject.Find ("ShootPoint").GetComponent<ShootLaser> ().aimPos);
		this.transform.Rotate (new Vector3 (0.0f, 0.0f, 90.0f) * Time.deltaTime);
			RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {

			victim = hit.collider.gameObject;
			//맞은 오브젝트가 돌이고 안잘리고 체력이 0이하일때만 썰림
			/*if (victim.name.Equals("Stone(Clone)") && victim.GetComponent<Stone>().cutCnt == 0 && victim.GetComponent<Stone>().hp <= 0) {
				GameObject[] pieces = MeshCutter.Cut (victim.transform.FindChild("rock").gameObject, transform.position, transform.right, capMaterial);
				victim.GetComponent<Stone>().cutCnt++;//다시 안잘리게 
				//victim.GetComponent<SphereCollider> ().isTrigger = false;
				victim.GetComponent<Rigidbody> ().useGravity = true;
				victim.transform.FindChild ("left side").GetComponent<Rigidbody> ().useGravity = true;
				if (!pieces [1].GetComponent<Rigidbody> ()) {
					pieces [1].transform.localScale = new Vector3 (0.005f, 0.005f, 0.005f);
					pieces [1].AddComponent<Rigidbody> ();
					pieces [1].GetComponent<Rigidbody> ().useGravity = true;
					pieces [1].AddComponent<MeshCollider> ();
					//pieces [1].GetComponent<MeshCollider> ().convex = true;
				}

				Destroy (pieces [1], 5);
				Destroy (victim, 5);
			}*/

			if (victim.name.Equals ("Stone(Clone)")&& victim.GetComponent<Stone>().cutCnt == 0 && victim.GetComponent<Stone> ().hp <= 0) {
				StartCoroutine ("DestroyStone");
			}
		}
	}
	IEnumerator DestroyStone(){//슬라이스 연산렉을 도저희..
		if(victim.name.Equals ("Stone(Clone)")){
			victim.GetComponent<Stone>().cutCnt++;//다시 안잘리게 
			victim.GetComponent<Rigidbody> ().useGravity = true;
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
				pieces [i] [1].transform.localScale = new Vector3 (0.005f, 0.005f, 0.005f);
				pieces [i] [1].AddComponent<Rigidbody> ();
				pieces [i] [1].AddComponent<MeshCollider> ();
				pieces [i] [1].GetComponent<MeshCollider> ().convex = true;
			}
			Destroy (victim, 5.0f);
			for (int i = 0; i < 7; i++) {
				Destroy (pieces [i] [1], 5.0f);
			}
		}
		yield return new WaitForSeconds (0.1f);
	}
	void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100.0f);
		/*Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * 0.5f);*/

	}

}
