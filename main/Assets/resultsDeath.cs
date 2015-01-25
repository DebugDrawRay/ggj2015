using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class resultsDeath : MonoBehaviour 
{

	void Update () {
		GetComponent<Text>().text = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().resultsDeath.ToString();
	}
}
