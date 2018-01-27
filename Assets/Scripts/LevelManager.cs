using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	static float level;
	// Use this for initialization
	void Start () {
		level = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static int GetLevel(){
		return (int)level;
	}

	public static float GetFloatLevel(){
		return level;
	}

	public static void SetFloatLevel(float newlevel){
		level=newlevel;
	}
}
