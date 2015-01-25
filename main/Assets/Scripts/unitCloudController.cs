using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class unitCloudController : MonoBehaviour {

	public GameObject warrior;
	public GameObject mage;
	public GameObject defender;
	public List<GameObject> unitCloud;

	public float spawnSpread;

	public int currentCount;

	void Awake()
	{
		formCloud();
		currentCount = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().unitCount;
	}
	void Update()
	{
		if(currentCount != GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().unitCount)
		{
			formCloud();
			currentCount = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().unitCount;
		}
	}

	void formCloud()
	{
		if (unitCloud.Count > 0)
		{
			foreach(GameObject unit in unitCloud)
			{
				Destroy(unit);
			}
		}

		unitCloud = new List<GameObject>();

		for (int i = 1; i <= GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().warriorCount; i++)
		{
			GameObject newUnit = Instantiate(warrior,new Vector3(Random.Range(transform.position.x + spawnSpread, transform.position.x - spawnSpread), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
			unitCloud.Add(newUnit);
		}
		for (int i = 1; i <= GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().mageCount; i++)
		{
			GameObject newUnit = Instantiate(mage,new Vector3(Random.Range(transform.position.x + spawnSpread, transform.position.x - spawnSpread), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
			unitCloud.Add(newUnit);
		}
		for (int i = 1; i <= GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().defenderCount; i++)
		{
			GameObject newUnit = Instantiate(defender,new Vector3(Random.Range(transform.position.x + spawnSpread, transform.position.x - spawnSpread), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
			unitCloud.Add(newUnit);
		}
	}
}
