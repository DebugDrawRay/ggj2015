using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class goldUI : MonoBehaviour 
{
	void Update () {
		GetComponent<Text>().text = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().gold.ToString();
	}
}
