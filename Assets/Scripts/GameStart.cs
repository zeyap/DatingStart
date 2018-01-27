using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	Button startBtn;
	Text timeTxt;
	Text startTxt;
	const float MaxReactionTime = 20;
	// Use this for initialization
	void Start () {
		startBtn = GameObject.Find ("startBtn").GetComponent<Button>();
		timeTxt = GameObject.Find ("timeTxt").GetComponent<Text> ();
		startTxt = GameObject.Find ("startTxt").GetComponent<Text> ();
		Time.timeScale = 0;
		startBtn.onClick.AddListener (OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		float elapseTime = Timer.GetElapseTime ();
		timeTxt.text = elapseTime.ToString("#.00");
		if (elapseTime > MaxReactionTime) {
			Freeze ();
		}
	}

	void Freeze(){
		Time.timeScale = 0;
		startBtn.gameObject.SetActive (true);
		startTxt.text = "时间到~";
	}
		
	void OnClick(){
		float level = LevelManager.GetFloatLevel ();
		Time.timeScale = 1;
		startBtn.gameObject.SetActive (false);
		if (level == 1) {
			LevelManager.SetFloatLevel(1.1f);
			OrganManager.RefreshOrgans ();
		}
		if (level == 1.1f) {
			LevelManager.SetFloatLevel(2.0f);
			OrganManager.RefreshOrgans ();
		}
		if (level == 2.0f) {
			LevelManager.SetFloatLevel(3.0f);
			OrganManager.RefreshOrgans ();
		}
		if (level == 3.0f) {
			SceneManager.LoadScene ("Ending");
		}
		Timer.Init ();
		Debug.Log (LevelManager.GetFloatLevel());
	}
}
