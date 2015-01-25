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

	public GameObject goldEmit;

	public int warriorCount;
	public int mageCount;
	public int defenderCount;
	public int gold;

	private List<GameObject> warriors = new List<GameObject>();
	private List<GameObject> mages = new List<GameObject>();
	private List<GameObject> defenders = new List<GameObject>();

	public Vector3 playerPos;
	public Vector3 unitPoint;
	public Vector3 worldSpawnPoint;
	public Vector3 newUnitOffset;
	public Vector3 commanderPos;

	public Vector3 maxForwardVelocity;
	public Vector3 maxReverseVelocity;

	private bool warriorSend = false;
	private bool mageSend = false;
	private bool defenderSend = false;
	private bool buyUnits = false;

	public bool switchState;
	public int state;

	public float introDelay;
	public float roundDelay;
	public float timeSet;
	private bool timeDone;

	public GameObject[] encounterList;
	private List<GameObject[]> encounterGroups;
	public float groupDelay;
	private bool sentGroup = false;
	public int currentEncounter;
	public int encounterUnits;
	public List<GameObject> encounterUnitList;
	private bool hostileEncounter;
	private GameObject encounterCommander;
	void Start()
	{
		switchState = true;
		spawnObjects();
		currentEncounter = 0;
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
		runStates();
		purchaseListener();
		Timer ();
	}
	void Timer()
	{
		timeSet -= Time.deltaTime;
	}

	void loadEncounter(GameObject target)
	{
		encounterUnitList = new List<GameObject>();
		encounterGroups = new List<GameObject[]>();
		encounterCommander = Instantiate(target.GetComponent<encounter>().commander, commanderPos, Quaternion.identity) as GameObject;
		encounterUnitList.Add(encounterCommander);
		groupDelay = target.GetComponent<encounter>().groupDelay;
		encounterUnits = target.GetComponent<encounter>().unitCount;
		hostileEncounter = target.GetComponent<encounter>().hostile;

		foreach(GameObject group in target.GetComponent<encounter>().encounterGroups)
		{
			encounterGroups.Add (group.GetComponent<group>().availableUnits);
		}
	}

	void runStates()
	{
		if(switchState && timeSet <= 0)
		{
			loadEncounter(encounterList[currentEncounter]);
			switchState = false;
		}
		if(encounterUnitList.Count > 0 && !switchState)
		{
			if (sentGroup && encounterGroups.Count > 0)
			{
				sendGroup(encounterGroups[0]);
				encounterGroups.Remove(encounterGroups[0]);
				timeSet = groupDelay;
				sentGroup = false;
			}
			if (!sentGroup && timeSet <= 0)
			{
				sentGroup = true;
			}
		}
		if(encounterUnitList.Count <= 0 && !switchState)
		{
			timeSet = roundDelay;
			switchState = true;
		}

		/*foreach(GameObject unit in encounterUnitList)
		{
			if (unit == null)
			{
				encounterUnitList.Remove(unit);
			}
			Debug.Log (encounterUnitList.Count);
		}*/
	}

	void sendGroup(GameObject[] units)
	{
		Vector3 currentSpawn = worldSpawnPoint;
		foreach(GameObject unit in units)
		{
			GameObject newUnit = Instantiate(unit, currentSpawn, Quaternion.identity) as GameObject;
			newUnit.GetComponent<unitProperties>().currentVelocity = maxReverseVelocity;
			newUnit.GetComponent<unitProperties>().useNewVelocity = true;
			newUnit.GetComponent<unitProperties>().isPlayer = false;
			currentSpawn += newUnitOffset;
			encounterUnitList.Add (newUnit);
		}
	}
	void purchaseListener()
	{
		if(buyUnits && !hostileEncounter && encounterUnitList.Count > 1)
		{
			if(encounterUnitList[1].GetComponent<unitProperties>().unitType == "warrior")
			{
				warriorCount ++;
				Destroy(encounterUnitList[1]);
				encounterUnitList.Remove(encounterUnitList[1]);
			}
			else if(encounterUnitList[1].GetComponent<unitProperties>().unitType == "mage")
			{
				mageCount ++;
				Destroy(encounterUnitList[1]);
				encounterUnitList.Remove(encounterUnitList[1]);
			}
			else if(encounterUnitList[1].GetComponent<unitProperties>().unitType == "defender")
			{
				mageCount ++;
				Destroy(encounterUnitList[1]);
				encounterUnitList.Remove(encounterUnitList[1]);
			}
			goldEmit.GetComponent<ParticleSystem>().Play ();
			buyUnits = false;
		}
	}
	/*void stateListener()
	{
		if(switchState && timeDone)
		{
			createEncounter(state);
			switchState = false;

		}
		if(encounterUnitList.Count <= 0 && !switchState)
		{
			endEncounter (state);
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
	}*/

	void endEncounter(int type)
	{
		if(type == 0)
		{
			state = 1;
		}
		if(type == 1)
		{
			state = 0;
		}
	}
	void sendListener()
	{
		if (hostileEncounter)
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
		if (Input.GetButtonDown("Buy"))
		{
			buyUnits = true;
		}
	}
}
