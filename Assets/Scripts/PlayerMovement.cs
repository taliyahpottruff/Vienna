using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour {
	[SerializeField]
	private float speed;

	Player player;
	Rigidbody2D rb;

	private void Awake() {
		player = GetComponent<Player>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		rb.velocity = (player.paused) ? Vector2.zero : Input.GetMovementVector() * speed;
	}
}