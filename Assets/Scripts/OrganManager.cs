using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrganManager : MonoBehaviour {
	const int organNum=4;
	public GameObject organPrefab;
	Transform canvasTrans;
	static GameObject[] organs=new GameObject[organNum];
	static Organ[] organComps=new Organ[organNum];

	Vector3[] positions = new Vector3[organNum];

	private LineRenderer line;
	public GameObject linePrefab;

	static int chosenCount;
	const int MaxChosenCount = 2;
	static int[] chosenIdx=new int[organNum];

	GameObject meterHand;
	float meterRot;
	float lastPressTime;
	float thisPressTime;

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
		InitPositions ();
		canvasTrans = GameObject.Find ("Canvas").transform;
		meterHand = GameObject.Find ("meter_hand");
		meterRot = 0;
		for (int i = 0; i < organNum; i++) {
			organs [i] = Instantiate (organPrefab,canvasTrans);
			organs [i].transform.Translate (positions[i]);
			organComps[i]=organs[i].GetComponent<Organ>();
			organComps[i].index=i;
		}
			
		chosenCount = 0;
		lastPressTime=0;
		thisPressTime=0;

		InitLine (canvasTrans);
	}

	void InitPositions(){
		positions [0].x =225;positions [0].y =-17;positions [0].z =10;
		positions [1].x=346;positions [1].y=-17;positions [1].z =10;
		positions [2].x=225;positions [2].y=-138;positions [2].z =10;
		positions [3].x=346;positions [3].y=-138;positions [3].z =10;
	}
	
	// Update is called once per frame
	void Update () {
		DrawLine ();
		UpdateScore ();
		if (Input.GetMouseButtonDown(1)) {//right click
			DisableLine ();
		}
		print (readjson.getResult ());
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
		Debug.Log (meterRot);
		StartCoroutine (MeterRotate());
	}
		
	void DrawLine(){
		
		line.positionCount=chosenCount;
		for(int j=0;j<chosenCount;j++) {
			line.SetPosition(j,organComps[chosenIdx[j]].chosenPos);
		}

	}
	static void DisableLine(){
		for (int j = 0; j < chosenCount; j++) {
			organComps[chosenIdx[j]].Disable();
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
					organComps [chosenIdx [0]].score -= (scoreStep * (chosenCount - 1));
					for (int j = 1; j < chosenCount; j++) {
						organComps [chosenIdx [j]].score += scoreStep;
					}
					CalcSpeed (1);
				}
				if (Input.GetKeyDown (KeyCode.D)||scroll<0) {
					organComps [chosenIdx [0]].score += (scoreStep * (chosenCount - 1));
					for (int j = 1; j < chosenCount; j++) {
						organComps [chosenIdx [j]].score -= scoreStep;
					}
					CalcSpeed (-1);
				}

				/*Debug.Log (organComps [0].score);
				Debug.Log (organComps [1].score);
				Debug.Log (organComps [2].score);
				Debug.Log (organComps [3].score);
				Debug.Log (";");

				*/

			}else{
				for (int i = 0; i < organNum; i++) {
					if (organComps [i].isCriteriaMet()==1) {
						organComps [i].score -= scoreStep;
					}
					if (organComps [i].isCriteriaMet()==2) {
						organComps [i].score += scoreStep;
					}
				}
			}

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
