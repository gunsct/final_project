using UnityEngine;
using System.Collections;

public class TestCode : MonoBehaviour 
{
    private Transform startPos, endPos;
    public Shell startNode { get; set; }
	public Shell goalNode { get; set; }

    public ArrayList pathArray;
	public ArrayList pathArray2;
	int index2 = 1;
	bool bStart = false;
	
    GameObject objStartCube, objEndCube;
	public GameObject leader;
	
    private float elapsedTime = 0.0f;
    public float intervalTime = 1.0f; //Interval time between path finding

	// Use this for initialization
	void Start () 
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");

		leader.transform.position = objStartCube.transform.position;
        //AStar Calculated Path
        pathArray = new ArrayList();
		pathArray2 = new ArrayList ();
        FindPath();


	}
	
	// Update is called once per frame
	void Update () 
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;
            FindPath();
        }
	}

    void FindPath()
    {
        startPos = objStartCube.transform;
        endPos = objEndCube.transform;

        //Assign StartNode and Goal Node
		startNode = new Shell(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos.position)));
		goalNode = new Shell(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));

		pathArray = AStar.FindPath(startNode, goalNode);
		pathArray2 = AStar.FindPath(startNode, goalNode);

		if(bStart == false)
			StartCoroutine ("MoveLeader");

		bStart = true;
    }

    void OnDrawGizmos()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Shell node in pathArray)
            {
                if (index < pathArray.Count)
                {
					Shell nextNode = (Shell)pathArray[index];
					//objStartCube.transform.position = nextNode.position;
					//Debug.Log (node.position + " " + nextNode.position + " " + startNode + " " + goalNode);
                    Debug.DrawLine(node.position, nextNode.position, Color.green);
                    index++;
                }
            };
        }
    }

	//찾은 루트를 따라 2초마다 선두가 이동
	IEnumerator MoveLeader(){
		if (pathArray2.Count <= index2)
			index2 = 0;

		Shell nextShell = (Shell)pathArray2[index2];
		leader.transform.position = new Vector3(nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);
		Debug.Log (nextShell.position +" "+pathArray2.Count+ " "+ index2);
		index2++;

		yield return new WaitForSeconds (2.0f);
		StartCoroutine ("MoveLeader");
	}
}