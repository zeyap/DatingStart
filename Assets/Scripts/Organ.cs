using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Organ : MonoBehaviour {

	const int inis = 50;
	const int maxs = 100;
	const int mins = 0;

	public float score;//0~100
	Color color;
	Button btn;
	public bool chosen;
	public int index;
	public Vector3 chosenPos;
	static Color green=new Color(0.24f,0.70f,0.44f);
	static Color red=new Color(0.86f,0.08f,0.24f);
	static Color yellow=new Color(1.0f,0.84f,0);
	// Use this for initialization

	void Start () {
		score = inis;
		color = green;
		chosen = false;
		btn = gameObject.GetComponent<Button> ();
		btn.onClick.AddListener (OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		Color newColor=(score>inis)?
			Color.Lerp(green,red,(score-inis)/(1.0f*(maxs-inis))):Color.Lerp(green,yellow,(inis-score)/(1.0f*(inis-mins)));
		this.GetComponent<Graphic>().color = newColor;
	}

	public void ChangeScore(float delta){
		score += delta;
		int valence;
		if (delta > 0) {
			valence = 1;
		} else if (delta < 0) {
			valence = -1;
		} else {
			valence = 0;
		}
		transform.DOBlendableScaleBy(new Vector3(0, valence*0.01f, 0), 0.5f);
		//transform.DOPunchScale(new Vector3(0, valence*0.1f, 0),1,20,1);
	}
	void OnClick(){
		if (chosen == false) {
			chosen = true;
			chosenPos = Input.mousePosition;
			OrganManager.RecordNewClick (index);
		}

	}
	void OnNewClick(){
		
	}

	public void Disable(){
		chosen = false;
	}

	public int isCriteriaMet(){
		if (score >= mins&&score <= maxs) {
			return 1;
		} else if (score > maxs) {
			return -1;
		} else if (score < mins) {
			return -2;
		} else {
			return 0;
		}
			
	}
}
