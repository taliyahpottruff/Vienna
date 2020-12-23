using UnityEngine;
using UnityEngine.AI;

public class GridPlayerController : MonoBehaviour {
    public NavMeshAgent agent;
    public Animator animator;

    private bool lastCondition = false;

    private void Update() {
        // Set animation
        if (agent.hasPath != lastCondition) {
            lastCondition = agent.hasPath;
            if (lastCondition) {
                animator.SetTrigger("Walk");
            } else {
                animator.SetTrigger("Stop Walk");
            }
        }
    }
}