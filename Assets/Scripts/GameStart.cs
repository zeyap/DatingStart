using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	Button startBtn;
	Text timeTxt;
	Text startTxt;
<<<<<<< HEAD
	public float MaxReactionTime = 20;
    private ArrayList backgroundList = new ArrayList();
    private GameObject background;
    // Use this for initialization
    void Start () {
=======
	const float MaxReactionTime = 30;
	// Use this for initialization
	void Start () {
>>>>>>> b7d868e46e02c8c94c2ce6215d68da58bbbbfa78
		startBtn = GameObject.Find ("startBtn").GetComponent<Button>();
		timeTxt = GameObject.Find ("timeTxt").GetComponent<Text> ();
		startTxt = GameObject.Find ("startTxt").GetComponent<Text> ();
		startBtn.onClick.AddListener (OnClick);
<<<<<<< HEAD
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
=======

        for (int i = 1; i <= 3; i++)
        {
            string fileName = "scene" + i.ToString();
            Sprite s = Resources.Load<Sprite>(fileName);
            backgroundList.Add(s);
        }
        background = GameObject.Find("background");
        background.GetComponent<SpriteRenderer>().sprite = (Sprite)backgroundList[0];
    }
>>>>>>> origin/master
	
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
<<<<<<< HEAD
		Resume ();
=======
        bool flag = ChangeImage.getFlag();
		Time.timeScale = 1;
		startBtn.gameObject.SetActive (false);
		if (level == 1) {
			LevelManager.SetFloatLevel(1.1f);
			OrganManager.RefreshOrgans ();
		}
>>>>>>> origin/master
		if (level == 1.1f) {
            if (flag)
            {
                LevelManager.SetFloatLevel(2.0f);
                background.GetComponent<SpriteRenderer>().sprite = (Sprite)backgroundList[1];
                ChangeImage.setFlag(false);
            }
            else
            {
                //bad scene
            }
            OrganManager.RefreshOrgans ();
		}
		if (level == 2.0f) {
            if (flag)
            {
                LevelManager.SetFloatLevel(3.0f);
                background.GetComponent<SpriteRenderer>().sprite = (Sprite)backgroundList[2];
                ChangeImage.setFlag(false);
            }
            else
            {
                //bad scene
            }
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
