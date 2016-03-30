using UnityEngine;
using System.Collections;

//맵은 30 * 30 정도가 될같다. 중심 기준 좌우상하로 15칸씩 그려보니 너무 크다..21*21이 좋을지도
//셀 단위를 크기2로 잡는다 좌표가 2단위, 적 이속도 이거에 맞춰야겠지
//shell 자료형 배열로 배치할거고 벽은 따로 지정해서 읽어들이자..
//맵에 들어갈 자료구조는... 자동생성이 아니니 엑셀에서 벽정보 읽어서 저장할 리스트 만들자
public class Map : MonoBehaviour {
	//맵용 변수들
	public float shellSize = 2.0f;//셀간격, 좌표에 사용될것 
	Shell[,] map =null;
	public int stageNum = 0;//스테이지 번호
	public float waveTime = 0.0f;
	public GameObject[] blockArray;
	private int blockCnt = 0;

	//맵 오브젝트
	public GameObject oBlock;
	public GameObject oSpawn;
	public GameObject oMiniTower;
	public GameObject oMainTower;
	public GameObject oCastleLong;
	public GameObject oCastleCorner;
	public GameObject oDoor;
	//public GameObject oPath;

	//cvs용 변수들
	TextAsset csvFile;//csv파일 오픈
	string fileFullStr;//전체 내용
	string[] strList;//한줄
	int cntLine;//줄 수
	string[] charList;//한 단어

