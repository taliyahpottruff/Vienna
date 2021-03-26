using Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatEntity : MonoBehaviour {
    public NavMeshAgent agent;
    public NavMeshObstacle obstacle;
    public Animator animator;
    public Weapon equippedWeapon;

    private bool lastCondition = false;
    [SerializeField]
    private List<Cover> surroundingCover = new List<Cover>();

    public delegate void FinishMove();
    public event FinishMove OnFinished;

    private void Start() {
        OnFinished += SearchForCover;
    }

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

    private void SearchForCover() {
        var objects = Physics.OverlapSphere(transform.position, 1f);
        surroundingCover.Clear();
        foreach (var obj in objects) {
            var cover = obj.transform.GetComponent<Cover>();
            if (cover != null) {
                Debug.Log($"{gameObject.name}: Found cover at {obj.transform.position}");
                surroundingCover.Add(cover);
            }
        }
    }
}