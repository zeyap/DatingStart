using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;   

[System.Serializable]
public class ThreholdFirst{

	public string organ;
	public int index;
	public float middle;
}
	
[System.Serializable]
public class Threhold{

	public string organ;
	public int index;
	public float lowLevel;
	public float highLevel;
}

[System.Serializable]
public class ThreholdCollectionFirst {

	public List <ThreholdFirst> threholds;
}

[System.Serializable]
public class ThreholdCollection {

	public List <Threhold> threholds;
}

public class readjson : MonoBehaviour {
//	define the class of threhold
	public int section = 1;
	public ThreholdCollectionFirst collecteddataFirst = new ThreholdCollectionFirst();
	public ThreholdCollection collecteddata = new ThreholdCollection();


	public int organNumber = 2;
	public int[] intLevel = new int[4]; 
	public float[] bloodLevel = new float[4]; 
	public static int result = 1;

	public string gameDataFileName = "Items/threholdFirst.json";
//	public string FileName = "Items/threholdFirst.json";

	// Use this for initialization
	void Start () {
		LoadGameData();
	}

	// Update is called once per frame
	void Update () {
		float[] scores = OrganManager.loadScore ();
		if (organNumber == 2) {
			for (int i = 0; i < organNumber; i++) {
//				Debug.Log ("index" + collecteddataFirst.threholds [i].index);
				bloodLevel [i] = scores [collecteddataFirst.threholds [i].index];
			}
			reactionFirst (bloodLevel);	
			Debug.Log (bloodLevel [0]);
			Debug.Log (bloodLevel [1]);
		} else if (organNumber == 3) {
			for (int i = 0; i < organNumber; i++) {
				bloodLevel [i] = scores [collecteddata.threholds [i].index];
			}
			reactionSecond (bloodLevel);
		} else {
			for (int i = 0; i < organNumber; i++) {
				bloodLevel [i] = scores [collecteddata.threholds [i].index];
			}
			reactionThird (bloodLevel);
		}
	}

	public void LoadGameData()
	{
		// Path.Combine combines strings into a file path
		// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
		string filePath = Path.Combine (Application.dataPath, gameDataFileName);
		Debug.Log (gameDataFileName);
		if(File.Exists(filePath))
		{
			// Read the json from the file into a string
			string dataAsJson = File.ReadAllText(filePath);
			if (section == 1) {
				JsonUtility.FromJsonOverwrite (dataAsJson, collecteddataFirst);
				organNumber = collecteddataFirst.threholds.Count;
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
			intLevel [i] = temreturnLevel (bloodLevel [i], collecteddataFirst.threholds[i].middle);
		}
		if (intLevel [0] == 0 && intLevel [1] == 0) {
			Debug.Log ("Sleepy and casual suit");
			result = 0;
		} else if (intLevel [0] == 0 && intLevel [1] == 1) {
			Debug.Log ("Sleepy and super star eating salted egg ");
			result = 1;
		} else if (intLevel [0] == 1 && intLevel [1] == 0) {
			Debug.Log ("Excited and casual suit");
			result = 2;
		} else {
			Debug.Log("Excited and super star eating salted egg");
			result = 3;
		}
	}

	void reactionSecond(float[] bloodLevel){
		// brain, stomach, spinalCord, lung
		for (int i = 0; i < organNumber; i++) {
			intLevel [i] = returnLevel (bloodLevel [i], collecteddata.threholds[i].lowLevel, collecteddata.threholds[i].highLevel);
		}
		if (intLevel [0] == 0 && intLevel [1] == 0 && intLevel [2] == 0) {
			Debug.Log ("Nervous and hug with both arms and stand");
			result = 0;
		} else if (intLevel [0] == 0 && intLevel [1] == 0 && intLevel [2] == 1) {
			Debug.Log ("Nervous and hug with both arms and stand by one foot like a bird");
			result = 1;
		} else if (intLevel [0] == 1 && intLevel [1] == 0 && intLevel [2] == 2) {
			Debug.Log ("Nervous and hug with both arms and sit on the floor");
			result = 2;
		} else if (intLevel [0] == 1 && intLevel [1] == 1 && intLevel [2] == 0){
			Debug.Log("Excited and super star eating salted egg");
			result = 3;
		}
	}

	void reactionThird(float[] bloodLevel){
		// brain, stomach, spinalCord, lung
		for (int i = 0; i < organNumber; i++) {
			intLevel [i] = returnLevel (bloodLevel [i], collecteddata.threholds[i].lowLevel, collecteddata.threholds[i].highLevel);
		}
		if (intLevel [0] == 0 && intLevel [1] == 0) {
			Debug.Log ("Sleepy and casual suit");
			result = 0;
		} else if (intLevel [0] == 0 && intLevel [1] == 1) {
			Debug.Log ("Sleepy and super star eating salted egg ");
			result = 1;
		} else if (intLevel [0] == 1 && intLevel [1] == 0) {
			Debug.Log ("Excited and casual suit");
			result = 2;
		} else {
			Debug.Log("Excited and super star eating salted egg");
			result = 3;
		}
	}

	public static int getResult(){
		return result;
	}
}
