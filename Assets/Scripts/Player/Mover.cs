using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement {
public class Mover : MonoBehaviour, IAction {

    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    Animator _animator;
    Health health;
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    private void Update() {
        navMeshAgent.enabled = !health.IsDead();
        
        UpdateAnimator();
    }

    public void StartMoveAction(Vector3 destination) {
        // When we start Move we cancel fighting
        GetComponent<ActionScheduler>().StartAction(this);
        MoveTo(destination);
    }
     public void MoveTo(Vector3 destination) {
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;
    }

    public void Cancel() {
          navMeshAgent.isStopped = true;
    }

    public void Running() {
        navMeshAgent.speed = 10f;
    }
    private void UpdateAnimator() {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        _animator.SetFloat("walkingSpeed", speed);
    }
}    

}