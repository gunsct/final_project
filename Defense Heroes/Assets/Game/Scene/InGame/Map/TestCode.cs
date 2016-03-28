using UnityEngine;
using System.Collections;

public class TestCode : MonoBehaviour 
{
    private Transform startPos, endPos;
    public Shell startNode { get; set; }
	public Shell goalNode { get; set; }

    public ArrayList pathArray;
	
    GameObject objStartCube, objEndCube;
	
    private float elapsedTime = 0.0f;
    public float intervalTime = 1.0f; //Interval time between path finding

	// Use this for initialization
	void Start () 
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");

        //AStar Calculated Path
        pathArray = new ArrayList();
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
					Debug.Log (node.position + " " + nextNode.position + " " + startNode + " " + goalNode);
                    Debug.DrawLine(node.position, nextNode.position, Color.green);
                    index++;
                }
            };
        }
    }
}