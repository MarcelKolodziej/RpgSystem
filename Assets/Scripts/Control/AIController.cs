using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;
using UnityEngine.AI;
using System;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] float chaseDistance = 10f;
        [SerializeField] float suspictionStateTimer = 3f;
        [SerializeField] float wayPointTolerance = 3f;
        [SerializeField] PatrolPath patrolPath;

        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        float distanceToPlayer; 
        float timeSinceLastSawPlayer = Mathf.Infinity;

        int currentWayPointIndex = 0;
        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        Vector3 guardLocation;

        private void Awake() {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardLocation = transform.position;
        }
        void Update() {
            if (health.IsDead()) return;

            if (DistanceToPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspictionStateTimer)
            {
                Debug.Log(timeSinceLastSawPlayer);
                SuspicionBehaviour();
            }
            else
            {
                //Debug.Log("Patrolling behaviour");
                PatrollingBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }
        private void AttackBehaviour()
        {
            fighter.Attack(player);
            Debug.Log("Attacking player");
            // mover.Running();
        }

        private void PatrollingBehaviour() {
            Vector3 nextPosition = guardLocation;
           // mover.Walking();

            if (patrolPath != null) 
                {
                    if (AtWaypoint())
                    {
                        CycleWaypoint();
                    }
                    nextPosition = GetCurrentWaypoint();
              }
           // Debug.Log(nextPosition);
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWayPointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWayPoint < wayPointTolerance;
            
        }
        private void CycleWaypoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }


        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private bool DistanceToPlayer() {
                float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                return distanceToPlayer < chaseDistance;
            }


        // Called by Unity, when selected
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }

}
