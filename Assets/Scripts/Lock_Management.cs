using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Lock_Management : MonoBehaviour
    {
        //[SerializeField] private AudioSource lockSound;
        [SerializeField] private GameObject texture;
        [SerializeField] private GameObject player;
        private GameObject colObject;
        public GameObject ColObject { get { return colObject; } }   

        private bool lockCol;

        void Update()
        {
            texture.transform.LookAt(Camera.main.transform);

            if (colObject != null)
            {
                texture.SetActive(true);
                texture.transform.position = colObject.transform.position;
            } else
            {
                texture.SetActive(false);
                lockCol = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((other.gameObject.tag == "Enemy") && !player.GetComponent<PlayerPhysics>().IsGrounded && (!lockCol))
            {
                if (!lockCol)
                {
                    lockCol = true;
                    colObject = other.gameObject;
                    //lockSound.Play();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (!lockCol)
                {
                    lockCol = true;
                    colObject = other.gameObject;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (colObject != null)
            {
                if (colObject.tag == "Enemy")
                {
                    colObject = null;
                    lockCol = false;
                    texture.SetActive(false);
                }
            }
        }
    }
}
