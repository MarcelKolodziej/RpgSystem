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

    
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator) 
        {

            if (equpipedPrefab != null) 
            {
                Transform handTransform;
                if (isRightHanded) handTransform = rightHand;
                else handTransform = leftHand; 
                Instantiate(equpipedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }            
        }

          public float GetWeaponDamage() {
            return weaponDamage;
        }
        public float GetWeaponRange() {
            return weaponRange;
        }


    }
}
