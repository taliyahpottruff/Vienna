using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour {
	public IInteractable interactable;

	public object Interact() {
		return interactable.Interact();
	}
}