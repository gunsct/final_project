using UnityEngine;
using System.Collections;

public class TestCode : MonoBehaviour 
{
    public Shell[] startNode { get; set; }
	public Shell goalNode { get; set; }

    public ArrayList pathArray;
	public ArrayList pathOneArray;//sp1 iEnemy01
	public ArrayList pathTwoArray;//sp2 iEnemy23
	public ArrayList pathThrArray;//sp2 iEnemy45
	public ArrayList pathFourArray;//sp2 iEnemy67
	int countArr = 1;
	int countCort = 0;
	bool bStart = false;
	bool bSpawn = false;
	
	private GameObject[] oStart;
	private GameObject oEnd;
	public GameObject slime;
	private GameObject[] iEnemy;
	
    private float elapsedTime = 0.0f;
    public float intervalTime = 1.0f; //Interval time between path finding
	private int timer;
	private int currentTime = 0;

	//class
	private Map map;
	// Use this for initialization
	void Start () 
    {
		map = this.GetComponent<Map>();

		oStart = new GameObject[4];
		oEnd = GameObject.FindGameObjectWithTag("Player");

		iEnemy = new GameObject[8];

        //AStar Calculated Path
		startNode = new Shell[4];
        pathArray = new ArrayList();
		pathOneArray = new ArrayList ();
		pathTwoArray = new ArrayList ();
		pathThrArray = new ArrayList ();
		pathFourArray = new ArrayList ();
        //FindPath();
	}
	
	// Update is called once per frame
	void Update () 
    {
		timer = (int)GameObject.Find ("GameManager").GetComponent<IngameUI> ().sec;

        elapsedTime += Time.deltaTime;

        if(elapsedTime <= intervalTime)
        {
            //elapsedTime = 0.0f;
			InitSpawn();
           
        }

		SpawnEnemy ();
	}

	void InitSpawn(){//스테이지별 시작점 지정
		if (map.stageNum == 0) {
			for(int i=0;i<4;i++){
				if (i < 2) {
					oStart [i] = map.spOneArray [i];//GameObject.FindGameObjectWithTag("Start");
				} else {
					oStart [i] = map.spTwoArray [i - 2];
				}
			}
		}
		if (this.GetComponent<Map> ().stageNum == 1) {
		}

		FindPath();
	}

    void FindPath()//스테이지별로 스위치로 바꾸자
    {
        //Assign StartNode and Goal Node
		for (int i = 0; i < 4; i++) {
			startNode[i] = new Shell (GridManager.instance.GetGridCellCenter (GridManager.instance.GetGridIndex (oStart [i].transform.position)));
		}
		goalNode = new Shell(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(oEnd.transform.position)));

		//pathArray = AStar.FindPath(startNode, goalNode);
		pathOneArray = AStar.FindPath(startNode[0], goalNode);
		pathTwoArray = AStar.FindPath(startNode[1], goalNode);
		pathThrArray = AStar.FindPath(startNode[2], goalNode);
		pathFourArray = AStar.FindPath(startNode[3], goalNode);

		if(bStart == false)
			StartCoroutine ("MoveLeader");

		bStart = true;
    }

	void SpawnEnemy(){
		if(timer >= currentTime+1)//중복 복사 제어
			switch(timer){
		case 2:
			currentTime = timer;
			iEnemy[0] = (GameObject)Instantiate (slime, oStart[0].transform.position, Quaternion.identity);
			iEnemy[1] = (GameObject)Instantiate (slime, oStart[1].transform.position, Quaternion.identity);
			break;
		case 10:
			currentTime = timer;
			iEnemy[3] = (GameObject)Instantiate (slime, oStart[0].transform.position, Quaternion.identity);
			iEnemy[4] = (GameObject)Instantiate (slime, oStart[1].transform.position, Quaternion.identity);
			break;
		/*case 10:
			currentTime = timer;
			iSlime = (GameObject)Instantiate (slime, objStartCube.transform.position, Quaternion.identity);
			break;*/
		}
	}

	//찾은 루트를 따라 2초마다 선두가 이동
	IEnumerator MoveLeader(){
		if (pathOneArray.Count <= countArr)
			countArr = 0;

		if (countCort == 20) {//2초마다
			//스포너마다 IEnemy 2개씩 붙여서 존재시만 이동하게 하자
			//지금 이동은 시작과 동시니까 각 부분별로 bool 넣어서 따로 제어하자
			if (iEnemy [0]) {//존재시만 이동
				Shell nextShell = (Shell)pathOneArray [countArr];
				iEnemy [0].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathOneArray [pathOneArray.Count-1]) {//종점시 null로 이동불가
					iEnemy [0] = null;
				}
			}
			if (iEnemy [1]) {
				Shell nextShell = (Shell)pathTwoArray [countArr];
				iEnemy [1].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathOneArray [pathTwoArray.Count-1]) {
					iEnemy [1] = null;
				}
			}
			countArr++;
			countCort = 0;
		}
		countCort++;

		yield return new WaitForSeconds (0.1f);
		StartCoroutine ("MoveLeader");
	}

	/*void OnDrawGizmos()
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
	}*/
}