using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 startposition;
    [SerializeField]
    private Vector3 finalposition;

    private bool rise;

    [SerializeField]
    private float enemyspeed;

    



    void Start()
    {
        
        rise = true;

        
    }

    
    void Update()
    {
        if(transform.position.y <= startposition.y)
        {
            rise = true;
        }
        if (transform.position.y >= finalposition.y)
        {
            rise = false;
        }
        if (rise)
        {
            transform.Translate(transform.up * Time.deltaTime * enemyspeed);

        }
        else
        {
            transform.Translate(-transform.up * Time.deltaTime * enemyspeed);
        }
        
    }
}
