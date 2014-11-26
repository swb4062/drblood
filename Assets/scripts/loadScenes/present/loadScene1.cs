using UnityEngine;
using System.Collections;

public class loadScene1 : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collider)
	{
		// If the player hits the trigger.
		if (collider.gameObject.tag == "Player")
		{
			Application.LoadLevel("scene1");
		}
	}
}
