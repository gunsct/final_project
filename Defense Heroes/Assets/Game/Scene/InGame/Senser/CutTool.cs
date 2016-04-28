using UnityEngine;
using System.Collections;

public class CutTool : MonoBehaviour {
	public Material capMaterial;
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

			GameObject victim = hit.collider.gameObject;
			//맞은 오브젝트가 돌이고 안잘리고 체력이 0이하일때만 썰림
			if (victim.name.Equals("Stone(Clone)") && victim.GetComponent<Stone>().cutCnt == 0 && victim.GetComponent<Stone>().hp <= 0) {
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
			}
		}
		//}
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
