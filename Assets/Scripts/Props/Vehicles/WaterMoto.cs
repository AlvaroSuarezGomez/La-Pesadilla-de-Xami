using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Xami.Player;

namespace Xami.Vehicles
{
    public class WaterMoto : Vehicle
    {
        protected override void Update()
        {
            base.Update();
        }

        private void FixedUpdate()
        {
            if (activated)
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f) + transform.forward * speed;
                Vector2 dir = rotateAction.ReadValue<Vector2>();
                transform.Rotate(new Vector3(0, dir.x, 0) * rotationSpeed * Time.fixedDeltaTime);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.tag == "Player")
            {
                activated = true;
                cam.GetComponent<FixedCamera>().Parent = transform;
                cam.GetComponent<FixedCamera>().lookAtParent = true;
                //cam.GetComponent<FixedCamera>().target = gameObject.transform;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Damage" && !player.GetComponent<PlayerHealth>().IsInvincible)
            {
                player.GetComponent<PlayerHealth>().Damage();
            }
        }
    }
}
