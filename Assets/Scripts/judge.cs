using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  

public class Judge : MonoBehaviour {
	//	private Threhold[] threholds;

	public const int organNumber = 2;
	public float low_level = 70;
	public float high_level = 100;
	public int[] intLevel = new int[organNumber]; 
	public float[] bloodLevel = new float[organNumber]; 

	// Use this for initialization
	void Start () {
		//		LoadGameData ();
	}

	int returnLevel(float ratio){
		if (ratio < low_level) {
			return 0;
		} else if (ratio < high_level) {
			return 1;
		} else {
			return 2;
		}
	}

	int temreturnLevel(float ratio){
		if (ratio < low_level) {
			return 0;
		} else {
			return 1;
		}
	}

	int reaction(float[] bloodLevel){
		// brain, stomach, spinalCord, lung
		for (int i = 0; i < organNumber; i++) {
			intLevel [i] = returnLevel (bloodLevel [i]);
		}
		if (intLevel [0] == 0 && intLevel [1] == 0) {
			Debug.Log ("all are low level");
			return 0;
		} else if (intLevel [0] == 0 && intLevel [1] == 1) {
			Debug.Log ("bone is high");
			return 1;
		} else if (intLevel [0] == 1 && intLevel [1] == 0) {
			Debug.Log ("brain is high");
			return 2;
		} else {
			Debug.Log("all are high level");
			return 3;
		}
	}

	// Update is called once per frame
	void Update () {

		for (int i = 0; i < organNumber; i++) {
			bloodLevel [i] = 20;
		}
		int result = reaction (bloodLevel);
	}
}
