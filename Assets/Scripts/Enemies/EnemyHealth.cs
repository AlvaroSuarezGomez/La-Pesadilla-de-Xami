using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private GameObject mainObject;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(mainObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Player")
        {
            health--;
        }
    }
}
