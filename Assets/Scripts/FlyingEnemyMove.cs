using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMove : MonoBehaviour
{
    [SerializeField]
    Vector3 startposition;
    [SerializeField]
    Vector3 finalposition;

    bool rise;

    [SerializeField]
    int enemyspeed;

    



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
