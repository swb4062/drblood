using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	// TIMER counting DOWN
	static float timer = 600.0f;

	void Update()
	{
		timer -= Time.deltaTime;
		
		if (timer < 0)
		{
			Debug.Log("timer Zero reached !");
			Application.LoadLevel("scene1");
			timer = 600.0f;

		}
	}

}