using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusoryWall : MonoBehaviour
{
    private Renderer rend;

    [SerializeField] private int hitNumber = 1;


    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (hitNumber <= 0)
        {
            StartCoroutine(DissipateWall());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hitNumber--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hitNumber--;
        }
    }

    private IEnumerator DissipateWall()
    {
        GetComponent<Collider>().enabled = false;
        while (rend.material.color.a > 0f)
        {
            rend.material.SetColor("_BaseColor", new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, rend.material.color.a - 0.01f));
            yield return new WaitForSeconds(0.01f);
        }
    }
}
