using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RocketShellExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionObject;
    [SerializeField] private Vector3 collisionRadius;
    [SerializeField] private LayerMask collisionLayer;

    private void Update()
    {
        Collider[] deathCollider = Physics.OverlapBox(transform.position, collisionRadius, transform.rotation, collisionLayer, QueryTriggerInteraction.Ignore);    
        foreach (Collider i in deathCollider)
        {
            Instantiate(explosionObject, transform.root.position, explosionObject.transform.rotation);
            CheckpointLogic.Instance.Respawn(1f);
            transform.root.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, collisionRadius);
    }
}
