using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	Button startBtn;
	Text timeTxt;
	Text startTxt;
	const float MaxReactionTime = 30;
	// Use this for initialization
	void Start () {
		startBtn = GameObject.Find ("startBtn").GetComponent<Button>();
		timeTxt = GameObject.Find ("timeTxt").GetComponent<Text> ();
		startTxt = GameObject.Find ("startTxt").GetComponent<Text> ();
		startBtn.onClick.AddListener (OnClick);
		StartCoroutine (CountDown());
	}
	IEnumerator CountDown(){
		for(int i=3;i>=1;i--){
			startTxt.text = i.ToString();
			yield return new WaitForSeconds (1);
		}
		startTxt.text = "开始！";
		yield return new WaitForSeconds (1);
		OrganManager.RefreshOrgans ();
		Resume ();
		LevelManager.SetFloatLevel(1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (LevelManager.GetFloatLevel () > 1.0f) {
			float elapseTime = Timer.GetElapseTime ();
			timeTxt.text = elapseTime.ToString ("#.00");
			if (elapseTime > MaxReactionTime) {
				Freeze ();
			}
		}
	}

	void Freeze(){
		Time.timeScale = 0;
		startBtn.gameObject.SetActive (true);
		startTxt.text = "时间到~";
	}
		
	void OnClick(){
		float level = LevelManager.GetFloatLevel ();
		Resume ();
		if (level == 1.1f) {
			LevelManager.SetFloatLevel(2.0f);
			OrganManager.RefreshOrgans ();
		}
		if (level == 2.0f) {
			LevelManager.SetFloatLevel(3.0f);
			OrganManager.RefreshOrgans ();
		}
		if (level == 3.0f) {
			SceneManager.LoadScene ("Ending",LoadSceneMode.Single);
		}
		Debug.Log (LevelManager.GetFloatLevel());
	}

	void Resume(){
		Timer.Init ();
		Time.timeScale = 1;
		startBtn.gameObject.SetActive (false);
	}
}
