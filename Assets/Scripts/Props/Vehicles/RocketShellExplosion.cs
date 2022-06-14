using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Xami.Vehicles
{
    public class RocketShellExplosion : MonoBehaviour
    {
        [SerializeField] private GameObject explosionObject;
        [SerializeField] private Transform parent;
        [SerializeField] private Vector3 collisionRadius;
        [SerializeField] private LayerMask collisionLayer;

        private void Update()
        {
            if (!parent.GetComponent<RocketShell>().IsJumping)
            {
                Collider[] deathCollider = Physics.OverlapBox(transform.position, collisionRadius, transform.rotation, collisionLayer, QueryTriggerInteraction.Ignore);
                foreach (Collider i in deathCollider)
                {
                    Instantiate(explosionObject, parent.position, explosionObject.transform.rotation);
                    LevelManager.Instance.Respawn(1f);
                    transform.root.gameObject.SetActive(false);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, collisionRadius);
        }
    }
}
