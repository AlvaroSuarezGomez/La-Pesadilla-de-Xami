using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] 
    List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private GameObject door;

    [SerializeField] private Transform doorTarget;

    [SerializeField] private float doorSpeed;

    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++) { 
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }

        if (enemies.Count <= 0 && (door.transform.position != doorTarget.position))
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorTarget.position, doorSpeed * Time.deltaTime);
        }
    }
}
