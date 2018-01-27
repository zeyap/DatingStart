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
		Time.timeScale = 1;
		startBtn.gameObject.SetActive (false);
		Timer.Init ();
	}
}
