using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingControl : MonoBehaviour {

	GameObject imgCanvas;
	Text subtext;
	public int endMode;//0-good or 1-bad
	// Use this for initialization
	int spriteNum;
	string prefix;
	const int totalFrame = 90;
	float offset=9;
	Vector3[] positions=new Vector3[totalFrame];
	void Start () {
		imgCanvas = GameObject.Find ("image");
		subtext = GameObject.Find ("subtitle").GetComponent<Text> ();
		for (int i = 0; i < totalFrame; i++) {
			positions [i].x = imgCanvas.transform.position.x;
			positions [i].y = offset*(i-10);
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
			StartCoroutine (Subtitle(i));
			if (i == 1 || i == 2) {
				for (int j = 0; j < 2; j++) {
					imgCanvas.GetComponent<Image> ().sprite = Resources.Load<Sprite> (prefix + (1).ToString ());
					yield return new WaitForSeconds (0.5f);
					imgCanvas.GetComponent<Image> ().sprite = Resources.Load<Sprite> (prefix + (2).ToString ());
					yield return new WaitForSeconds (0.5f);
				}
			} else {
				for(int j=0;j<totalFrame;j++){
					string fname = prefix + i.ToString ();
					Sprite newSprite = Resources.Load<Sprite> (fname);
					imgCanvas.GetComponent<Image> ().sprite = newSprite;
					imgCanvas.transform.position=positions[j];
					yield return new WaitForSeconds (0.1f);
				}
			}
		}
	}
	IEnumerator Subtitle(int spriteNum){
		switch (spriteNum) {
		case 1:
			{	subtext.text = "";
				break;}
		case 2:{
				subtext.text = "策划：一帆 阿包 原浆饭";
				break;}
		case 3:{
				subtext.text = "美术：原浆饭 阿包";
				break;}
		case 4:{
				subtext.text = "程序：pzy 坤坤 新九";
				break;}
		case 5:{
				subtext.text = "Dating Start. GGJ18";
				break;
			}
		default:
			{
				subtext.text = " ";
				break;}
		}

		for (float alpha = 0.1f; alpha <1.0; alpha+=0.1f) {
			subtext.color = new Color (0, 0, 0,alpha);
			yield return new WaitForSeconds (0.1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
