using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class resultsMessage : MonoBehaviour 
{
	void Update()
	{
		GetComponent<Text>().text = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().resultsMessage;
	}
}
