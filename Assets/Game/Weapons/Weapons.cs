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
    
        public void Spawn(Transform handTransform, Animator animator) 
        {
            if (equpipedPrefab != null){
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
