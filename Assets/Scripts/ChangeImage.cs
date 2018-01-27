using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeImage : MonoBehaviour{
    public int choice = 0;
    private ArrayList spriteList = new ArrayList();
    private Animation anim;
    private GameObject superman;
    private GameObject star;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        superman = GameObject.Find("superman");
        superman.SetActive(false);
        star = GameObject.Find("star");
        star.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            string fileName = "per" + i.ToString();
            Sprite s = Resources.Load<Sprite>(fileName);
            spriteList.Add(s);
        }
        GetComponent<SpriteRenderer>().sprite = (Sprite)spriteList[0];
        anim.Play("fadein");
    }
	
	// Update is called once per frame
	void Update () {
		choice=readjson.getResult();
        if(choice < 4)
        {
            Sprite s = (Sprite)spriteList[choice];
            GetComponent<SpriteRenderer>().sprite = s;
        }
        if (choice == 1)
            anim.Play("shake");
        if(choice == 4)
        {
            GameObject person = GameObject.Find("person");
            person.SetActive(false);
            superman.SetActive(true);
            star.SetActive(true);
        }
    }
}