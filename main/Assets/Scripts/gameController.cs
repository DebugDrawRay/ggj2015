using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour 

{
	public GameObject playerChar;
	public GameObject warrior;
	public GameObject mage;
	public GameObject defender;

	public GameObject enemyWarrior;
	public GameObject enemyMage;
	public GameObject enemyDefender;

	public int warriorCount;
	public int mageCount;
	public int defenderCount;

	private List<GameObject> warriors = new List<GameObject>();
	private List<GameObject> mages = new List<GameObject>();
	private List<GameObject> defenders = new List<GameObject>();

	public Vector3 playerPos;
	public Vector3 unitPoint;
	public Vector3 worldSpawnPoint;
	public Vector3 newUnitOffset;

	public Vector3 maxForwardVelocity;
	public Vector3 maxReverseVelocity;

	private bool warriorSend = false;
	private bool mageSend = false;
	private bool defenderSend = false;

	public bool switchState;
	public int state;

	public float introDelay;
	public float roundDelay;
	public float timeSet;
	private bool timeDone;
	public List<GameObject> encounterUnitList = new List<GameObject>();

	void Start()
	{
		switchState = true;
		spawnObjects();
		//initialize vars
	}

	void spawnObjects()
	{
		Instantiate (playerChar, playerPos, Quaternion.identity);
	}
	void Update()
	{
		inputListener ();
		sendListener ();
		stateListener ();
		Timer ();
	}
	void Timer()
	{
		if (timeSet<=0)
		{
			timeDone = true;
		}
		if (timeSet>= 0)
		{
			timeDone = false;
			timeSet -= Time.deltaTime;
		}
	}

	void stateListener()
	{
		if(switchState && timeDone)
		{
			if (state == 1)
			{
				createEncounter(0);
			}
			if (state == 0)
			{
				createEncounter(1);
			}
			switchState = false;

		}
		if(encounterUnitList.Count <= 0 && !switchState)
		{
			if (state == 1)
			{
				endEncounter(0);
			}
			if (state == 0)
			{
				endEncounter(1);
			}
			timeSet = roundDelay;
			switchState = true;
		}
		foreach(GameObject unit in encounterUnitList)
		{
			if (unit == null)
			{
			encounterUnitList.Remove(unit);
			}
			Debug.Log (encounterUnitList.Count);
		}
	}

	void createEncounter(int type)
	{
		if(type == 0)
		{
			createNewUnits(warrior, 10, false);
		}
		if(type == 1)
		{
			createNewUnits(enemyWarrior, 10, true);
		}
	}

	void createNewUnits(GameObject type, int count, bool hostile)
	{
		encounterUnitList = new List<GameObject>();
		Vector3 currentSpawn = worldSpawnPoint;
		for (int i = 1; i <= 10; i++)
		{
			GameObject newUnit = Instantiate(type, currentSpawn, Quaternion.identity) as GameObject;
			newUnit.GetComponent<unitProperties>().hostile = hostile;
			newUnit.GetComponent<unitProperties>().currentVelocity = maxReverseVelocity;
			newUnit.GetComponent<unitProperties>().useNewVelocity = true;
			newUnit.GetComponent<unitProperties>().isPlayer = false;
			currentSpawn += newUnitOffset;
			encounterUnitList.Add(newUnit);

		}
	}

	void endEncounter(int type)
	{
		if(state == 0)
		{
			state = 1;
		}
		if(state == 1)
		{
			state = 0;
		}
	}
	void sendListener()
	{
		if(warriorSend && warriorCount > 0) 
		{
			warriorCount --;
			sendUnit(warrior, maxForwardVelocity);
			warriorSend = false;
		}
		if(mageSend && mageCount > 0) 
		{
			mageCount --;
			sendUnit(mage, maxForwardVelocity);
			mageSend = false;
		}
		if (defenderSend && defenderCount > 0) 
		{
			defenderCount --;
			sendUnit(defender, maxForwardVelocity);
			defenderSend = false;
		}
	}

	void sendUnit(GameObject target, Vector3 velocity)
	{	
		GameObject newObj = Instantiate (target, unitPoint, Quaternion.identity) as GameObject;
		newObj.gameObject.GetComponent<unitProperties> ().isPlayer = true;
		newObj.gameObject.GetComponent<unitProperties> ().useNewVelocity = true;
		newObj.gameObject.GetComponent<unitProperties>().currentVelocity = velocity;
	}

	void inputListener()
	{
		if (Input.GetButtonDown ("Warriors")) 
		{
			warriorSend = true;
		}
		if (Input.GetButtonDown ("Mages")) 
		{
			mageSend = true;
		}
		if (Input.GetButtonDown ("Defenders")) 
		{
			defenderSend = true;
		}
	}
}
