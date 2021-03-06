﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	Button startBtn;
	Text timeTxt;
	Text startTxt;

	static public float MaxReactionTime = 30;
	private ArrayList backgroundList = new ArrayList();
	private GameObject background;
	float elapseTime;
 
	// Use this for initialization
	void Start () {
		startBtn = GameObject.Find ("startBtn").GetComponent<Button>();
		timeTxt = GameObject.Find ("timeTxt").GetComponent<Text> ();
		startTxt = GameObject.Find ("startTxt").GetComponent<Text> ();
		startBtn.onClick.AddListener (OnClick);
		timeTxt.gameObject.SetActive (false);

		for (int i = 1; i <= 3; i++)
		{
			string fileName = "scene" + i.ToString();
			Sprite s = Resources.Load<Sprite>(fileName);
			backgroundList.Add(s);
		}
		background = GameObject.Find("background");
		background.GetComponent<SpriteRenderer>().sprite = (Sprite)backgroundList[0];
		StartCoroutine (CountDown());
	}

	IEnumerator CountDown(){
		for(int i=3;i>=1;i--){
			startTxt.text = i.ToString();
			yield return new WaitForSeconds (1);
		}
		startTxt.text = "Dating Start！";
		yield return new WaitForSeconds (1);
		OrganManager.RefreshOrgans ();
		Resume ();
		LevelManager.SetFloatLevel(1.1f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(2)) {
			elapseTime = MaxReactionTime+0.1f;
		} else {
			elapseTime = Timer.GetElapseTime ();
		}
		timeTxt.text = elapseTime.ToString("#.00");
		if (elapseTime > MaxReactionTime) {
			Freeze ();
		}
	}

	void Freeze(){
		Time.timeScale = 0;
		startBtn.gameObject.SetActive (true);
		startTxt.text = "Time's Up! _(:3JL)_";
	}

	void OnClick(){
		float level = LevelManager.GetFloatLevel ();
		Time.timeScale = 1;
		startBtn.gameObject.SetActive (false);
		Resume ();
		if (level == 1) {
			LevelManager.SetFloatLevel(1.1f);
			OrganManager.RefreshOrgans ();
		}
		if (level == 1.1f) {
            if (ChangeImage.getChoice() == 7)
            {
                LevelManager.SetFloatLevel(2.0f);
                background.GetComponent<SpriteRenderer>().sprite = (Sprite)backgroundList[1];
            }
            else
            {
				SceneManager.LoadScene ("BadEnding",LoadSceneMode.Single);
            }
            OrganManager.RefreshOrgans ();
		}
		if (level == 2.0f) {
            if (ChangeImage.getChoice() == 21)
            {
                LevelManager.SetFloatLevel(3.0f);
                background.GetComponent<SpriteRenderer>().sprite = (Sprite)backgroundList[2];
            }
			else
			{
				SceneManager.LoadScene ("BadEnding",LoadSceneMode.Single);
			}
			OrganManager.RefreshOrgans ();
		}
		if (level == 3.0f) {
			if (ChangeImage.getChoice() == 4)
			{
				LevelManager.SetFloatLevel(4.0f);
			}
			else
			{
				SceneManager.LoadScene ("BadEnding",LoadSceneMode.Single);
			}
			OrganManager.RefreshOrgans ();
		}
		if (level == 4.0f) {
			SceneManager.LoadScene ("GoodEnding", LoadSceneMode.Single);
		}
//		Debug.Log ("level" + LevelManager.GetFloatLevel());
	}

	void Resume(){
		Timer.Init ();
		Time.timeScale = 1;
		startBtn.gameObject.SetActive (false);
		timeTxt.gameObject.SetActive (true);
	}

	public static float getMaxtime(){
		return MaxReactionTime;
	}
}
