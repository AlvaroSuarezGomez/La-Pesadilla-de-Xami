using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mine : MonoBehaviour
{
    private Color defaultColor;
    private Color newColor;
    private Renderer render;
    private float tiempoExplosion = 1f;
    private float temporaltiempo = 0;
    private int nbucle;
    private bool colorb = false;
    private bool empezar;
    private float tiempoEspera= 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        nbucle = 3;
        defaultColor = gameObject.GetComponent<Renderer>().material.color;
        newColor = Color.red;
        render = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

       if (colorb)
       {
            StartCoroutine(Esperar());
            colorb = false;
       }
        
    }
    void DestroyMine()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //DestroyMine();
            if (!empezar)
            {
                colorb = true;
                empezar = true;

            }
            
        }
    }

    IEnumerator Esperar()
    {


        colorb = false;
        while(nbucle > 0)
        {
            
            temporaltiempo = 0;
            nbucle--;
            render.material.color = newColor;
            yield return new WaitForSeconds(tiempoEspera);
            render.material.color = defaultColor;
            yield return new WaitForSeconds(tiempoEspera*2);
        }
        
        DestroyMine();
        


    }
}