﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class shieldUI : MonoBehaviour {

	void Update () {
		GetComponent<Text>().text = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().defenderCount.ToString();
	}
}
