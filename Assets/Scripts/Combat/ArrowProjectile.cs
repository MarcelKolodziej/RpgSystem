using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class ArrowProjectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 20f;
        Health target = null;
        float damage = 0f;

        [SerializeField] private bool isHoming = true;
        
        private void Start() {
                transform.LookAt(GetAimLocation());
        }
        private void Update()
            {
                if (target is null) return;
                if (isHoming)
                {
                    if (!target.IsDead())
                    {
                        transform.LookAt(GetAimLocation());
                    }
                }
                transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);      
            }
     
        public void SetTarget(Health target, float damage) 
        { 
            this.target = target;   
            this.damage = damage;     
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.GetComponent<Health>() != target) return; // procces if our target, otherwise return
                if(target.isDead) return;
                target.TakeDamage(damage);
                Destroy(gameObject);

        }

    }

}
