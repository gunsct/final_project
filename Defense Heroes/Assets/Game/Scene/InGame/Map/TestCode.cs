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

	int[] countArr;//각 iEnemy의 등장과 함께 path의 위치를 알려준다.
	int countCort = 0;
	bool bStart = false;
	bool bSpawn = false;
	
	private GameObject[] oStart;
	private GameObject oEnd;

	public GameObject slime;
	public GameObject knight;
	public GameObject warrior;
	public GameObject dragon;
	public GameObject Leader;
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
		countArr = new int[8];
		for (int i = 0; i < 8; i++) {
			countArr [i] = 1;
		}

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

				iEnemy [0] = (GameObject)Instantiate (Leader, oStart [0].transform.position, Quaternion.identity);
				for (int i = 0; i < 5; i++) {
				GameObject goTemp = Instantiate (knight, new Vector3(iEnemy [0].transform.position.x - i/2,iEnemy [0].transform.position.y, iEnemy [0].transform.position.z), Quaternion.identity) as GameObject;
					goTemp.transform.parent = iEnemy [0].transform; 
				}

				iEnemy[1] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);
				for (int i = 0; i < 5; i++) {
				GameObject goTemp = Instantiate (knight, new Vector3(iEnemy [1].transform.position.x + i/2,iEnemy [1].transform.position.y, iEnemy [1].transform.position.z), Quaternion.identity) as GameObject;
					goTemp.transform.parent = iEnemy [1].transform; 
				}
				break;

			case 30:
				currentTime = timer;

				iEnemy[4] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				for (int i = 0; i < 7; i++) {
					GameObject goTemp = Instantiate (warrior, new Vector3(iEnemy [4].transform.position.x - i/2,iEnemy [4].transform.position.y, iEnemy [4].transform.position.z), Quaternion.identity) as GameObject;
					goTemp.transform.parent = iEnemy [4].transform; 
				}

				iEnemy[5] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);
				for (int i = 0; i < 7; i++) {
					GameObject goTemp = Instantiate (warrior, new Vector3(iEnemy [5].transform.position.x + i/2,iEnemy [5].transform.position.y, iEnemy [5].transform.position.z), Quaternion.identity) as GameObject;
					goTemp.transform.parent = iEnemy [5].transform; 
				}
				break;

			case 80:
				currentTime = timer;

				iEnemy[0] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				for (int i = 0; i < 4; i++) {
					GameObject goTemp = Instantiate (slime, new Vector3(iEnemy [0].transform.position.x - i/2,iEnemy [0].transform.position.y, iEnemy [0].transform.position.z), Quaternion.identity) as GameObject;
					goTemp.transform.parent = iEnemy [0].transform; 
				}

				iEnemy[1] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);
				for (int i = 0; i < 4; i++) {
					GameObject goTemp = Instantiate (slime, new Vector3(iEnemy [1].transform.position.x + i/2,iEnemy [1].transform.position.y, iEnemy [1].transform.position.z), Quaternion.identity) as GameObject;
					goTemp.transform.parent = iEnemy [1].transform; 
				}
				break;

			case 100:
				currentTime = timer;

				iEnemy[4] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				for (int i = 0; i < 2; i++) {
					GameObject goTemp = Instantiate (dragon, new Vector3(iEnemy [4].transform.position.x - i/2,iEnemy [4].transform.position.y, iEnemy [4].transform.position.z), Quaternion.identity) as GameObject;
					goTemp.transform.parent = iEnemy [4].transform; 
				}

				iEnemy[5] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);
				for (int i = 0; i < 2; i++) {
					GameObject goTemp = Instantiate (dragon, new Vector3(iEnemy [5].transform.position.x + i/2,iEnemy [5].transform.position.y, iEnemy [5].transform.position.z), Quaternion.identity) as GameObject;
					goTemp.transform.parent = iEnemy [5].transform; 
				}
				break;
			
		/*case 10:
			currentTime = timer;
			break;*/
		}
	}

	//찾은 루트를 따라 2초마다 선두가 이동
	IEnumerator MoveLeader(){
		if (countCort == 20) {//2초마다
			//스포너마다 IEnemy 2개씩 붙여서 존재시만 이동하게 하자
			//지금 이동은 시작과 동시니까 각 부분별로 bool 넣어서 따로 제어하자
			if (iEnemy [0]) {//존재시만 이동
				countArr[0]++;
				Shell nextShell = (Shell)pathOneArray [countArr[0]];
				iEnemy [0].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathOneArray [pathOneArray.Count-1]) {//종점시 null로 이동불가,위치도 초기화
					iEnemy [0] = null;
					countArr [0] = 0;
				}
			}
			if (iEnemy [4]) {
				countArr[4]++;
				Shell nextShell = (Shell)pathOneArray [countArr[4]];
				iEnemy [4].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathOneArray [pathOneArray.Count-1]) {
					iEnemy [4] = null;
					countArr [4] = 0;
				}
			}

			if (iEnemy [1]) {
				countArr[1]++;
				Shell nextShell = (Shell)pathTwoArray [countArr[1]];
				iEnemy [1].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathTwoArray [pathTwoArray.Count-1]) {
					iEnemy [1] = null;
					countArr [1] = 0;
				}
			}
			if (iEnemy [5]) {
				countArr[5]++;
				Shell nextShell = (Shell)pathTwoArray [countArr[5]];
				iEnemy [5].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathTwoArray [pathTwoArray.Count-1]) {
					iEnemy [5] = null;
					countArr [5] = 0;
				}
			}

			if (iEnemy [2]) {
				countArr[2]++;
				Shell nextShell = (Shell)pathThrArray [countArr[2]];
				iEnemy [2].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathThrArray [pathThrArray.Count-1]) {
					iEnemy [2] = null;
					countArr [2] = 0;
				}
			}
			if (iEnemy [6]) {
				countArr[6]++;
				Shell nextShell = (Shell)pathThrArray [countArr[6]];
				iEnemy [6].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathThrArray [pathThrArray.Count-1]) {
					iEnemy [6] = null;
					countArr [6] = 0;
				}
			}

			if (iEnemy [3]) {
				countArr[3]++;
				Shell nextShell = (Shell)pathFourArray [countArr[3]];
				iEnemy [3].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathFourArray [pathFourArray.Count-1]) {
					iEnemy [3] = null;
					countArr [3] = 0;
				}
			}
			if (iEnemy [7]) {
				countArr[7]++;
				Shell nextShell = (Shell)pathFourArray [countArr[7]];
				iEnemy [7].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)pathFourArray [pathFourArray.Count-1]) {
					iEnemy [7] = null;
					countArr [7] = 0;
				}
			}

			countCort = 0;//시간 0초
		}
		countCort++;//시간 증가
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