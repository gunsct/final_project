using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	private static PlayerInfo instance;

	void Awake(){
		//DontDestroyOnLoad (this.gameObject);
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
	public void SaveBLog(int _blog){
		PlayerPrefs.SetInt ("BLog", _blog);
	}
	public void SavePacketType(string _type){
		PlayerPrefs.SetString ("Type", _type);
	}
	public void SaveLogInfo(string _id, string _password){
		PlayerPrefs.SetString ("Id", _id);
		PlayerPrefs.SetString ("Password", _password);
	}
	public void SaveScorePoint(int _score, int _point){
		PlayerPrefs.SetInt ("Score", _score);
		PlayerPrefs.SetInt ("Point", _point);
		PlayerPrefs.Save ();
	}
	public void SaveClearStage(int _stage){
		PlayerPrefs.SetInt ("CLEStage", _stage);
		PlayerPrefs.Save ();
	}
	public void SaveStage(int _stage){
		PlayerPrefs.SetInt ("Stage", _stage);
		PlayerPrefs.Save ();
	}
	public void SavePlayerInfo(int _point, float _maxHp, int _maxMp, float _reMp, float _speed, float _dmg, float _fullDmg, float _splash, float _meteorDmg, float _durationDmg, float _lightingDmg){
		PlayerPrefs.SetInt ("Point", _point);
		PlayerPrefs.SetFloat ("MaxHp", _maxHp);
		PlayerPrefs.SetFloat ("MaxMp", _maxMp);
		PlayerPrefs.SetFloat ("ReMp", _reMp);
		PlayerPrefs.SetFloat ("Speed", _speed);
		PlayerPrefs.SetFloat ("DMG", _dmg);
		PlayerPrefs.SetFloat ("FullDMG", _fullDmg);
		PlayerPrefs.SetFloat ("Splash", _splash);
		PlayerPrefs.SetFloat ("MeteorDmg", _meteorDmg);
		PlayerPrefs.SetFloat ("DurationDmg", _durationDmg);
		PlayerPrefs.SetFloat ("LightingDmg", _lightingDmg);
		PlayerPrefs.Save ();
	}


	public int LoadBLog(){
		return PlayerPrefs.GetInt ("BLog", 0);
	}
	public string LoadPacketType(){
		return PlayerPrefs.GetString ("Type", "");
	}
	public string LoadId(){
		return PlayerPrefs.GetString ("Id", "id");
	}
	public string LoadPassword(){
		return PlayerPrefs.GetString ("Password", "****");
	}
	public int LoadCLEStage(){
		return PlayerPrefs.GetInt ("CLEStage", 0);
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
		return PlayerPrefs.GetFloat ("DMG",1.5f);
	}
	public float LoadFullDMG(){
		return PlayerPrefs.GetFloat ("FullDMG",10.0f);
	}
	public float LoadSplash(){
		return PlayerPrefs.GetFloat ("Splash",0.0f);
	}
	public float LoadMeteorDmg(){
		return PlayerPrefs.GetFloat ("MeteorDmg",40.0f);
	}
	public float LoadDurationDmg(){
		return PlayerPrefs.GetFloat ("DurationDmg",1.0f);
	}
	public float LoadLightingDmg(){
		return PlayerPrefs.GetFloat ("LightingDmg",15.0f);
	}

	public int m_data{ set; get; }
}
