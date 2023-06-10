using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapons : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equpipedPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] ArrowProjectile projectile = null;

        const string weaponName = "Weapon";

    
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator) 
        {
            
            DestroyOldWeapon(rightHand, leftHand);
            

            if (equpipedPrefab != null) 
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(equpipedPrefab, handTransform);
                weapon.name = weaponName;
            }


            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null) {
                
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }            
        }

        private static void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            Console.WriteLine("pickup" + oldWeapon);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
                Console.WriteLine("pickup" + oldWeapon);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }


        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public bool HasProjectile() // do we have projectile equipped
        {
            return projectile != null;
        }

        public void LunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            ArrowProjectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        public float GetWeaponDamage() {
            return weaponDamage;
        }
            
        public float GetWeaponRange() {
            return weaponRange;
        }


    }
}
