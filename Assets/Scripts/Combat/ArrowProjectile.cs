using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat
{
    public class ArrowProjectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 20f;
        [SerializeField] Transform target = null;

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

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.position;
            }
            return target.position + Vector3.up * targetCapsule.height / 2;
        }

    }
}
