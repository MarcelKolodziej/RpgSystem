using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapons defaultWeapon = null;


        Health target;
        Animator _animator;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapons currentWeapon;

        private void Start() {
            EquipWeapon(defaultWeapon);
        }
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
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        public void EquipWeapon(Weapons weapon) {
            currentWeapon = weapon;
            weapon.Spawn(rightHandTransform, leftHandTransform, _animator);
        }

        private void AttackBehaviour() {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks) {
                // This will trigger the hit event.
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }
        void Hit(float damage) {
            if(target == null) { return; }
            target.TakeDamage(currentWeapon.GetWeaponDamage());
        }
        private void TriggerAttack()
        {
            _animator.ResetTrigger("stopAttack");
            _animator.SetTrigger("attack");
        }
        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
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
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("stopAttack");
        }



    }
    
}
