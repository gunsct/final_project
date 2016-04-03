using UnityEngine;
using System.Collections;

public class TestCode : MonoBehaviour 
{
	private Transform[] startPos;
	private Transform endPos;
    public Shell startNode { get; set; }
	public Shell goalNode { get; set; }

    public ArrayList pathArray;
	public ArrayList pathArray2;
	int index2 = 1;
	int corucnt = 0;
	bool bStart = false;
	bool bSpawn = false;
	
	private GameObject[] objStartCube;
	private GameObject objEndCube;
	public GameObject slime;
	private GameObject iSlime;
	
    private float elapsedTime = 0.0f;
    public float intervalTime = 1.0f; //Interval time between path finding
	private int timer;
	private int currentTime = 0;

	// Use this for initialization
	void Start () 
    {
		startPos = new Transform[4];
		objStartCube = new GameObject[4];

        objStartCube[0] = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");

		//leader.transform.position = objStartCube.transform.position;
        //AStar Calculated Path
        pathArray = new ArrayList();
		pathArray2 = new ArrayList ();
        FindPath();
	}
	
	// Update is called once per frame
	void Update () 
    {
		timer = (int)GameObject.Find ("GameManager").GetComponent<IngameUI> ().sec;

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;
            FindPath();
        }
		SpawnEnemy ();

	}

	void SpawnEnemy(){
		if(timer >= currentTime+1)//중복 복사 제어
		switch(timer){
			case 2:
				currentTime = timer;
				iSlime = (GameObject)Instantiate (slime, objStartCube[0].transform.position, Quaternion.identity);
				break;
		/*case 5:
			currentTime = timer;
			iSlime = (GameObject)Instantiate (slime, objStartCube.transform.position, Quaternion.identity);
			break;
		case 10:
			currentTime = timer;
			iSlime = (GameObject)Instantiate (slime, objStartCube.transform.position, Quaternion.identity);
			break;*/
		}
	}

    void FindPath()//스테이지별로 스위치로 바꾸자
    {
		startPos[0] = objStartCube[0].transform;
        endPos = objEndCube.transform;

        //Assign StartNode and Goal Node
		startNode = new Shell(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos[0].position)));
		goalNode = new Shell(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));

		pathArray = AStar.FindPath(startNode, goalNode);
		pathArray2 = AStar.FindPath(startNode, goalNode);

		if(bStart == false)
			StartCoroutine ("MoveLeader");

		bStart = true;
    }

   

	//찾은 루트를 따라 2초마다 선두가 이동
	IEnumerator MoveLeader(){
		if (pathArray2.Count <= index2)
			index2 = 0;

		if (corucnt == 20) {
			Shell nextShell = (Shell)pathArray2 [index2];
			iSlime.transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);
			Debug.Log (nextShell.position + " " + pathArray2.Count + " " + index2);
			index2++;
			corucnt = 0;
		}
		corucnt++;
		yield return new WaitForSeconds (0.1f);
		StartCoroutine ("MoveLeader");
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
}