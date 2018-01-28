using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFart : MonoBehaviour {
    private bool flag;
    private GameObject Fart;
	// Use this for initialization
	void Start () {
        flag = false;
	}
	
	// Update is called once per frame
	void Update () {
        flag = readjson.getIfFart();
        Fart.SetActive(flag);
	}
}
