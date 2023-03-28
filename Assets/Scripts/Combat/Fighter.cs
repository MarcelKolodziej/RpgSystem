using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;
        Health target;
        Animator _animator;
        float timeSinceLastAttack = 0;
        private void Start() {
            _animator = GetComponent<Animator>();
        }
        private void Update() {
            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange()) {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else {
                GetComponent<Mover>().Cancel();
                
                AttackBehaviour();
            }
        }
        private void AttackBehaviour() {
            transform.LookAt(target.transform);
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack > timeBetweenAttacks) {
                // This will trigger the hit event.
                _animator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }
        // Animation event 
        void Hit() {
            target.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel() {
            _animator.SetTrigger("stopAttack");
            target = null;
        }

        public bool CanAttack()
        {
            if (target != null && !target.isDead) {
                return true;
            }
            return false;

        }


    }
}
