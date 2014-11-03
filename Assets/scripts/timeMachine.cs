using UnityEngine;
using System.Collections;

public class timeMachine : MonoBehaviour {

	
	void OnTriggerEnter2D(Collider2D collider)
	{

		// If the player hits the trigger.
		if (collider.gameObject.tag == "Player")
		{
			audio.Play();
			Application.LoadLevel ("scene1_past1");

		}
	}


}
