using UnityEngine;
using System.Collections;

public class dblood_ifshot_fall : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		// If the player hits the trigger.
		if (collider.gameObject.tag == "shot" || collider.gameObject.tag == "Player")
		{
			rigidbody2D.gravityScale = 10;
		}
	}
}
