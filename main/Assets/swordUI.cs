﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class swordUI : MonoBehaviour {

	void Update () {
		GetComponent<Text>().text = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().warriorCount.ToString();
	}
}
