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

	//맵 오브젝트
	public GameObject oBlock;
	public GameObject oSpawn;
	public GameObject oMiniTower;
	public GameObject oMainTower;
	//public GameObject oPath;

	//cvs용 변수들
	TextAsset csvFile;//csv파일 오픈
	string fileFullStr;//전체 내용
	string[] strList;//한줄
	int cntLine;//줄 수
	string[] charList;//한 단어

	// Use this for initialization
	void Start () {
		ExcelLoader (stageNum);
		MapSetting ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//******************************************************************************
	//설명 : 엑셀에서 만든 맵을 cvs로 바꾼 후 텍스트를 읽어들이는 기능
	//리턴값 : 
	//매개변수 : int형의 스테이지 번호
	//매개변수명 : _stageNum
	//2016.01.24, 27 
	//******************************************************************************
	void ExcelLoader(int _stageNum){
		switch (_stageNum) {
			case 0:
				csvFile = (TextAsset)Resources.Load ("map") as TextAsset;
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


	//******************************************************************************
	//설명 : 읽어들인 텍트로 shell의 속성을 지정하고 맵을 만들어준다.
	//리턴값 : 
	//매개변수 :
	//매개변수명 : 
	//2016.01.24, 27
	//******************************************************************************
	void MapSetting(){
		map = new Shell[cntLine,cntLine];//shell배열 선언
		oBlock =  MonoBehaviour.Instantiate(Resources.Load("prefabs/Block") as GameObject);


		for(int i = 0; i < cntLine; i++){//1차원 배열로 1자씩 떼서 21개단위로 본다
			charList = strList[i].Split(',');//캐릭터단위로 자름

			for (int j = 0; j < cntLine; j++) {
				map[i,j] = new Shell();//각 맵의 shell을 초기화..이거때문에 에러남 ㅠ..

				//좌표설정
				map [i, j].xPos = j * shellSize;
				map [i, j].zPos = i * shellSize;
				Vector3 oPos = new Vector3 (map [i, j].xPos, map [i, j].yPos, map [i, j].zPos); 

				//타입설정
				switch (charList [j]) {//왜 j가 20일때 값을 않넣어줄까?
					case "p":
						map [i, j].type = Shell.sType.PATH;
					//GameObject Path = (GameObject)Instantiate (oPath, oPos, Quaternion.identity);
						break;
					case "b":
						map [i, j].type = Shell.sType.BLOCK;
						GameObject Block = (GameObject)Instantiate (oBlock, oPos, Quaternion.identity);
						break;
					case "s":
						map [i, j].type = Shell.sType.SPAWN;
						GameObject Spawn = (GameObject)Instantiate (oSpawn, oPos, Quaternion.identity);
						break;
					case "a":
						map [i, j].type = Shell.sType.MINITOWER;
						GameObject MiniTower = (GameObject)Instantiate (oMiniTower, oPos, Quaternion.identity);
						break;
					case "m":
						map [i, j].type = Shell.sType.MAINTOWER;
						GameObject MainTower = (GameObject)Instantiate (oMainTower, oPos, Quaternion.identity);
						break;
				}
			

				Debug.Log (i + " " + j + " " +charList[j] +" "+ map [i, j].type +" "+ map [i, j].xPos +" "+ map [i, j].yPos +" "+ map [i, j].zPos);
			}
		}
			
	}
}
//맵을 미리 만들어두고 따로 벽 번호만 텍스트같은거에 두고 읽어서 지정해줘야할듯
