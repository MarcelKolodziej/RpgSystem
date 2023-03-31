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
        float timeSinceLastAttack = Mathf.Infinity;
        private void Awake() {
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        private void AttackBehaviour() {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks) {
                // This will trigger the hit event.
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }
        // Animation event 
        void Hit() {
            if(target == null) { return; }
            target.TakeDamage(weaponDamage);
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger("stopAttack");
            _animator.SetTrigger("attack");
        }
        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget) {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        public void Cancel() {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("stopAttack");
        }
    }
    
}
