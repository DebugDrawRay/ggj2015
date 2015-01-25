using UnityEngine;
using System.Collections;

public class recruitHouse : MonoBehaviour 
{
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Boundary")
		{
			Destroy(this.gameObject);
		}
	}
}
