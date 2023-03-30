using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;
using UnityEngine.AI;


namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] float chaseDistance = 10f;
        float distanceToPlayer; 
        GameObject player;
        Fighter fighter;

        Health health;

        private void Awake() {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
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
                fighter.Cancel();
            }
        }
        private bool DistanceToPlayer() {
                float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                return distanceToPlayer < chaseDistance;
            }



    }

}
