using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float maxDistance;

    private void Update()
    {
        FollowPlayer();
        //Debug.Log(Vector3.Distance(transform.position, player.position));
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= maxDistance)
        {
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), enemySpeed * Time.deltaTime);
        }
    }
}
