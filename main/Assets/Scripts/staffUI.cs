using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class staffUI : MonoBehaviour {

	void Update () {
		GetComponent<Text>().text = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().mageCount.ToString();
	}
}
