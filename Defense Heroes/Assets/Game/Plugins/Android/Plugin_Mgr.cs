using UnityEngine;
using System.Collections;

public class Plugin_Mgr : MonoBehaviour {
	#if UNITY_ANDROID

	AndroidJavaObject _AndroidPluginObj = null;

	// Use this for initialization
	void Start () {
		AndroidJNI.AttachCurrentThread ();
		_Init ();
	}

	void Destroy(){
		if (_AndroidPluginObj != null)
			_AndroidPluginObj.Dispose ();
	}

	public void CallFunctionName(string strParam){
		strLavelReceiveMessage = strParam;
	}

	int _Init(){
		AndroidJavaClass _androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");

		if(_androidJC != null){
			_AndroidPluginObj = _androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			_androidJC.Dispose ();

			if(_AndroidPluginObj == null){
				return -1;
			}

			return 0;
		}

		return -1;
	}

	int CallIntValue(){
		if(_AndroidPluginObj != null){
			int nIntValue = _AndroidPluginObj.Call<int> ("GetPluginNum", 10, 20);
			return nIntValue;
		}

		return 0;
	}

	void Req_CheckPluginMessage(){
		if(_AndroidPluginObj != null){
			_AndroidPluginObj.Call ("PluginToUnitySendMessage");
		}
	}

	string strLabelReturnInt = "";
	string strLavelReceiveMessage = "";

	void OnGUI(){
		if(GUILayout.Button("Called IntValue",GUILayout.Width(200))){
			int nReturnValue = CallIntValue ();
			strLabelReturnInt = nReturnValue.ToString ();
		}

		if(GUILayout.Button("Called RequestMessage", GUILayout.Width(200))){
			Req_CheckPluginMessage ();
		}

		GUILayout.Label ("ReturnInt:");
		GUILayout.Label (strLabelReturnInt);
		GUILayout.Label ("ReceiveMessage");
		GUILayout.Label (strLavelReceiveMessage);
	}
	#endif // UNITY_ANDROID
	// Update is called once per frame
	//void Update () {
	
	//}
}
