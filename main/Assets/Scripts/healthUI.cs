using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class healthUI : MonoBehaviour 
{

	void Awake()
	{
	}
	void Update () 
	{
		GetComponent<Image>().fillAmount = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<playerCharacter>().health / 1000;
	}
}
