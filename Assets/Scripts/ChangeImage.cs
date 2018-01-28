using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeImage : MonoBehaviour{
    public int choice;
    private static bool flag;
    private ArrayList spriteListFirst = new ArrayList();
	private ArrayList spriteListSecond = new ArrayList();
    private Animation anim;
    private GameObject superman;
    private GameObject star;
	private int totalNumber;
	private int level;
    private void Awake()
    {
        superman = GameObject.Find("superman");
        superman.SetActive(false);
        star = GameObject.Find("star");
        star.SetActive(false);
    }

    public static bool getFlag()
    {
        return flag;
    }

    public static void setFlag(bool f)
    {
        flag = f;
    }

    // Use this for initialization
    void Start () {
        choice = totalNumber;
        flag = false;
        anim = GetComponent<Animation>();
		totalNumber = readjson.getTotalNumber ();
		for (int i = 0; i <= 9; i++)
        {
            string fileName = "1_" + i.ToString();
            Sprite s = Resources.Load<Sprite>(fileName);
            spriteListFirst.Add(s);
        }
		for (int i = 0; i <= 27; i++)
		{
			string fileName = "2_" + i.ToString();
			Sprite s = Resources.Load<Sprite>(fileName);
			spriteListSecond.Add(s);
		}
		GetComponent<SpriteRenderer>().sprite = (Sprite)spriteListFirst[totalNumber];
        anim.Play("fadein");
    }
	
	// Update is called once per frame
	void Update () {
		choice=readjson.getResult();
		level = LevelManager.GetLevel ();
		totalNumber = readjson.getTotalNumber ();
		Debug.Log("result:" + choice);
		if (level == 1) {
			if (choice < totalNumber) {
				Sprite s = (Sprite)spriteListFirst [choice];
				GetComponent<SpriteRenderer> ().sprite = s;
			}
            if(choice == 7)
            {
                flag = true;
            }
		} else if (level == 2) {
			if (choice < totalNumber) {	
				Sprite s = (Sprite)spriteListSecond [choice];
				GetComponent<SpriteRenderer> ().sprite = s;
			}	
		}

    }
}