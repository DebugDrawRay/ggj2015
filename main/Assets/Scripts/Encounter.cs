using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class encounter : MonoBehaviour {

	public GameObject[] encounterGroups;

	public float groupDelay;
	public int unitCount;
	public bool hostile;
	void Start () 
	{
		foreach(GameObject group in encounterGroups)
		{
			unitCount += group.GetComponent<group>().availableUnits.Length;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
