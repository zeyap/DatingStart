using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;   
using System;

//[System.Serializable]
//public class ThreholdFirst{
//
//	public string organ;
//	public int index;
//	public float middle;
//}
	
[System.Serializable]
public class Threhold{

	public string organ;
	public int index;
	public float lowLevel;
	public float highLevel;
}

//[System.Serializable]
//public class ThreholdCollectionFirst {
//
//	public List <ThreholdFirst> threholds;
//}

[System.Serializable]
public class ThreholdCollection {

	public List <Threhold> threholds;
}

public class readjson : MonoBehaviour {
//	define the class of threhold
	public int section;
	public List<ThreholdCollection> collecteddatas;

	public int organNumber = 2;
	public int[] intLevel = new int[4]; 
	public float[] bloodLevel = new float[4]; 
	public static int result = 0;
	public static int totalNumber = 0;

//	public string gameDataFileName = "Items/threholdFirst.json";
	public List<string> FileName;
	public List <string> filePaths;

//	public string FileName = "Items/threholdFirst.json";

	// Use this for initialization
	void Start () {
		FileName.Add ("Items/threholdFirst.json");
		FileName.Add ("Items/threholdSecond.json");
		FileName.Add ("Items/threholdThird.json");
		section = LevelManager.GetLevel() - 1;
		LoadGameData();
	}

	// Update is called once per frame
	void Update () {
		result = 0;
		section = LevelManager.GetLevel() - 1;
		organNumber = collecteddatas[section].threholds.Count;
		totalNumber = (int)Math.Pow (3, organNumber);
		float[] scores = OrganManager.loadScore ();
		if (organNumber == 2) {
			for (int i = 0; i < organNumber; i++) {
//				Debug.Log ("index" + collecteddataFirst.threholds [i].index);
				bloodLevel [i] = scores [collecteddatas[section].threholds [i].index];
//				Debug.Log (collecteddatas[section].threholds [i].organ + ": " + bloodLevel [i]);
			}
			reaction (bloodLevel);	

		} else {
			for (int i = 0; i < organNumber; i++) {
				bloodLevel [i] = scores [collecteddatas[section].threholds [i].index];
//				Debug.Log (collecteddatas[section].threholds [i].organ + ": " + bloodLevel [i]);
			}
			reaction (bloodLevel);
		} 

	}

	public void LoadGameData()
	{
		// Path.Combine combines strings into a file path
		// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
		for (int i = 0; i < 3; i++) {
			filePaths.Add(Path.Combine(Application.dataPath, FileName[i]));
			Debug.Log (filePaths[i]);
			if(File.Exists(filePaths[i])){
				// Read the json from the file into a string
				string dataAsJson = File.ReadAllText(filePaths[i]);
				ThreholdCollection collecteddata = new ThreholdCollection ();
				JsonUtility.FromJsonOverwrite (dataAsJson, collecteddata);
				collecteddatas.Add (collecteddata);
			} else{
				Debug.LogError("Cannot load game data!");
			}
		}
		organNumber = collecteddatas[section].threholds.Count;
		totalNumber = (int)Math.Pow (3, organNumber);
	}

	int temreturnLevel(float ratio, float middle){
		if (ratio < middle) {
			return 0;
		} else {
			return 1;
		}
	}

	int returnLevel(float score, float lowLevel, float highLevel){
		if (score < lowLevel) {
			return 0;
		} else if (score < highLevel) {
			return 1;
		} else {
			return 2;
		}
	}

//	void reactionFirst(float[] bloodLevel){
//		// brain, stomach, spinalCord, lung
//		for (int i = 0; i < organNumber; i++) {
//			intLevel [i] = temreturnLevel (bloodLevel [i], collecteddata.threholds[i].lowLevel);
//			result += intLevel [i] * (int)Math.Pow (2, organNumber - i - 1);
//		}
//	}

	void reaction(float[] bloodLevel){
		// brain, stomach, spinalCord, lung
		for (int i = 0; i < organNumber; i++) {
			intLevel [i] = returnLevel (bloodLevel [i], collecteddatas[section].threholds[i].lowLevel, collecteddatas[section].threholds[i].highLevel);
			result += intLevel [i] * (int)Math.Pow (3, organNumber - i - 1);
		}
	}
		
	public static int getResult(){
		return result;
	}

	public static int getTotalNumber(){
		return totalNumber;
	}
}
