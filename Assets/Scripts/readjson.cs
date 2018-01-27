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
<<<<<<< HEAD
	public int section=LevelManager.GetLevel();
	public ThreholdCollectionFirst collecteddataFirst = new ThreholdCollectionFirst();
=======
	public int section=1;
//	public ThreholdCollectionFirst collecteddataFirst = new ThreholdCollectionFirst();
>>>>>>> origin/master
	public ThreholdCollection collecteddata = new ThreholdCollection();


	public int organNumber = 2;
	public int[] intLevel = new int[4]; 
	public float[] bloodLevel = new float[4]; 
	public static int result = 0;

//	public string gameDataFileName = "Items/threholdFirst.json";
	public List<string> FileName;

//	public string FileName = "Items/threholdFirst.json";

	// Use this for initialization
	void Start () {
		FileName.Add ("Items/threholdFirst.json");
		FileName.Add ("Items/threholdSecond.json");
		FileName.Add ("Items/threholdThird.json");
		LoadGameData();
	}

	// Update is called once per frame
	void Update () {
		result = 0;
		float[] scores = OrganManager.loadScore ();
		if (organNumber == 2) {
			for (int i = 0; i < organNumber; i++) {
//				Debug.Log ("index" + collecteddataFirst.threholds [i].index);
				bloodLevel [i] = scores [collecteddata.threholds [i].index];
				Debug.Log (collecteddata.threholds [i].organ + ": " + bloodLevel [i]);
			}
			reaction (bloodLevel);	

		} else {
			for (int i = 0; i < organNumber; i++) {
				bloodLevel [i] = scores [collecteddata.threholds [i].index];
				Debug.Log (collecteddata.threholds [i].organ + ": " +bloodLevel [i]);
			}
			reaction (bloodLevel);
		} 
	}

	public void LoadGameData()
	{
		// Path.Combine combines strings into a file path
		// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
		string filePath = Path.Combine (Application.dataPath, FileName[section - 1]);
		Debug.Log (filePath);
		if(File.Exists(filePath))
		{
			// Read the json from the file into a string
			string dataAsJson = File.ReadAllText(filePath);
			if (section == 1) {
				JsonUtility.FromJsonOverwrite (dataAsJson, collecteddata);
				organNumber = collecteddata.threholds.Count;
			} else {
				JsonUtility.FromJsonOverwrite (dataAsJson, collecteddata);
				organNumber = collecteddata.threholds.Count;
			}
		}
		else
		{
			Debug.LogError("Cannot load game data!");
		}
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

	void reactionFirst(float[] bloodLevel){
		// brain, stomach, spinalCord, lung
		for (int i = 0; i < organNumber; i++) {
			intLevel [i] = temreturnLevel (bloodLevel [i], collecteddata.threholds[i].lowLevel);
			result += intLevel [i] * (int)Math.Pow (2, organNumber - i - 1);
		}
	}

	void reaction(float[] bloodLevel){
		// brain, stomach, spinalCord, lung
		for (int i = 0; i < organNumber; i++) {
			intLevel [i] = returnLevel (bloodLevel [i], collecteddata.threholds[i].lowLevel, collecteddata.threholds[i].highLevel);
			result += intLevel [i] * (int)Math.Pow (3, organNumber - i - 1);
		}
	}

	public static int getResult(){
		return result;
	}
}
