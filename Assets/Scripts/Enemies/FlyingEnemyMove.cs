using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 startposition;
    [SerializeField]
    private Vector3 finalposition;

    private bool rise = true;

    [SerializeField]
    private float enemyspeed;
    
    void Update()
    {
        if(transform.localPosition == startposition)
        {
            rise = true;
        }
        if (transform.localPosition == finalposition)
        {
            rise = false;
        }
        if (rise)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, finalposition, enemyspeed * Time.deltaTime);

        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startposition, enemyspeed * Time.deltaTime);
        }
    }
}
