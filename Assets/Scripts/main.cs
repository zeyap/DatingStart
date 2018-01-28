using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour {
	Button start;
	// Use this for initialization
	void Start () {
		start = GameObject.Find ("StartButton").GetComponent<Button>();
		start.onClick.AddListener (OnClickStart);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClickStart(){
		SceneManager.LoadScene ("adjust");
	}
}
