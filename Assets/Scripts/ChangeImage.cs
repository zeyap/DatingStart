using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeImage : MonoBehaviour{
    public int choice = 9;
    private ArrayList spriteList = new ArrayList();
    private Animation anim;
    private GameObject superman;
    private GameObject star;
	private int totalNumber;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        superman = GameObject.Find("superman");
        superman.SetActive(false);
        star = GameObject.Find("star");
        star.SetActive(false);
		totalNumber = readjson.getTotalNumber ();
		for (int i = 0; i <= totalNumber; i++)
        {
            string fileName = "1_" + i.ToString();
            Sprite s = Resources.Load<Sprite>(fileName);
            spriteList.Add(s);
        }
		GetComponent<SpriteRenderer>().sprite = (Sprite)spriteList[totalNumber];
        anim.Play("fadein");
    }
	
	// Update is called once per frame
	void Update () {
		choice=readjson.getResult();
//		Debug.Log("total number" + totalNumber);
		if(choice < totalNumber)
        {	
            Sprite s = (Sprite)spriteList[choice];
            GetComponent<SpriteRenderer>().sprite = s;
        }
    }
}