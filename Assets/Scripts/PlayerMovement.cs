using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
	[SerializeField]
	private float speed;

	Rigidbody2D rb;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		rb.velocity = Input.GetMovementVector() * speed;
	}
}