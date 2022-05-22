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
        [SerializeField] private List<string> objectTags = new List<string>();
        private HommingAttack hommingAttackScript;
        private GameObject colObject;
        public GameObject ColObject { get { return colObject; } }   

        private bool lockCol;

        private void Awake()
        {
            hommingAttackScript = player.GetComponent<HommingAttack>();
        }

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
            if (objectTags.Contains(other.gameObject.tag) && (!lockCol))
            {
                if (Physics.Raycast(transform.position, (other.transform.position - transform.position), out RaycastHit hit))
                {
                    if (objectTags.Contains(hit.transform.tag))
                    {
                        lockCol = true;
                        colObject = other.gameObject;
                        hommingAttackScript.TargetObject = colObject;
                        //lockSound.Play();
                        return;
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (objectTags.Contains(other.gameObject.tag) && (!lockCol))
            {
                if (Physics.Raycast(transform.position, (other.transform.position - transform.position), out RaycastHit hit))
                {
                    if (objectTags.Contains(hit.transform.tag))
                    {
                        lockCol = true;
                        colObject = other.gameObject;
                        hommingAttackScript.TargetObject = colObject;
                        return;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == colObject)
            {
                colObject = null;
                lockCol = false;
                hommingAttackScript.TargetObject = colObject;
                return;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (objectTags.Contains(collision.gameObject.tag) && (!lockCol))
            {
                if (Physics.Raycast(transform.position, (collision.transform.position - transform.position), out RaycastHit hit))
                {
                    if (objectTags.Contains(hit.transform.tag))
                    {
                        lockCol = true;
                        colObject = collision.gameObject;
                        hommingAttackScript.TargetObject = colObject;
                        //lockSound.Play();
                        return;
                    }
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (objectTags.Contains(collision.gameObject.tag) && (!lockCol))
            {
                if (Physics.Raycast(transform.position, (collision.transform.position - transform.position), out RaycastHit hit))
                {
                    if (objectTags.Contains(hit.transform.tag))
                    {
                        lockCol = true;
                        colObject = collision.gameObject;
                        hommingAttackScript.TargetObject = colObject;
                        return;
                    }
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject == colObject)
            {
                colObject = null;
                lockCol = false;
                hommingAttackScript.TargetObject = colObject;
                return;
            }
        }
    }
}
