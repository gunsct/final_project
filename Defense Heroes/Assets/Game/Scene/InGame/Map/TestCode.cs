using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TestCode : MonoBehaviour 
{
	public GameObject gameManager;
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
	public GameObject griffon;
	private Path[] airPath;

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

		airPath = new Path[2];
		airPath[0] = GameObject.Find ("AirPath1").GetComponent<Path>();
		airPath[1] = GameObject.Find ("AirPath2").GetComponent<Path>();

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

		//웨이브나 유닛 등장시 조명 색을 일정시간 바꿔줌
		if (bLignt == true) {
			lightTime += Time.deltaTime;
			oLight.GetComponent<Light> ().color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
			if (lightTime >= 2.5f) {
				lightTime = 0.0f;
				bLignt = false;

				oLight.GetComponent<Light> ().color = new Color (1.0f, 1.0f, 1.0f,1.0f);
			}
		}
		//적소환하는거 프레임마다 돌림
		SpawnEnemy ();
	}

	void InitSpawn(){//스테이지별 시작점 지정
		for (int i = 0; i < 4; i++) {
			if (i < 2) {
				oStart [i] = (GameObject)map.spOneArray [i];//GameObject.FindGameObjectWithTag("Start");
			} else {
				oStart [i] = (GameObject)map.spTwoArray [i - 2];
			}
		}

		FindPath();
	}

    void FindPath()//시작하면 스포너별 길찾기
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

	void SpawnEnemy(){//유닛 생성
		if(timer >= currentTime+1){//중복 복사 제어
			if (map.stageNum == 0) {
				switch (timer) {
					case 2:
						currentTime = timer;
						AlarmWave ();
						break;

					case 5:
						SpawnSetGround (0, 1, 0, 4);

						InstanceEnemy (0, 0, warrior, 4);
						InstanceEnemy (1, 4, warrior, 4);
						break;

					case 25:
						SpawnSetGround (0, 1, 1, 5);

						InstanceEnemy (0, 1, warrior, 3);
						InstanceEnemy (0, 1, knight, 1);

						InstanceEnemy (1, 5, warrior, 3);
						InstanceEnemy (1, 5, knight, 1);
						break;

					case 30:
						SpawnSetFly (2, 3, griffon, 1);
						break;

					case 35:
						SpawnSetGround (0, 1, 2, 6);

						InstanceEnemy (0, 2, slime, 1);
						InstanceEnemy (0, 2, knight, 3);

						InstanceEnemy (1, 6, slime, 1);
						InstanceEnemy (1, 6, knight, 3);
						break;

					case 45:
						SpawnSetGround (0, 1, 3, 7);

						InstanceEnemy (0, 3, slime, 1);
						InstanceEnemy (0, 3, warrior, 3);

						InstanceEnemy (1, 7, slime, 1);
						InstanceEnemy (1, 7, warrior, 3);
						break;

					case 70:
						SpawnSetFly (2, 3, griffon, 1);
						break;

					case 100:
						SpawnSetFly (2, 3, griffon, 1);
						break;

					case 105:
						SpawnSetGround (0, 1, 0, 4);

						InstanceEnemy (0, 0, knight, 3);
						InstanceEnemy (0, 0, slime, 1);

						InstanceEnemy (1, 4, knight, 3);
						InstanceEnemy (1, 4, slime, 1);
						break;

					case 120:
						SpawnSetFly (2, 3, griffon, 1);
						break;

					case 125:
						SpawnSetGround (0, 1, 1, 5);

						InstanceEnemy (0, 1, warrior, 3);
						InstanceEnemy (0, 1, knight, 1);

						InstanceEnemy (1, 5, warrior, 3);
						InstanceEnemy (1, 5, knight, 1);
						break;

					case 145:
						SpawnSetGround (0, 1, 2, 6);

						InstanceEnemy (0, 2, slime, 2);
						InstanceEnemy (0, 2, knight, 2);

						InstanceEnemy (1, 6, slime, 2);
						InstanceEnemy (1, 6, knight, 2);
						break;

					case 150:
						SpawnSetFly (2, 3, griffon, 1);
						break;

					case 180:
						SpawnSetGround (0, 1, 3, 7);

						InstanceEnemy (0, 3, dragon, 2);

						InstanceEnemy (1, 7, dragon, 2);
						break;

					case 190:
						SpawnSetFly (2, 3, griffon, 1);
						break;

					case 220:
						SpawnSetFly (2, 3, griffon, 1);
						break;

					case 225:
						SpawnSetGround (0, 1, 0, 4);

						InstanceEnemy (0, 0, warrior, 1);
						InstanceEnemy (0, 0, slime, 3);

						InstanceEnemy (1, 4, warrior, 1);
						InstanceEnemy (1, 4, slime, 3);
						break;

					case 235:
						SpawnSetGround (0, 1, 1, 5);

						InstanceEnemy (0, 1, knight, 4);

						InstanceEnemy (1, 5, knight, 4);
						break;

					case 240:
						SpawnSetFly (2, 3, griffon, 1);
						break;

					case 245:
						SpawnSetGround (0, 1, 2, 6);

						InstanceEnemy (0, 2, warrior, 1);
						InstanceEnemy (0, 2, knight, 3);

						InstanceEnemy (1, 6, warrior, 1);
						InstanceEnemy (1, 6, knight, 3);
						break;

					case 265:
						SpawnSetGround (0, 1, 3, 7);

						InstanceEnemy (0, 3, dragon, 3);
						InstanceEnemy (0, 3, slime, 2);

						InstanceEnemy (1, 7, dragon, 3);
						InstanceEnemy (1, 7, slime, 2);
						break;

					case 270 : 
						SpawnSetFly (2, 3, griffon, 1);
						break;
				}
			}


			if (map.stageNum == 1) {
				switch (timer) {
				case 2:
					currentTime = timer;
					AlarmWave ();
					break;

				case 5:
					SpawnSetGround (0, 1, 0, 4);

					InstanceEnemy (0, 0, warrior, 4);
					InstanceEnemy (1, 4, warrior, 4);
					break;

				case 25:
					SpawnSetGround (0, 1, 1, 5);

					InstanceEnemy (0, 1, warrior, 3);
					InstanceEnemy (0, 1, knight, 1);

					InstanceEnemy (1, 5, warrior, 3);
					InstanceEnemy (1, 5, knight, 1);
					break;

				case 30:
					SpawnSetFly (2, 3, griffon, 1);
					break;

				case 45:
					SpawnSetGround (0, 1, 2, 6);

					InstanceEnemy (0, 2, slime, 1);
					InstanceEnemy (0, 2, knight, 3);

					InstanceEnemy (1, 6, slime, 1);
					InstanceEnemy (1, 6, knight, 3);
					break;

				case 65:
					SpawnSetGround (0, 1, 3, 7);

					InstanceEnemy (0, 3, slime, 1);
					InstanceEnemy (0, 3, warrior, 3);

					InstanceEnemy (1, 7, slime, 1);
					InstanceEnemy (1, 7, warrior, 3);
					break;

				case 60:
					SpawnSetFly (2, 3, griffon, 1);
					break;

				case 90:
					SpawnSetFly (2, 3, griffon, 1);
					break;

				case 115:
					SpawnSetGround (0, 1, 0, 4);

					InstanceEnemy (0, 0, knight, 3);
					InstanceEnemy (0, 0, slime, 1);

					InstanceEnemy (1, 4, knight, 3);
					InstanceEnemy (1, 4, slime, 1);
					break;

				case 120:
					SpawnSetFly (2, 3, griffon, 1);
					break;

				case 135:
					SpawnSetGround (0, 1, 1, 5);

					InstanceEnemy (0, 1, warrior, 3);
					InstanceEnemy (0, 1, knight, 1);

					InstanceEnemy (1, 5, warrior, 3);
					InstanceEnemy (1, 5, knight, 1);
					break;

				case 150:
					SpawnSetFly (2, 3, griffon, 1);
					break;

				case 155:
					SpawnSetGround (0, 1, 2, 6);

					InstanceEnemy (0, 2, slime, 2);
					InstanceEnemy (0, 2, knight, 2);

					InstanceEnemy (1, 6, slime, 2);
					InstanceEnemy (1, 6, knight, 2);
					break;

				case 180:
					SpawnSetFly (2, 3, griffon, 1);
					break;

				case 190:
					SpawnSetGround (0, 1, 3, 7);

					InstanceEnemy (0, 3, dragon, 2);

					InstanceEnemy (1, 7, dragon, 2);
					break;

				case 210:
					SpawnSetFly (2, 3, griffon, 1);
					break;

				case 235:
					SpawnSetGround (0, 1, 0, 4);

					InstanceEnemy (0, 0, warrior, 1);
					InstanceEnemy (0, 0, slime, 3);

					InstanceEnemy (1, 4, warrior, 1);
					InstanceEnemy (1, 4, slime, 3);
					break;

				case 245:
					SpawnSetGround (0, 1, 1, 5);

					InstanceEnemy (0, 1, knight, 4);

					InstanceEnemy (1, 5, knight, 4);
					break;

				case 240:
					SpawnSetFly (2, 3, griffon, 1);
					break;

				case 255:
					SpawnSetGround (0, 1, 2, 6);

					InstanceEnemy (0, 2, warrior, 1);
					InstanceEnemy (0, 2, knight, 3);

					InstanceEnemy (1, 6, warrior, 1);
					InstanceEnemy (1, 6, knight, 3);
					break;

				case 275:
					SpawnSetGround (0, 1, 3, 7);

					InstanceEnemy (0, 3, dragon, 3);
					InstanceEnemy (0, 3, slime, 2);

					InstanceEnemy (1, 7, dragon, 3);
					InstanceEnemy (1, 7, slime, 2);
					break;

				case 270 : 
					SpawnSetFly (2, 3, griffon, 1);
					break;
				}
			}
		}
	}
		
	//지상유닛 생성
	void InstanceEnemy(int _start, int _inum, GameObject _oenemy, int _count){
		int enemyCnt = 0;
		float dst = 0.0f;

		if (map.stageNum == 0) {//젠 간격
			if (_start == 0)
				dst = -_count / 4;
			if (_start == 1)
				dst = _count / 4;
		}

		if (map.stageNum == 1) {
			dst = -_count / 4;
		}

		for (enemyCnt = 0; enemyCnt < _count; enemyCnt++) {
			GameObject goTemp = Instantiate (_oenemy, new Vector3(iEnemy [_inum].transform.position.x + dst, iEnemy [_inum].transform.position.y, iEnemy [_inum].transform.position.z), Quaternion.identity) as GameObject;
			goTemp.transform.parent = iEnemy [_inum].transform; 
		}
	}

	//공중유닛 생성
	void InstanceFlyEnemy(int _inum, int _path, GameObject _oenemy, int _count){
		for (int enemyCnt = 0; enemyCnt < _count; enemyCnt++) {
			GameObject grif =  Instantiate (_oenemy, new Vector3(oStart[_inum].transform.position.x, oStart[_inum].transform.position.y, oStart[_inum].transform.position.z), Quaternion.identity) as GameObject;
			grif.GetComponent<VehicleFollowing> ().path = airPath [_path];
		}
	}

	//지상유닛 리더, 위험표시 셋팅
	void SpawnSetGround(int _sp1, int _sp2, int _ie1, int _ie2){
		//스포너1, 스포너2, 리더1, 리더2, 히어로1, 히어로1 수, 히어로2, 히어로2 수
		currentTime = timer;
		AlarmSpawn ();

		gameManager.GetComponent<IngameUI> ().PointSpawn (oStart [_sp1]);
		gameManager.GetComponent<IngameUI> ().PointSpawn (oStart [_sp2]);

		iEnemy [_ie1] = (GameObject)Instantiate (Leader, oStart [_sp1].transform.position, Quaternion.identity);
		iEnemy [_ie2] = (GameObject)Instantiate (Leader, oStart [_sp2].transform.position, Quaternion.identity);
	}

	//공중유닛 생성 및 위험표시 셋팅
	void SpawnSetFly(int _sp1, int _sp2, GameObject _oie, int _coie){
		//스포너1, 스포너2, 리더1, 리더2, 히어로1, 히어로1 수, 히어로2, 히어로2 수
		currentTime = timer;
		AlarmSpawn ();

		gameManager.GetComponent<IngameUI> ().PointSpawn (oStart [_sp1]);
		gameManager.GetComponent<IngameUI> ().PointSpawn (oStart [_sp2]);

		InstanceFlyEnemy (_sp1,1, _oie, _coie);
		InstanceFlyEnemy (_sp2,0, _oie, _coie);
	}

	//a*로 리더 움직임
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

	void AlarmWave(){//웨이브 시작 알림음
		audio.PlayOneShot (aWave, 1.0f);
		bLignt = true;
	}

	void AlarmSpawn(){//유닛스폰 시작 알림음
		audio.PlayOneShot (aSpawn, 0.2f);
		bLignt = true;
	}

	//찾은 루트를 따라 2초마다 선두가 이동하게 하는 프레임
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

	//라인 디버깅
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