using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class LivingAnimator : MonoBehaviour {
	public List<Sprite> upSprites, downSprites, sideSprites;
	
	private SpriteRenderer sr;
	private Rigidbody2D rb;

	private void Awake() {
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		Vector2 direction = rb.velocity.normalized;
		if (!direction.Equals(Vector2.zero)) { //First check if movement is even happening
			if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) || Mathf.Abs(direction.x) == Mathf.Abs(direction.y)) { //Sideways movement
				if (direction.x > 0) {
					Debug.Log("Right");
				} else {
					Debug.Log("Left");
				}
			} else { //Vertical movement
				if (direction.y > 0) {
					Debug.Log("Up");
				} else {
					Debug.Log("Down");
				}
			}
		}
	}
}