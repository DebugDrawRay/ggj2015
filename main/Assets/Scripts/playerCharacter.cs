using UnityEngine;
using System.Collections;

public class playerCharacter : MonoBehaviour {

	public float health;

	void Update () 
	{
		if (health <= 0)
		{
			Destroy(this.gameObject);
			Time.timeScale = 0;
			GameObject.FindGameObjectWithTag("Game Over").GetComponent<RectTransform>().localPosition = Vector2.zero;
		}
	}
}
