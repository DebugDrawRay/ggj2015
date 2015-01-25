using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class resultsGold : MonoBehaviour 
{
	void Update () 
	{
		GetComponent<Text>().text = "+" + GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().resultsGold.ToString();
	}
}
