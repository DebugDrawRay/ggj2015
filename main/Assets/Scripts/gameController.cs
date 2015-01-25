using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour 

{
	public GameObject playerChar;
	public GameObject warrior;
	public GameObject mage;
	public GameObject defender;
	public GameObject recruitHouse;

	public List<GameObject> fieldedUnits;

	public GameObject enemyWarrior;
	public GameObject enemyMage;
	public GameObject enemyDefender;
	public GameObject commanderCharacter;

	public GameObject goldEmit;
	public int unitCost;

	public int warriorCount;
	public int mageCount;
	public int defenderCount;
	public int gold;

	public int unitCount;

	public Vector3 playerPos;
	public Vector3 unitPoint;
	public Vector3 worldSpawnPoint;
	public Vector3 newUnitOffset;
	public Vector3 commanderPos;
	public Vector3 housePos;

	public Vector3 maxForwardVelocity;
	public Vector3 maxReverseVelocity;

	private bool warriorSend = false;
	private bool mageSend = false;
	private bool defenderSend = false;
	private bool buyUnits = false;

	public bool switchState;
	public int state;
	public bool paused;
	public GameObject pauseScreen;
	public GameObject resultsScreen;

	public Vector2	UIScreenOffset;

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
		paused = false;
		spawnObjects();
		//initialize vars
	}

	void spawnObjects()
	{
		Instantiate (playerChar, playerPos, Quaternion.identity);
	}
	void Update()
	{
		if (!paused)
		{
			inputListener ();
			sendListener ();
			runStates();
			purchaseListener();
			Timer ();
			unitCount = warriorCount + mageCount + defenderCount;
		}
	}
	public void pause()
	{
		if(paused)
		{
			paused = false;
			pauseScreen.transform.position = UIScreenOffset;
			Time.timeScale = 1;
		}
		else
		{
			paused = true;
			pauseScreen.GetComponent<RectTransform>().localPosition = Vector2.zero;
			Time.timeScale = 0;
		}
	}
	public void endGame()
	{
		Application.Quit();
	}
	public void restartGame()
	{
		Application.LoadLevel("main");
		Time.timeScale = 1;
	}
	void Timer()
	{
		timeSet -= Time.deltaTime;
	}

	void loadEncounter(GameObject target)
	{
		showResults(false);
		encounterUnitList = new List<GameObject>();
		fieldedUnits = new List<GameObject>();
		encounterGroups = new List<GameObject[]>();
		groupDelay = target.GetComponent<encounter>().groupDelay;
		encounterUnits = target.GetComponent<encounter>().unitCount;
		hostileEncounter = target.GetComponent<encounter>().hostile;

		if(hostileEncounter)
		{
			encounterCommander = Instantiate(commanderCharacter, commanderPos, Quaternion.identity) as GameObject;
			encounterUnitList.Add(encounterCommander);
		}
		else
		{
			encounterCommander = Instantiate(recruitHouse, housePos, Quaternion.identity) as GameObject;
			encounterUnitList.Add(encounterCommander);
		}

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
		if (encounterUnitList.Count == 1 && fieldedUnits.Count <= 0 && unitCount <= 0)
		{
			Destroy(encounterUnitList[0]);
		}
		if(encounterUnitList.Count <= 0 && !switchState)
		{
			timeSet = roundDelay;
			switchState = true;
			currentEncounter ++;
			if(hostileEncounter)
			{
				showResults(true);
			}

			foreach(GameObject unit in fieldedUnits)
			{
				Destroy(unit);
			}
		}

		foreach(GameObject unit in encounterUnitList)
		{
			if (unit == null)
			{
				encounterUnitList.Remove(unit);
			}
		}
		foreach(GameObject unit in fieldedUnits)
		{
			if (unit == null)
			{
				encounterUnitList.Remove(unit);
			}
		}
	}

	void showResults(bool show)
	{
		if(show)
		{
		resultsScreen.transform.localPosition = Vector2.zero;
		}
		else
		{
			resultsScreen.transform.position = UIScreenOffset;
		}
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
		if(buyUnits && !hostileEncounter && encounterUnitList.Count > 1 && gold >= unitCost)
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
			gold -= unitCost;
			buyUnits = false;
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
		newObj.gameObject.GetComponent<unitProperties>().isPlayer = true;
		newObj.gameObject.GetComponent<unitProperties>().useNewVelocity = true;
		newObj.gameObject.GetComponent<unitProperties>().currentVelocity = velocity;
		fieldedUnits.Add(newObj);
	}

	void inputListener()
	{
		if (hostileEncounter && encounterUnitList.Count > 0)
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
		else if(!hostileEncounter && encounterUnitList.Count > 1)
		{
			if (Input.GetButtonDown("Buy"))
			{
				buyUnits = true;
			}
		}
		if (Input.GetButtonDown("Pause"))
		{
			pause();
		}
	}
}
