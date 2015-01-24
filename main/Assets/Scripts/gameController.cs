using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour 

{
	public GameObject playerChar;
	public GameObject warriorPoint;
	public GameObject magePoint;
	public GameObject defenderPoint;

	private GameObject warrior;
	private GameObject mage;
	private GameObject defender;

	public Vector3 playerPos;
	public Vector3 warriorOffset;
	public Vector3 mageOffset;
	public Vector3 defenderOffset;

	public Vector3 maxForwardVelocity;
	private Vector3 currentForwardVelocity;

	private bool warriorSend = false;
	private bool mageSend = false;
	private bool defenderSend = false;
	private bool warriorSent = false;
	private bool mageSent = false;
	private bool defenderSent = false;

	void Start()
	{
		spawnObjects();
		//initialize vars
		currentForwardVelocity = maxForwardVelocity;
	}

	void spawnObjects()
	{
		Instantiate (playerChar, playerPos, Quaternion.identity);
		warrior = Instantiate (warriorPoint, playerPos - warriorOffset, Quaternion.identity) as GameObject;
		mage = Instantiate (magePoint, playerPos - mageOffset, Quaternion.identity) as GameObject;
		defender = Instantiate (defenderPoint, playerPos - defenderOffset, Quaternion.identity)as GameObject;

	}
	void Update()
	{
		inputListener ();
		sendListener ();

	}

	void sendListener()
	{
		if(warriorSend && !warriorSent) 
		{
			warriorSent = true;
			sendUnit(warrior);
		}
		if(mageSend && !mageSent) 
		{
			mageSent = true;
			sendUnit(mage);
		}
		if (defenderSend && !defenderSent) 
		{
			defenderSent = true;
			sendUnit(defender);
		}
	}

	void sendUnit(GameObject target)
	{
		target.gameObject.GetComponent<unitProperties> ().useNewVelocity = true;
		target.gameObject.GetComponent<unitProperties>().currentVelocity = maxForwardVelocity;
	}

	void inputListener()
	{
		if (Input.GetButtonDown ("Warriors")) 
		{
			warriorSend = true;
		}
		if (Input.GetButtonUp ("Warriors")) 
		{
			warriorSent = false;
		}
		if (Input.GetButtonDown ("Mages")) 
		{
			mageSend = true;
		}
		if (Input.GetButtonUp ("Mages")) 
		{
			mageSent = false;
		}
		if (Input.GetButtonDown ("Defenders")) 
		{
			defenderSend = true;
		}
		if (Input.GetButtonUp ("Defenders")) 
		{
			defenderSent = false;
		}
	}
}
