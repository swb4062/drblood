using UnityEngine;
using System.Collections;

public class JetPack : MonoBehaviour {

	private float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private SpriteRenderer spriteRenderer;
	public Sprite jetPackSprite;

	Collision2D coll;

	public PlatformerCharacter2D character;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll) {

				if (coll.gameObject.tag == "Player") {

						GameObject.Destroy (gameObject);
	
						character.rigidbody2D.gravityScale = 1;
						moveDirection.y -= gravity * Time.deltaTime;
						character.airControl = true;
			
						spriteRenderer = character.GetComponent<SpriteRenderer> ();
						spriteRenderer.sprite = jetPackSprite;

				}
	}

}
