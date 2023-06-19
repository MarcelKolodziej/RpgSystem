using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Attribiutes;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class ArrowProjectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 20f;
        [SerializeField] private bool isHoming = true;
        [SerializeField] private GameObject impactEffect = null;
        [SerializeField] private float lifeTime = 5f;
        Health target = null;
        GameObject instigator = null;
        float damage = 0f;

        [SerializeField] private GameObject[] destroyOnHit;
        [SerializeField] private float lifeTimeAfterImpact = 5f;

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
     
        public void SetTarget(Health target, GameObject instigator, float damage) 
        { 
            this.target = target;   
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, lifeTime);     
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

        private void OnTriggerEnter(Collider other, GameObject instigator) 
        {
            if(other.GetComponent<Health>() != target) return; // procces if our target, otherwise return
            if(target.isDead) return;
            target.TakeDamage(gameObject, damage);

            if (impactEffect != null) 
            {
                Instantiate(impactEffect, GetAimLocation(), transform.rotation);
            }
            
            foreach (GameObject toDestroy in destroyOnHit) {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeTimeAfterImpact);



        }

    }

}
