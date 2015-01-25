using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class resultsTime : MonoBehaviour 
{
	void Update () 
	{
		GetComponent<Text>().text = Mathf.Floor(GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().resultsTime).ToString() + "sec";
	}
}
