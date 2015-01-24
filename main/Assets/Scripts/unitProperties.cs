using UnityEngine;
using System.Collections;

public class unitProperties : MonoBehaviour 
{
	public Vector3 currentVelocity;
	public bool isPlayer;
	public bool useNewVelocity = false;
	public bool hostile;
	public string oppositionTag;
	public string unitType;
	public float unitHealth;
	void Update () 
	{
		if(!isPlayer && !hostile)
		{
			foreach(Transform child in transform)
			{
				child.gameObject.layer = 11;
			}
		}
		else if(hostile)
		{
			foreach(Transform child in transform)
			{
				child.gameObject.layer = 12;
			}
		}
		else
		{
			foreach(Transform child in transform)
			{
				child.gameObject.layer = 9;
			}
		}
		if (useNewVelocity) 
		{
			rigidbody.velocity = currentVelocity;

		}
		if (unitHealth <=0)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == oppositionTag)
		{
			if(unitType == other.gameObject.GetComponent<unitProperties>().unitType)
			{
				unitHealth --;
				other.gameObject.GetComponent<unitProperties>().unitHealth--;
			}
			else
			{
				if(victoryCheck(unitType, other.gameObject.GetComponent<unitProperties>().unitType))
				{
					other.gameObject.GetComponent<unitProperties>().unitHealth--;
				}
				else
				{
					unitHealth --;
				}
			}
		}

	}


	bool victoryCheck(string type, string oppositionType)
	{
		if(type == "warrior")
		{
			if (oppositionType == "mage")
			{
				return true;
			}
			if (oppositionType == "defender")
			{
				return false;
			}
		}
		if(type == "mage")
		{
			if (oppositionType == "warrior")
			{
				return false;
			}
			if (oppositionType == "defender")
			{
				return true;
			}
		}
		if(type == "defender")
		{
			if (oppositionType == "mage")
			{
				return false;
			}
			if (oppositionType == "warrior")
			{
				return true;
			}
		}
		return false;
	}
}
