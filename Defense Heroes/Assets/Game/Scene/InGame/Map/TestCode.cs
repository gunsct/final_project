using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
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

	public GameObject oLight;
	private bool bLignt;
	private float lightTime = 0.0f;

	public AudioClip aWave, aSpawn;
	AudioSource audio;
	//class
	private Map map;
	// Use this for initialization
	void Start () 
    {
		map = this.GetComponent<Map>();

		oStart = new GameObject[4];
		oEnd = GameObject.FindGameObjectWithTag("Player");

		iEnemy = new GameObject[12];
		countArr = new int[12];
		for (int i = 0; i < 12; i++) {
			countArr [i] = 1;
			iEnemy [i] = null;
		}

        //AStar Calculated Path
		startNode = new Shell[4];
        pathArray = new ArrayList();
		pathOneArray = new ArrayList ();
		pathTwoArray = new ArrayList ();
		pathThrArray = new ArrayList ();
		pathFourArray = new ArrayList ();

		audio = GetComponent<AudioSource>();
		bLignt = false;
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

		if (bLignt == true) {
			lightTime += Time.deltaTime;
			oLight.GetComponent<Light> ().color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
			if (lightTime >= 2.5f) {
				lightTime = 0.0f;
				bLignt = false;

				oLight.GetComponent<Light> ().color = new Color (1.0f, 1.0f, 1.0f,1.0f);
			}
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
				AlarmWave();
				break;

			case 5:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[0] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[4] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 0, warrior, 5);

				InstanceEnemy (1, 4, warrior, 5);
				break;

			case 25:
				currentTime = timer;
				AlarmSpawn ();
				
				iEnemy [1] = (GameObject)Instantiate (Leader, oStart [0].transform.position, Quaternion.identity);
				iEnemy [5] = (GameObject)Instantiate (Leader, oStart [1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 1, warrior, 2);
				InstanceEnemy (0, 1, knight, 3);

				InstanceEnemy (1, 5, warrior, 2);
				InstanceEnemy (1, 5, knight, 3);
				break;

			case 35:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[2] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[6] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 2, slime, 2);
				InstanceEnemy (0, 2, knight, 3);

				InstanceEnemy (1, 6, slime, 2);
				InstanceEnemy (1, 6, knight, 3);
				break;

			case 45:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[3] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[7] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 3, slime, 1);
				InstanceEnemy (0, 3, warrior, 4);

				InstanceEnemy (1, 7, slime, 1);
				InstanceEnemy (1, 7, warrior, 4);
				break;


			case 90:
				currentTime = timer;
				AlarmWave();
				break;

			case 95:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[0] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[4] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 0, knight, 4);
				InstanceEnemy (0, 0, slime, 1);

				InstanceEnemy (1, 4, knight, 4);
				InstanceEnemy (1, 4, slime, 1);
				break;

			case 115:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy [1] = (GameObject)Instantiate (Leader, oStart [0].transform.position, Quaternion.identity);
				iEnemy [5] = (GameObject)Instantiate (Leader, oStart [1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 1, warrior, 3);
				InstanceEnemy (0, 1, knight, 2);

				InstanceEnemy (1, 5, warrior, 3);
				InstanceEnemy (1, 5, knight, 2);
				break;

			case 135:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[2] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[6] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 2, slime, 2);
				InstanceEnemy (0, 2, knight, 3);

				InstanceEnemy (1, 6, slime, 2);
				InstanceEnemy (1, 6, knight, 3);
				break;

			case 170:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[3] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[7] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 3, dragon, 2);

				InstanceEnemy (1, 7, dragon, 2);
				break;


			case 210:
				currentTime = timer;
				AlarmWave ();
				break;

			case 215:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[0] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[4] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 0, warrior, 3);
				InstanceEnemy (0, 0, slime, 2);

				InstanceEnemy (1, 4, warrior, 3);
				InstanceEnemy (1, 4, slime, 2);
				break;

			case 225:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy [1] = (GameObject)Instantiate (Leader, oStart [0].transform.position, Quaternion.identity);
				iEnemy [5] = (GameObject)Instantiate (Leader, oStart [1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 1, knight, 5);

				InstanceEnemy (1, 5, knight, 5);
				break;

			case 235:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[2] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[6] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 2, warrior, 3);
				InstanceEnemy (0, 2, knight, 2);

				InstanceEnemy (1, 6, warrior, 3);
				InstanceEnemy (1, 6, knight, 2);
				break;

			case 255:
				currentTime = timer;
				AlarmSpawn ();

				iEnemy[3] = (GameObject)Instantiate (Leader, oStart[0].transform.position, Quaternion.identity);
				iEnemy[7] = (GameObject)Instantiate (Leader, oStart[1].transform.position, Quaternion.identity);

				InstanceEnemy (0, 3, dragon, 3);
				InstanceEnemy (0, 3, slime, 2);

				InstanceEnemy (1, 7, dragon, 3);
				InstanceEnemy (1, 7, slime, 2);
				break;
			
		/*case 10:
			currentTime = timer;
			break;*/
		}
	}
		
	void InstanceEnemy(int _start, int _inum, GameObject _oenemy, int _count){
		int enemyCnt = 0;
		float dst = 0.0f;

		if (_start == 0) dst = -enemyCnt / 3;
		if (_start == 1) dst = enemyCnt / 3;

		for (enemyCnt = 0; enemyCnt < _count; enemyCnt++) {
			GameObject goTemp = Instantiate (_oenemy, new Vector3(iEnemy [_inum].transform.position.x + dst, iEnemy [_inum].transform.position.y, iEnemy [_inum].transform.position.z), Quaternion.identity) as GameObject;
			goTemp.transform.parent = iEnemy [_inum].transform; 
		}
	}

	void MovingLeader(ArrayList _path ,int _startnum, int _endnum){
		for (int i = _startnum; i < _endnum; i++) {
			if (iEnemy [i]) {
				countArr [i]++;
				Shell nextShell = (Shell)_path [countArr [i]];
				iEnemy [i].transform.position = new Vector3 (nextShell.position.x - 1f, 1.0f, nextShell.position.z - 1f);

				if (nextShell == (Shell)_path [_path.Count - 1]) {
					iEnemy [i] = null;
					countArr [i] = 0;
				}
			}
		}
	}

	void AlarmWave(){
		audio.PlayOneShot (aWave, 1.0f);
		bLignt = true;
	}

	void AlarmSpawn(){
		audio.PlayOneShot (aSpawn, 1.0f);
		bLignt = true;
	}
	//찾은 루트를 따라 2초마다 선두가 이동
	IEnumerator MoveLeader(){
		if (countCort == 20) {//2초마다
			//스포너마다 IEnemy 2개씩 붙여서 존재시만 이동하게 하자
			//지금 이동은 시작과 동시니까 각 부분별로 bool 넣어서 따로 제어하자
			MovingLeader(pathOneArray, 0, 4);
			MovingLeader(pathTwoArray, 4, 8);
			MovingLeader(pathThrArray, 8, 10);
			MovingLeader(pathFourArray, 10, 12);

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