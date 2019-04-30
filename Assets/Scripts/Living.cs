using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Living : MonoBehaviour {
    public Species species = Species.Human;
    public string firstName;
    public string lastName;
	[SerializeField]
	private float health;
	[SerializeField]
	private float maxHealth;

	#region Displays
	private Slider healthSlider;
	#endregion

	private void Awake() {
		healthSlider = GameObject.FindGameObjectWithTag("Health Bar").GetComponent<Slider>();

		healthSlider.maxValue = maxHealth;
	}

	private void Update() {
		healthSlider.value = health;
	}
}

public enum Species {
    Human
}