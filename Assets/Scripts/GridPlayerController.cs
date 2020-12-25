using UnityEngine;
using UnityEngine.AI;

public class GridPlayerController : MonoBehaviour {
    public NavMeshAgent agent;
    public Animator animator;

    private bool lastCondition = false;

    public delegate void FinishMove();
    public event FinishMove OnFinished;

    private void Update() {
        // Set animation
        if (agent.hasPath != lastCondition) {
            lastCondition = agent.hasPath;
            if (lastCondition) {
                animator.SetTrigger("Walk");
            } else {
                animator.SetTrigger("Stop Walk");
                OnFinished.Invoke();
            }
        }
    }
}