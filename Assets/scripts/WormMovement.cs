using UnityEngine;
using System.Collections;

public class WormMovement : MonoBehaviour {

	public Vector3 speed = new Vector3(0,5.0F,0);

	void Start () {
		
	}

	void Update () {
		if (renderer.isVisible) {
		} else {
			//speed = -speed;
			transform.Rotate(0,0, 180);
		}
		transform.Translate(speed * Time.deltaTime);
	}
}
