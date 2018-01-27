using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrganManager : MonoBehaviour {
	const int organNum=4;
	public GameObject organPrefab;
	static Transform canvasTrans;
	static GameObject[] organs=new GameObject[organNum];
	static Organ[] organComps=new Organ[organNum];

	static Vector3[] positions = new Vector3[organNum];

	private LineRenderer line;
	public GameObject linePrefab;

	static int chosenCount;
	const int MaxChosenCount = 2;
	static int[] chosenIdx=new int[organNum];

	GameObject meterHand;
	float meterRot;
	static float lastPressTime;
	static float thisPressTime;

	// Use this for initialization

	void OnEnable(){
		organPrefab = Resources.Load ("organ") as GameObject;
		linePrefab = Resources.Load ("line") as GameObject;
	}

	void InitLine(Transform parent){
		line = GameObject.Instantiate (linePrefab, linePrefab.transform.position, transform.rotation).GetComponent<LineRenderer>();
		line.startWidth = 2.0f;
		line.endWidth=2.0f;
	}

	void Start () {
		canvasTrans = GameObject.Find ("Canvas").transform;
		meterHand = GameObject.Find ("meter_hand");
		meterRot = 0;

		for (int i = 0; i < organNum; i++) {
			organs [i] = Instantiate (organPrefab,canvasTrans);
			organComps[i]=organs[i].GetComponent<Organ>();
			organComps[i].index=i;
		}
		InitLine (canvasTrans);

		RefreshOrgans ();

	}

	public static void RefreshOrgans(){
		InitPositions ();
		for (int i = 0; i < organNum; i++) {
			organs [i].transform.position=positions[i];
		}
		chosenCount = 0;
		lastPressTime=0;
		thisPressTime=0;
	}

	static void InitPositions(){
		float x1 = Screen.width/2+225,y1 =Screen.height/2-86,x2=Screen.width/2+366,y2=Screen.height/2-207;
		Vector3 posOut = new Vector3 (999,999,0);

		int level = LevelManager.GetLevel();

		switch (level) {
		case 1:
			{
				positions [0].x =posOut.x;positions [0].y =posOut.y;positions [0].z =10;//heart
				positions [1].x=x2;positions [1].y=y1;positions [1].z =10;//brain
				positions [2].x=posOut.x;positions [2].y=posOut.y;positions [2].z =10;//stomach
				positions [3].x=x2;positions [3].y=y2;positions [3].z =10;//spine
				break;}
		case 2:
			{
				positions [0].x =x1;positions [0].y =y1;positions [0].z =10;//heart
				positions [1].x=x2;positions [1].y=y1;positions [1].z =10;//brain
				positions [2].x=posOut.x;positions [2].y=posOut.y;positions [2].z =10;//stomach
				positions [3].x=x2;positions [3].y=y2;positions [3].z =10;//spine
				break;}
		case 3:
			{
				positions [0].x =x1;positions [0].y =y1;positions [0].z =10;//heart
				positions [1].x=x2;positions [1].y=y1;positions [1].z =10;//brain
				positions [2].x=x1;positions [2].y=y2;positions [2].z =10;//stomach
				positions [3].x=x2;positions [3].y=y2;positions [3].z =10;//spine
				break;}
		}

	}
	
	// Update is called once per frame
	void Update () {
		DrawLine ();
		UpdateScore ();
		if (Input.GetMouseButtonDown(1)) {//right click
			DisableLine ();
		}
//		print ("result: " + readjson.result); 
	}

	IEnumerator MeterRotate(){
		for (int i = 0; i < 3; i++) {
			meterHand.transform.Rotate(0,0,meterRot);
			yield return new WaitForSeconds(0.1f);
			meterHand.transform.Rotate(0,0,-meterRot/4);
			yield return new WaitForSeconds (0.1f);
			meterHand.transform.rotation = Quaternion.Euler (new Vector3(0,0,0));
			yield return new WaitForSeconds (0.1f);
		}

	}

	public static void RecordNewClick(int idx){
		if (chosenCount < MaxChosenCount) {
			chosenIdx [chosenCount] = idx;
			chosenCount++;
		}
	}

	void CalcSpeed (int valence){
		meterRot = 0;
		meterHand.transform.rotation = Quaternion.Euler (new Vector3(0,0,0));
		lastPressTime = thisPressTime;
		thisPressTime = Time.realtimeSinceStartup;
		meterRot=valence*3.0f/(thisPressTime-lastPressTime);
		//Debug.Log (meterRot);
		StartCoroutine (MeterRotate());
	}
		
	void DrawLine(){
		
		line.positionCount=chosenCount;
		for(int j=0;j<chosenCount;j++) {
			
			line.SetPosition(j,organComps[chosenIdx[j]].chosenPos);
		}

	}
	static void DisableLine(){
		for (int j = 0; j <  organNum; j++) {
			organComps[j].Disable();
		}
		chosenCount = 0;
	}

	void UpdateScore (){
		float scoreStep = 1.0f;
		float scoreSum = 0;
		if (chosenCount > 1) {
			if (organComps [0].isCriteriaMet ()>0 && organComps [1].isCriteriaMet ()>0 && organComps [2].isCriteriaMet ()>0 && organComps [3].isCriteriaMet ()>0) {
				float scroll=Input.GetAxis ("Mouse ScrollWheel");
				if (Input.GetKeyDown (KeyCode.A)||scroll>0) {
					organComps [chosenIdx [0]].ChangeScore(-scoreStep * (chosenCount - 1));
					for (int j = 1; j < chosenCount; j++) {
						organComps [chosenIdx [j]].ChangeScore(scoreStep);
					}
					CalcSpeed (1);
				}
				if (Input.GetKeyDown (KeyCode.D)||scroll<0) {
					organComps [chosenIdx [0]].ChangeScore(scoreStep * (chosenCount - 1));
					for (int j = 1; j < chosenCount; j++) {
						organComps [chosenIdx [j]].ChangeScore(-scoreStep);
					}
					CalcSpeed (-1);
				}

			}else{
				ReBalanceScores (scoreStep);
			}

		}
	}

	void ReBalanceScores (float scoreStep){
		float offset=0;
		for (int i = 0; i < organNum; i++) {
			if (organComps [i].isCriteriaMet()==-1) {
				organComps [i].ChangeScore(-scoreStep);
				offset -= scoreStep;
			}
			if (organComps [i].isCriteriaMet()==-2) {
				organComps [i].ChangeScore(scoreStep);
				offset += scoreStep;
			}
		}
		for (int i = 0; i < organNum; i++) {
			organComps [i].ChangeScore(offset/organNum);
		}
	}

	public static float[] loadScore(){
		float[] scores = new float[organNum];
		for (int i = 0; i < organNum; i++) {
			scores [i] = organComps [i].score;
		}
		return scores;
	}

}
