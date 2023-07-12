using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attribiutes;
using RPG.Stats;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider  {

        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapons defaultWeapon = null;
        Animator _animator;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapons currentWeapon = null;

        private void Start() 
        {
        if(currentWeapon == null) 
            {
                EquipWeapon(defaultWeapon);
            }
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
        private void TriggerAttack()
        {
            _animator.ResetTrigger("stopAttack");
            _animator.SetTrigger("attack");
        }

                // Animation Event 
        void Hit() 
        {
            if(target == null) { return; }
            // check if weapon has a projectile
            
            float damage = GetComponent<BaseStats>().GetStat(Stat.WeaponDamage);
            if (currentWeapon.HasProjectile())
            {
                // lunch it
                currentWeapon.LunchProjectile(leftHandTransform, rightHandTransform, target, gameObject, damage);
                // print("animation event");
            }
            else {
                target.TakeDamage(gameObject, damage);
            }
        }

        public Health GetTarget()
        {
            return target;
        } 
        void Shoot() // Animation event is named diffrently in prefabs, so we wrap it into Hit function
        {
            Hit();
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
        public IEnumerable<float> GetAdditiveModifiers(Stat stat) {
            if (stat == Stat.WeaponDamage) {

                yield return currentWeapon.GetWeaponDamage();
            }
        }
        public IEnumerable<float> GetPrecentageModifiers(Stat stat) {
            if (stat == Stat.WeaponDamage) {

                yield return currentWeapon.GetPercentageBonus();
            }
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapons weapon = UnityEngine.Resources.Load<Weapons>(weaponName);
            EquipWeapon(weapon);
        }

    }
    
}
