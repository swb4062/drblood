using UnityEngine;
using System.Collections;

public class engageSlippery : MonoBehaviour {

	public PlatformerCharacter2D character;
	bool startSlippery = false;
	bool isSliding = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (character.hasJetPack) {
			character.rigidbody2D.gravityScale = 1;
			isSliding = false;
			character.anim.SetBool("isSliding", isSliding);
		}
		if (character.hasJetPack = false && startSlippery)
			character.rigidbody2D.gravityScale = 50;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			startSlippery = true;
			character.rigidbody2D.gravityScale = 50;
			isSliding = true;
			character.anim.SetBool("isSliding", isSliding);
		}

	}



}
