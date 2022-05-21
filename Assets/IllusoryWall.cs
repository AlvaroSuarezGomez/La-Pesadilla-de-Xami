using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusoryWall : MonoBehaviour
{
    private Renderer rend;


    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(DissipateWall());
        }
    }

    private IEnumerator DissipateWall()
    {
        GetComponent<Collider>().enabled = false;
        while (rend.material.color.a > 0f)
        {
            rend.material.SetColor("_BaseColor", new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, rend.material.color.a - 0.01f));
            yield return new WaitForSeconds(0.01f);
            Debug.Log(rend.material.color.a);
        }
    }
}
