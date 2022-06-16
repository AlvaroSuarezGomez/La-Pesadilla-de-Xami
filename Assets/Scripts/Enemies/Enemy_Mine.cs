using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Enemy_Mine : MonoBehaviour
{
    private Color defaultColor;
    private Color newColor;
    [SerializeField] private Renderer render;
    private float tiempoExplosion = 1f;
    private float temporaltiempo = 0;
    private int nbucle;
    private bool colorb = false;
    private bool empezar;
    private float tiempoEspera= 0.1f;
    [SerializeField] private GameObject explosionObject;
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 collisionRadius;
    [SerializeField] private AudioSource beep;
    [SerializeField] private float explosionRadius;
    private SphereCollider colliderThis;
    // Start is called before the first frame update
    void Start()
    {
        nbucle = 3;
        //render = gameObject.GetComponent<Renderer>();
        defaultColor = render.materials[1].color;
        newColor = new Color(139, 0, 0);
        colliderThis = GetComponent<SphereCollider>();

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
   
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")|| other.gameObject.CompareTag("Vehicle"))
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
            render.materials[1].color = newColor;
            beep.Play();
            yield return new WaitForSeconds(tiempoEspera);
            render.materials[1].color = defaultColor;
            yield return new WaitForSeconds(tiempoEspera*2);
        }
        GameObject explosion = Instantiate(explosionObject, parent.position, explosionObject.transform.rotation);
        
        colliderThis.tag = "Damage";
        while (colliderThis.radius < explosionRadius)
        {
            colliderThis.radius+=explosionRadius/10;
            yield return new WaitForSeconds(Time.deltaTime*5f);
        }
        Destroy(explosion, 1f);
        Destroy(gameObject);
        


    }
}

/*
 * namespace Xami.Vehicles
{
    public class RocketShellExplosion : MonoBehaviour
    {
        [SerializeField] private GameObject explosionObject;
        [SerializeField] private Transform parent;
        [SerializeField] private Vector3 collisionRadius;
        [SerializeField] private LayerMask collisionLayer;

        private void Update()
        {
            if (!parent.GetComponent<RocketShell>().IsJumping)
            {
                Collider[] deathCollider = Physics.OverlapBox(transform.position, collisionRadius, transform.rotation, collisionLayer, QueryTriggerInteraction.Ignore);
                foreach (Collider i in deathCollider)
                {
                    Instantiate(explosionObject, parent.position, explosionObject.transform.rotation);
                    LevelManager.Instance.Respawn(1f);
                    transform.root.gameObject.SetActive(false);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, collisionRadius);
        }
    }
}*/