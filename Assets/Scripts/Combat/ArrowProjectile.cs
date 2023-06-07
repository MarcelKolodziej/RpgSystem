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

        private void Update() {
            moveToTarget(projectileSpeed);
        }

        private void moveToTarget(float projectileSpeed) {
            if(target == null) return;
             {
                    transform.LookAt(GetAimLocation());
                    transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
            }
        }

        public void SetTarget(Health target) 
        { 
            this.target = target;        
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

    }

}
