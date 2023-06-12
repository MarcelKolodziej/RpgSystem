using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Attribiutes;

namespace RPG.Movement {
    public class Mover : MonoBehaviour, IAction, ISaveable {

        [SerializeField] Transform target;
        [SerializeField] float speedFraction = 6f;
        [SerializeField] float maxSpeed = 6f;
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

        public void StartMoveAction(Vector3 destination, float speedFraction) {
            // When we start Move we cancel fighting
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }
        public void MoveTo(Vector3 destination, float speedFraction) {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel() {
            navMeshAgent.isStopped = true;
        }

        // public void Running() {
        //     navMeshAgent.speed = 5f;
        // }

        //  public void Walking() {
        //     navMeshAgent.speed = 2f;
        // }
        private void UpdateAnimator() {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            _animator.SetFloat("walkingSpeed", speed);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
        
    }    

}
