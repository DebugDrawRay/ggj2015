using UnityEngine;
using System.Collections;

public class unitProperties : MonoBehaviour 
{
	public Vector3 currentVelocity;
	public bool useNewVelocity = false;
	void Update () 
	{
		if (useNewVelocity) 
		{
			rigidbody.velocity = currentVelocity;

		}
	}
}
