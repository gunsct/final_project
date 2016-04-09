using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	private static PlayerInfo instance;

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}

	public static PlayerInfo getInstance
	{
		get{
			if(instance == null){
				instance = new PlayerInfo ();
			}
			return instance;
		}
	}

	public void SaveScorePoint(int _score, int _point){
		PlayerPrefs.SetInt ("Score", _score);
		PlayerPrefs.SetInt ("Point", _point);
		PlayerPrefs.Save ();
	}
	public void SaveStage(int _stage){
		PlayerPrefs.SetInt ("Stage", _stage);
		PlayerPrefs.Save ();
	}
	public void SavePlayerInfo(int _point, float _maxHp, int _maxMp, float _reMp, float _speed, float _dmg, float _fullDmg, float _splash){
		PlayerPrefs.SetInt ("Point", _point);
		PlayerPrefs.SetFloat ("MaxHp", _maxHp);
		PlayerPrefs.SetFloat ("MaxMp", _maxMp);
		PlayerPrefs.SetFloat ("ReMp", _reMp);
		PlayerPrefs.SetFloat ("Speed", _speed);
		PlayerPrefs.SetFloat ("DMG", _dmg);
		PlayerPrefs.SetFloat ("FullDMG", _fullDmg);
		PlayerPrefs.SetFloat ("Splash", _splash);
		PlayerPrefs.Save ();
	}

	public int LoadStage(){
		return PlayerPrefs.GetInt ("Stage", 0);
	}
	public int LoadScore(){
		return PlayerPrefs.GetInt ("Score",0);
	}
	public int LoadPoint(){
		return PlayerPrefs.GetInt ("Point",0);
	}
	public float LoadMaxHp(){
		return PlayerPrefs.GetFloat ("MaxHp",100.0f);
	}
	public float LoadMaxMp(){
		return PlayerPrefs.GetFloat ("MaxMp",100.0f);
	}
	public float LoadReMp(){
		return PlayerPrefs.GetFloat ("ReMp",1.0f);
	}
	public float LoadSpeed(){
		return PlayerPrefs.GetFloat ("Speed",0.1f);
	}
	public float LoadDMG(){
		return PlayerPrefs.GetFloat ("DMG",1.0f);
	}
	public float LoadFullDMG(){
		return PlayerPrefs.GetFloat ("FullDMG",0.0f);
	}
	public float LoadSplash(){
		return PlayerPrefs.GetFloat ("Splash",0.0f);
	}
		
	public int m_data{ set; get; }
}
