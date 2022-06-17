using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xami.Player {
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Properties")]
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private int health;
        public int Health { get { return health; } set { health = value; } }
        [SerializeField] private List<string> damageTags = new List<string>();
        [SerializeField] private List<string> enemyTags = new List<string>();

        [Header("Invincibility")]
        private bool isAttacking = false;
        public bool IsAttacking { get { return isAttacking || attackTime > 0; } set { isAttacking = value; } }
        private bool isInvincible = false;
        public bool IsInvincible { get { return isInvincible; } set { isInvincible = value; } }
        [SerializeField] private float invincibilityTime = 0.5f;
        private float attackTime = 0f;
        public float AttackTime { get { return attackTime; } set { attackTime = value; } }

        [Header("Layers")]
        [SerializeField] private int defaultLayerIndex;
        [SerializeField] private int attackLayerIndex;

        [Header("Components")]
        [SerializeField] private Movement movementScript;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float damageForce;

        private void Awake()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }

            if (movementScript == null)
            {
                movementScript = GetComponent<Movement>();
            }

            health = maxHealth;
        }

        private void Update()
        {
            if (health <= 0)
            {
                Die();
            }

            if (IsAttacking)
            {
                gameObject.layer = attackLayerIndex;
                gameObject.tag = "Hitbox";
            } else
            {
                gameObject.layer = defaultLayerIndex;
                gameObject.tag = "Player";
            }

            attackTime -= Time.deltaTime;
            attackTime = Mathf.Max(0, attackTime);

            //Debug.Log(IsAttacking);
        }

        private void Die()
        {
            LevelManager.Instance.Respawn();
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log(((damageTags.Contains(collision.gameObject.tag) && !IsInvincible) || (enemyTags.Contains(collision.gameObject.tag) && (!IsAttacking && !IsInvincible))));
            //Debug.Log(IsAttacking);
            if ((damageTags.Contains(collision.gameObject.tag) && !IsInvincible) || (enemyTags.Contains(collision.gameObject.tag) && (!IsAttacking && !IsInvincible)))
            {
                Damage();
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            //Debug.Log(((damageTags.Contains(collision.gameObject.tag) && !IsInvincible) || (enemyTags.Contains(collision.gameObject.tag) && (!IsAttacking && !IsInvincible))));
            //Debug.Log(IsAttacking);
            if ((damageTags.Contains(collision.gameObject.tag) && !IsInvincible) || (enemyTags.Contains(collision.gameObject.tag) && (!IsAttacking && !IsInvincible)))
            {
                Damage();
                rb.velocity += ((collision.contacts[0].normal + Vector3.up * 0.1f) * damageForce);
            }
        }

        /*private void OnTriggerStay(Collider collision)
        {
            //Debug.Log(((damageTags.Contains(collision.gameObject.tag) && !IsInvincible) || (enemyTags.Contains(collision.gameObject.tag) && (!IsAttacking && !IsInvincible))));
            //Debug.Log(IsAttacking);
            if ((damageTags.Contains(collision.gameObject.tag) && !IsInvincible) || (enemyTags.Contains(collision.gameObject.tag) && (!IsAttacking && !IsInvincible)))
            {
                isInvincible = true;
                health--;
                movementScript.DisableMovementForTime(0.5f);
                rb.velocity += ((collision.ClosestPoint(transform.position) + Vector3.up * 0.1f) * damageForce/100);
                StartCoroutine(WaitAndDisableInvincibility());
            }
        }*/

        public void Damage()
        {
            isInvincible = true;
            health--;
            movementScript.DisableMovementForTime(0.5f);
            StartCoroutine(WaitAndDisableInvincibility());
        }
        private IEnumerator WaitAndDisableInvincibility()
        {
            yield return new WaitForSeconds(invincibilityTime);
            IsInvincible = false;
        }
    }
}
