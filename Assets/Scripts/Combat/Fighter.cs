using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;
        Transform target;
        Animator _animator;

        float timeSinceLastAttack = 0;
        private void Start() {
            _animator = GetComponent<Animator>();
        }
        private void Update() {
            if (target == null) return;

            if (!GetIsInRange()) {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour() {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack > timeBetweenAttacks) {
                // This will trigger the hit event.
                _animator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        // Animation event 
        void Hit()         {
            Debug.Log("Hit hit!");
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel() {
            target = null;
        }

    }

}
