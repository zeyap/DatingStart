using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingControl : MonoBehaviour {

	GameObject imgCanvas;
	int endMode=0;//0-good or 1-bad
	// Use this for initialization
	int spriteNum;
	string prefix;
	const int totalFrame = 30;
	float offset=9;
	Vector3[] positions=new Vector3[totalFrame];
	void Start () {
		imgCanvas = GameObject.Find ("image");
		for (int i = 0; i < totalFrame; i++) {
			positions [i].x = imgCanvas.transform.position.x;
			positions [i].y = offset*i;
		}
		if (endMode == 0) {
			spriteNum = 6;
			prefix="ge";
		} else {
			spriteNum = 5;
			prefix="be";
		}
		StartCoroutine (ChangeSprite());
	}

	IEnumerator ChangeSprite(){
		for(int i=1;i<=spriteNum;i++){
			string fname = prefix + i.ToString ();
			Sprite newSprite = Resources.Load<Sprite> (fname);
			imgCanvas.GetComponent<Image> ().sprite = newSprite;
			for(int j=0;j<totalFrame;j++){
				imgCanvas.transform.position=positions[j];
				yield return new WaitForSeconds (0.2f);
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
