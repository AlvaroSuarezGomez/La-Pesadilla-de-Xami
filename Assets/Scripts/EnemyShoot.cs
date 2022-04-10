using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    private Vector3 playerPosition;
    private enum EnemyState {Track,Shoot,Wait};
    private EnemyState enemyState;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float maxWaitTime;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Wait;
    }

    // Update is called once per frame
    void Update()
    {
        if((Vector3.Distance(transform.position,player.transform.position) <= maxDistance) && (enemyState == EnemyState.Wait))
        {
            enemyState = EnemyState.Track;
            StartCoroutine(WaitAndChangeState());
        }
 
        else if (enemyState == EnemyState.Shoot)
        {
            Shoot();

        }
    }

    private void Shoot()
    {
        var bulletObject = Instantiate(bullet, transform.position, bullet.transform.rotation);
        bulletObject.GetComponent<BulletBehaviour>().Position = playerPosition;
        enemyState = EnemyState.Wait;
    }

    private IEnumerator WaitAndChangeState()
    {
        playerPosition = player.transform.position;
        yield return new WaitForSeconds(maxWaitTime);
        if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            enemyState = EnemyState.Shoot;
        }

    }


}
