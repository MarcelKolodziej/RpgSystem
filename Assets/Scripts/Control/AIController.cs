using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;
using UnityEngine.AI;


namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] float chaseDistance = 10f;
        float distanceToPlayer; 
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
        }

        private void Start() {
            guardLocation = transform.position;
        }
        void Update() {
            if (health.IsDead()) return;

            ChaseThePlayer();
        }

        private void ChaseThePlayer()
        {
            if (DistanceToPlayer() && fighter.CanAttack(player)) 
            {
                fighter.Attack(player);
            } else {
                mover.StartMoveAction(guardLocation);
            }
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
