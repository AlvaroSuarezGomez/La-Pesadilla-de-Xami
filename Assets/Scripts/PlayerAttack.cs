using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Xami.Player {
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Collider col;
        [SerializeField] private Movement playerMovement;


        [Header("Animator")]
        [SerializeField] private Animator anim;
        private int AttackIndex = Animator.StringToHash("Attack");

        [Header("Input")]
        [SerializeField] private InputActionReference attackAction;

        [Header("Attack Properties")]
        private bool canAttack = true;
        [SerializeField] private float attackTime;

        private void Awake()
        {
            attackAction.action.performed += Attack_Action_performed;
        }

        private void Attack_Action_performed(InputAction.CallbackContext obj)
        {
            if (playerMovement.CanMove && canAttack)
            {
                col.enabled = true;
                anim.SetTrigger(AttackIndex);
                StartCoroutine(WaitAndDisableAttack());
            }
        }

        private IEnumerator WaitAndDisableAttack()
        {
            canAttack = false;
            yield return new WaitForSeconds(attackTime);
            col.enabled = false;
            canAttack = true;
        }
    }
}
