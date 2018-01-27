using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	static float elapseTime;
	static float startTime;

	// Use this for initialization
	void Start () {
		Init ();
		StartCoroutine (CalcElapseTime());
	}

	public static void Init(){
		startTime = Time.realtimeSinceStartup;
		elapseTime = 0;
	}

	public static float GetElapseTime(){
		return elapseTime;
	}

	IEnumerator CalcElapseTime(){
		while (true) {
			elapseTime = Time.realtimeSinceStartup - startTime;
			yield return new WaitForSeconds (0.2f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
