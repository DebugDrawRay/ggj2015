using UnityEngine;
using System.Collections;

public class backgroundEngine : MonoBehaviour 
{
	public float scrollSpeed;
	void Update () 
	{
		transform.Translate(-Vector3.right * scrollSpeed * Time.deltaTime);
	}
}