	// Use this for initialization
	void Start () {
		blockArray = new GameObject[110];
		ExcelLoader (stageNum);
		MapSetting ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/***************************************************************
	 * @brief 엑셀에서 만든 맵을 csv로 바꾼 후 텍스트를 읽어들이는 기능
	 * @param int $_stageNum 스테이지 번호
	 * @param TextAsset $csvFile csv파일 불러옴
	 * @param float $waveTime 웨이브 시간
	 * @param String $fileFullStr csv텍스트 전체
	 * @param String $strList csv텍스트 라인별
	 * @param String $cntLine csv텍스트 단어별	
	***************************************************************/
	void ExcelLoader(int _stageNum){
		switch (_stageNum) {
			case 0:
				csvFile = (TextAsset)Resources.Load ("stage1") as TextAsset;
				waveTime = 90.0f;
				break;
			case 1:
				csvFile = (TextAsset)Resources.Load ("stage2") as TextAsset;
				waveTime = 120.0f;
				break;
		}

		fileFullStr = csvFile.text;
		strList = fileFullStr.Split ('\n');
		cntLine = strList.Length - 1;
		Debug.Log (cntLine);

		for (int i = 0; i < cntLine; i++) {
			Debug.Log (strList[i]);
		}
	}


	/***************************************************************
	 * @brief 읽어들인 텍트로 shell의 속성을 지정하고 맵을 만들어준다.
	 * @param Shell[] $map Shell 클래스로 구성된 맵
	 * @param GameObject $oBlock 벽
	 * @param GameObject $oSpawn 스포너
	 * @param GameObject $oMiniTower 미니타워
	 * @param GameObject $oMainTower 메인타워
	 * @param GameObject $oCastleLong 성벽 1자
	 * @param GameObject $oCastleCorner 성벽 코너
	 * @param GameObject $oDoor 성문
	 * @see 맵 오브젝트중 성벽과 문은 방향에 따라 복사 오브젝트가 다름
	***************************************************************/
	void MapSetting(){
		map = new Shell[cntLine,cntLine];//shell배열 선언
		//oBlock =  MonoBehaviour.Instantiate(Resources.Load("prefabs/Block") as GameObject);//이건 직접 폴더에서 부기 개멍청함


		for(int i = 0; i < cntLine; i++){//1차원 배열로 1자씩 떼서 21개단위로 본다
			charList = strList[i].Split(',');//캐릭터단위로 자름

			for (int j = 0; j < cntLine; j++) {
				map[i,j] = new Shell();//각 맵의 shell을 초기화..이거때문에 에러남 ㅠ..

				//좌표설정
				map [i, j].position.x = j * shellSize;
				map [i, j].position.z = i * shellSize;
				map [i, j].position.y = 0.0f;
				Vector3 oPos = new Vector3 (map [i, j].position.x, map [i, j].position.y, map [i, j].position.z); 

				//타입설정
				switch (charList [j]) {//왜 j가 14일때 값을 않넣어줄까?
					case "p":
						map [i, j].type = Shell.sType.PATH;
					//GameObject Path = (GameObject)Instantiate (oPath, oPos, Quaternion.identity);
						break;
					case "b":
						map [i, j].type = Shell.sType.BLOCK;
						GameObject Block = (GameObject)Instantiate (oBlock, oPos, Quaternion.identity);
						blockArray [blockCnt++] = Block;
						break;

					case "s":
						map [i, j].type = Shell.sType.SPAWN;
						GameObject Spawn = (GameObject)Instantiate (oSpawn, oPos, Quaternion.identity);
						break;
					case "s2":
						map [i, j].type = Shell.sType.SPAWN;
						GameObject Spawn2 = (GameObject)Instantiate (oSpawn, oPos, Quaternion.identity);
						break;
					case "s3":
						map [i, j].type = Shell.sType.SPAWN;
						GameObject Spawn3 = (GameObject)Instantiate (oSpawn, oPos, Quaternion.identity);
						break;

					case "mn":
						map [i, j].type = Shell.sType.MINITOWER;
						GameObject MiniTower = (GameObject)Instantiate (oMiniTower, oPos, Quaternion.identity);
						break;
					case "m":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject MainTower = (GameObject)Instantiate (oMainTower, oPos, Quaternion.identity);
						MainTower.transform.Rotate (270, 0, 0);
						break;

					case "cv":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject CastleLongV = (GameObject)Instantiate (oCastleLong, oPos, Quaternion.identity);
						CastleLongV.transform.Rotate (0, 90, 0);
						break;
					case "ch":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject CastleLongH = (GameObject)Instantiate (oCastleLong, oPos, Quaternion.identity);
						CastleLongH.transform.Rotate (0, 0, 0);
						break;
					case "clu":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject CastleLongLU = (GameObject)Instantiate (oCastleCorner, oPos, Quaternion.identity);
						CastleLongLU.transform.Rotate (0, 270, 0);
						break;
					case "cld":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject CastleLongLD = (GameObject)Instantiate (oCastleCorner, oPos, Quaternion.identity);
						CastleLongLD.transform.Rotate (0, 0, 0);
						break;
					case "cru":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject CastleLongRU = (GameObject)Instantiate (oCastleCorner, oPos, Quaternion.identity);
						CastleLongRU.transform.Rotate (0, 180, 0);
						break;
					case "crd":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject CastleLongRD = (GameObject)Instantiate (oCastleCorner, oPos, Quaternion.identity);
						CastleLongRD.transform.Rotate (0, 90, 0);
						break;

					case "dd":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject DoorD = (GameObject)Instantiate (oDoor, oPos, Quaternion.identity);
						DoorD.transform.Rotate (0, 180, 0);
						break;
					case "du":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject DoorU = (GameObject)Instantiate (oDoor, oPos, Quaternion.identity);
						DoorU.transform.Rotate (0, 0, 0);
						break;
				}
			

				Debug.Log (i + " " + j + " " +charList[j] +" "+ map [i, j].type +" "+ map [i, j].position.x +" "+ map [i, j].position.y +" "+ map [i, j].position.z);
			}
		}
		//맵을 먼저 생성 후 블럭 리스트를 그리드매니저로 전달
		this.GetComponent<GridManager> ().Setting (blockArray);

		for(int b= 0;b<108;b++){
			Debug.Log (blockArray [b].transform.position.x + " " + blockArray [b].transform.position.y + " " + blockArray [b].transform.position.z);
				}
	}
}
//맵을 미리 만들어두고 따로 벽 번호만 텍스트같은거에 두고 읽어서 지정해줘야할듯
