using UnityEngine;
using System.Collections;

public class unitProperties : MonoBehaviour 
{
	public Vector3 currentVelocity;
	public bool isPlayer;
	public bool useNewVelocity = false;
	public bool hostile;
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
	}
}
