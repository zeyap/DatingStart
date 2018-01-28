using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeImage : MonoBehaviour{
    public int choice;
    private static bool flag;
    private ArrayList spriteListFirst = new ArrayList();
	private ArrayList spriteListSecond = new ArrayList();
	private ArrayList spriteListThird = new ArrayList();
	private ArrayList spriteListThirdGirl = new ArrayList();
    private Animation anim;
    private GameObject superman;
    private GameObject star;
	private GameObject person;
	private int totalNumber;
	private int level;
    private void Awake()
    {
        superman = GameObject.Find("superman");
        superman.SetActive(false);
        star = GameObject.Find("star");
        star.SetActive(false);
<<<<<<< HEAD

=======
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
>>>>>>> a5f8bbadc4dde542d306d54348c8084d9f6ef799
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
		for (int i = 0; i <= 27; i++)
		{
			string fileName = "3_" + i.ToString();
			Sprite s = Resources.Load<Sprite>(fileName);
			spriteListThird.Add(s);
		}
		for (int i = 0; i <= 27; i++)
		{
			string fileName = "31_" + i.ToString();
			Sprite s = Resources.Load<Sprite>(fileName);
			spriteListThirdGirl.Add(s);
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

		if (Timer.GetElapseTime() == 0) {
			if (level == 1) {
				Sprite s = (Sprite)spriteListFirst [totalNumber];
				GetComponent<SpriteRenderer> ().sprite = s;
<<<<<<< HEAD
			} else if (level == 2) {
				Sprite s = (Sprite)spriteListSecond [totalNumber];
				GetComponent<SpriteRenderer> ().sprite = s;
			} else if (level == 3) {
				Sprite s = (Sprite)spriteListThird [totalNumber];
=======
			}
            if(choice == 7)
            {
                flag = true;
            }
		} else if (level == 2) {
			if (choice < totalNumber) {	
				Sprite s = (Sprite)spriteListSecond [choice];
>>>>>>> a5f8bbadc4dde542d306d54348c8084d9f6ef799
				GetComponent<SpriteRenderer> ().sprite = s;
			}
		}else{
			if (level == 1) {
				if (choice < totalNumber) {
					Sprite s = (Sprite)spriteListFirst [choice];
					GetComponent<SpriteRenderer> ().sprite = s;
				}
			} else if (level == 2) {
				if (choice < totalNumber) {	
					Sprite s = (Sprite)spriteListSecond [choice];
					GetComponent<SpriteRenderer> ().sprite = s;
				}	
			} else if (level == 3) {
				if (choice < totalNumber) {	
					Sprite s = (Sprite)spriteListThird [choice];
					GetComponent<SpriteRenderer> ().sprite = s;
				}
			}
		}
	}
}