using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour {
	[SerializeField]
	private float speed;
	private Vector2 input;

	Player player;
	Rigidbody2D rb;
	Controls controls;

	private void Awake() {
		player = GetComponent<Player>();
		rb = GetComponent<Rigidbody2D>();
		controls = new Controls();
		controls.Player.Move.performed += ctx => input = ctx.ReadValue<Vector2>();
	}

	private void Update() {
		rb.velocity = (GameManager.singleton.paused) ? Vector2.zero : input * speed;
	}

	private void OnEnable() {
		controls.Enable();
	}

	private void OnDisable() {
		controls.Disable();
	}
}