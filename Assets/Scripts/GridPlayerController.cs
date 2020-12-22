using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class GridPlayerController : MonoBehaviour {
    public NavMeshAgent agent;
    public Animator animator;

    private Controls controls;
    private Vector2 mousePosition;
    private bool lastCondition = false;

    private void Start() {
        controls = new Controls();
        controls.UI.Select.performed += Select_performed;
        controls.UI.PointerPosition.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();
        controls.Enable();
    }

    private void Update() {
        if (agent.hasPath != lastCondition) {
            lastCondition = agent.hasPath;
            if (lastCondition) {
                animator.SetTrigger("Walk");
            } else {
                animator.SetTrigger("Stop Walk");
            }
        }
    }

    private void Select_performed(InputAction.CallbackContext obj) {
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            var position = hit.point;
            position.x = Mathf.Round(position.x);
            position.z = Mathf.Round(position.z);
            agent.SetDestination(position);
        }
    }
}