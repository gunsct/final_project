using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class StoreButton : MonoBehaviour {
	private string msginit;
	public GameObject tPoint, tMsg; 
	public GameObject[] tNum;
	public GameObject[] bLevel;

	public AudioClip aShort,aUpgrade;
	AudioSource audio;

	public GameObject infomanager;
	// Use this for initialization
	void Start () {
		tPoint.GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadPoint ().ToString();
		msginit = "보유 금액";
		tMsg.GetComponent<UILabel> ().text = msginit;

		//데미지,포인트,증가율 텍스트 초기화
		tNum [0].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadMaxHp ().ToString () + "+20 / " + PlayerInfo.getInstance.LoadHpPrice().ToString() + "원";
		tNum [1].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadDMG().ToString () + "+0.1 / " + PlayerInfo.getInstance.LoadNormalPrice().ToString() + "원";
		tNum [2].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadFullDMG ().ToString () + "+0.5 / " + PlayerInfo.getInstance.LoadPowerBallPrice().ToString() + "원";
		tNum [3].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadLightingDmg ().ToString () + "+1 / " + PlayerInfo.getInstance.LoadStomePrice().ToString() + "원";
		tNum [4].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadMeteorDmg ().ToString () + "+5 / " + PlayerInfo.getInstance.LoadMetearPrice().ToString() + "원";

		bLevel [0].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadHpLevel () / 10.0f + 0.05f;
		bLevel [1].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadNormalLevel () / 10.0f + 0.05f;
		bLevel [2].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadPowerBallLevel () / 10.0f + 0.05f;
		bLevel [3].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadStomeLevel () / 10.0f + 0.05f;
		bLevel [4].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadMetearLevel () / 10.0f + 0.05f;

		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	void Back(){
		PlayerInfo.getInstance.SavePacketType ("upload");
		infomanager.GetComponent<Client> ().st.InitInfo ();
		infomanager.GetComponent<Client> ().ClickUploadStore ();
		StartCoroutine ("DelayOut");
	}

	//각 버튼 누를시 업그레이드 단계와 가격 판별 후 데이터 및 텍스트 갱신
	void Heart(){
		if (PlayerInfo.getInstance.LoadPoint () >= PlayerInfo.getInstance.LoadHpPrice() && PlayerInfo.getInstance.LoadHpLevel () < 10) {
			PlayerInfo.getInstance.SavePoint (PlayerInfo.getInstance.LoadPoint () - PlayerInfo.getInstance.LoadHpPrice ());
			PlayerInfo.getInstance.SaveHpStore (PlayerInfo.getInstance.LoadHpPrice () + 100, PlayerInfo.getInstance.LoadMaxHp () + 20.0f, PlayerInfo.getInstance.LoadHpLevel () + 1);
			bLevel [0].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadHpLevel () / 10.5f + 0.05f;
			tPoint.GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadPoint ().ToString();

			if (PlayerInfo.getInstance.LoadHpLevel () == 9) {
				tNum [0].transform.position = new Vector3 (tNum [0].transform.position.x + 0.3f, tNum [0].transform.position.y, tNum [0].transform.position.z);
				tNum [0].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadMaxHp ().ToString ();
			}else
				tNum [0].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadMaxHp ().ToString () + "+20 / " + PlayerInfo.getInstance.LoadHpPrice().ToString() + "원";
			
			audio.PlayOneShot (aUpgrade, 0.5f);
		} else {
			StartCoroutine ("PointMSG");
		}
	}
	void Normal(){
		if (PlayerInfo.getInstance.LoadPoint () >= PlayerInfo.getInstance.LoadNormalPrice() && PlayerInfo.getInstance.LoadNormalLevel () < 10) {
			PlayerInfo.getInstance.SavePoint (PlayerInfo.getInstance.LoadPoint () - PlayerInfo.getInstance.LoadNormalPrice());
			PlayerInfo.getInstance.SaveNormalStore (PlayerInfo.getInstance.LoadNormalPrice () + 150, PlayerInfo.getInstance.LoadDMG () + 0.1f, PlayerInfo.getInstance.LoadNormalLevel () + 1);
			bLevel [1].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadNormalLevel () / 10.0f + 0.05f;
			tPoint.GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadPoint ().ToString();

			if (PlayerInfo.getInstance.LoadNormalLevel () == 9) {
				tNum [1].transform.position = new Vector3 (tNum [1].transform.position.x + 0.3f, tNum [1].transform.position.y, tNum [0].transform.position.z);
				tNum [1].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadDMG ().ToString ();
			}else
				tNum [1].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadDMG().ToString () + "+0.1 / " + PlayerInfo.getInstance.LoadNormalPrice().ToString() + "원";
			
			audio.PlayOneShot (aUpgrade, 0.5f);
		} else {
			StartCoroutine ("PointMSG");
		}
	}
	void PowerBall(){
		if (PlayerInfo.getInstance.LoadPoint () >= PlayerInfo.getInstance.LoadPowerBallPrice() && PlayerInfo.getInstance.LoadPowerBallLevel () < 10) {
			PlayerInfo.getInstance.SavePoint (PlayerInfo.getInstance.LoadPoint () - PlayerInfo.getInstance.LoadPowerBallPrice());
			PlayerInfo.getInstance.SavePowerBallStore (PlayerInfo.getInstance.LoadPowerBallPrice () + 200, PlayerInfo.getInstance.LoadFullDMG () + 0.5f, PlayerInfo.getInstance.LoadPowerBallLevel () + 1);
			bLevel [2].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadPowerBallLevel () / 10.0f + 0.05f;
			tPoint.GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadPoint ().ToString();

			if (PlayerInfo.getInstance.LoadPowerBallLevel () == 9) {
				tNum [2].transform.position = new Vector3 (tNum [2].transform.position.x + 0.3f, tNum [2].transform.position.y, tNum [0].transform.position.z);
				tNum [2].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadFullDMG ().ToString ();
			}else
				tNum [2].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadFullDMG ().ToString () + "+0.5 / " + PlayerInfo.getInstance.LoadPowerBallPrice().ToString() + "원";
			
			audio.PlayOneShot (aUpgrade, 0.5f);
		} else {
			StartCoroutine ("PointMSG");
		}
	}
	void Stome(){
		if (PlayerInfo.getInstance.LoadPoint () >= PlayerInfo.getInstance.LoadStomePrice() && PlayerInfo.getInstance.LoadStomeLevel () < 10) {
			PlayerInfo.getInstance.SavePoint (PlayerInfo.getInstance.LoadPoint () - PlayerInfo.getInstance.LoadStomePrice());
			PlayerInfo.getInstance.SaveStomeStore (PlayerInfo.getInstance.LoadStomePrice () + 400, PlayerInfo.getInstance.LoadLightingDmg () + 1.0f, PlayerInfo.getInstance.LoadStomeLevel () + 1);
			bLevel [3].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadStomeLevel () / 10.0f + 0.05f;
			tPoint.GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadPoint ().ToString();

			if (PlayerInfo.getInstance.LoadStomeLevel () == 9) {
				tNum [3].transform.position = new Vector3 (tNum [3].transform.position.x + 0.3f, tNum [3].transform.position.y, tNum [0].transform.position.z);
				tNum [3].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadLightingDmg ().ToString ();
			}else
				tNum [3].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadLightingDmg ().ToString () + "+1 / " + PlayerInfo.getInstance.LoadStomePrice().ToString() + "원";

			audio.PlayOneShot (aUpgrade, 0.5f);
		} else {
			StartCoroutine ("PointMSG");
		}
	}
	void Metear(){
		if (PlayerInfo.getInstance.LoadPoint () >= PlayerInfo.getInstance.LoadMetearPrice() && PlayerInfo.getInstance.LoadMetearLevel () < 10) {
			PlayerInfo.getInstance.SavePoint (PlayerInfo.getInstance.LoadPoint () - PlayerInfo.getInstance.LoadMetearPrice());
			PlayerInfo.getInstance.SaveMetearStore (PlayerInfo.getInstance.LoadMetearPrice () + 700, PlayerInfo.getInstance.LoadMeteorDmg () + 5.0f, PlayerInfo.getInstance.LoadMetearLevel () + 1);
			//tNum [4].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadMeteorDmg ().ToString () + "+5 / " + PlayerInfo.getInstance.LoadMetearPrice().ToString() + "원";
			bLevel [4].GetComponent<UISlider> ().sliderValue = PlayerInfo.getInstance.LoadMetearLevel () / 10.0f + 0.05f;
			tPoint.GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadPoint ().ToString();

			if (PlayerInfo.getInstance.LoadMetearLevel () == 9) {
				tNum [4].transform.position = new Vector3 (tNum [4].transform.position.x + 0.3f, tNum [4].transform.position.y, tNum [0].transform.position.z);
				tNum [4].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadMeteorDmg ().ToString ();
			}else
				tNum [4].GetComponent<UILabel> ().text = PlayerInfo.getInstance.LoadMeteorDmg ().ToString () + "+5 / " + PlayerInfo.getInstance.LoadMetearPrice().ToString() + "원";
			
			audio.PlayOneShot (aUpgrade, 0.5f);
		} else {
			StartCoroutine ("PointMSG");
		}
	}

	IEnumerator PointMSG(){
		tMsg.GetComponent<UILabel> ().text = "포인트가 부족해!";
		audio.PlayOneShot (aShort, 0.5f);
		yield return new WaitForSeconds (3.0f);
		tMsg.GetComponent<UILabel> ().text = msginit;
	}

	IEnumerator DelayOut(){
		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel ("Main");
	}
}
