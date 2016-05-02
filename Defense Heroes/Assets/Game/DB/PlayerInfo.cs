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
	public void SavePoint(int _point){
		PlayerPrefs.SetInt ("Point", _point);
		PlayerPrefs.Save ();
	}
	public void SaveScorePoint(int _score, int _point){
		PlayerPrefs.SetInt ("Score", _score);
		PlayerPrefs.SetInt ("Point", _point);
		PlayerPrefs.Save ();
	}
	public void SaveClear(string _clear){
		PlayerPrefs.SetString ("Clear", _clear);
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
	public void SaveHpStore(int _price, float _hp, int _level){
		PlayerPrefs.SetInt ("HpPrice", _price);
		PlayerPrefs.SetFloat ("MaxHp", _hp);
		PlayerPrefs.SetInt ("HpLevel", _level);
	}
	public void SaveNormalStore(int _price, float _dmg, int _level){
		PlayerPrefs.SetInt ("NormalPrice", _price);
		PlayerPrefs.SetFloat ("DMG", _dmg);
		PlayerPrefs.SetInt ("NormalLevel", _level);
	}
	public void SavePowerBallStore(int _price, float _dmg, int _level){
		PlayerPrefs.SetInt ("PowerBallPrice", _price);
		PlayerPrefs.SetFloat ("FullDMG", _dmg);
		PlayerPrefs.SetInt ("PowerBallLevel", _level);
	}
	public void SaveStomeStore(int _price, float _dmg, int _level){
		PlayerPrefs.SetInt ("StomePrice", _price);
		PlayerPrefs.SetFloat ("LightingDmg", _dmg);
		PlayerPrefs.SetInt ("StomeLevel", _level);
	}
	public void SaveMetearStore(int _price, float _dmg, int _level){
		PlayerPrefs.SetInt ("MetearPrice", _price);
		PlayerPrefs.SetFloat ("MeteorDmg", _dmg);
		PlayerPrefs.SetInt ("MetearLevel", _level);
	}
	public void SavePlayerInfo(int _point, float _maxHp,float _reMp, float _dmg, float _fullDmg,float _meteorDmg,float _lightingDmg){
		PlayerPrefs.SetInt ("Point", _point);
		PlayerPrefs.SetFloat ("MaxHp", _maxHp);
		PlayerPrefs.SetFloat ("ReMp", _reMp);
		PlayerPrefs.SetFloat ("DMG", _dmg);
		PlayerPrefs.SetFloat ("FullDMG", _fullDmg);
		PlayerPrefs.SetFloat ("MeteorDmg", _meteorDmg);
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
	public string LoadClear(){
		return PlayerPrefs.GetString ("Clear","L O S E");
	}
	public int LoadCLEStage(){
		return PlayerPrefs.GetInt ("CLEStage", -1);
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
		return PlayerPrefs.GetFloat ("MaxHp",100.0f);//+20
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
		return PlayerPrefs.GetFloat ("DMG",1.5f);//+0.1
	}
	public float LoadFullDMG(){
		return PlayerPrefs.GetFloat ("FullDMG",10.0f);//+0.5
	}
	public float LoadSplash(){
		return PlayerPrefs.GetFloat ("Splash",0.0f);
	}
	public float LoadMeteorDmg(){
		return PlayerPrefs.GetFloat ("MeteorDmg",40.0f);//+5
	}
	public float LoadDurationDmg(){
		return PlayerPrefs.GetFloat ("DurationDmg",1.0f);
	}
	public float LoadLightingDmg(){
		return PlayerPrefs.GetFloat ("LightingDmg",15.0f);//+1
	}
	public int LoadHpLevel(){
		return PlayerPrefs.GetInt ("HpLevel",0);  
	}
	public int LoadNormalLevel(){
		return PlayerPrefs.GetInt ("NormalLevel",0);
	}
	public int LoadPowerBallLevel(){
		return PlayerPrefs.GetInt ("PowerBallLevel",0);
	}
	public int LoadStomeLevel(){
		return PlayerPrefs.GetInt ("StomeLevel",0);
	}
	public int LoadMetearLevel(){
		return PlayerPrefs.GetInt ("MetearLevel",0);
	}
	public int LoadHpPrice(){
		return PlayerPrefs.GetInt ("HpPrice",100);//+100  
	}
	public int LoadNormalPrice(){
		return PlayerPrefs.GetInt ("NormalPrice",200);//+150
	}
	public int LoadPowerBallPrice(){
		return PlayerPrefs.GetInt ("PowerBallPrice",250);//+200
	}
	public int LoadStomePrice(){
		return PlayerPrefs.GetInt ("StomePrice",500);//+400
	}
	public int LoadMetearPrice(){
		return PlayerPrefs.GetInt ("MetearPrice",1000);//+700
	}

	public int m_data{ set; get; }
}
