using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] private List<string> tags = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (tags.Contains(other.gameObject.tag))
        {
            LevelManager.Instance.Respawn();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (tags.Contains(other.gameObject.tag))
        {
            LevelManager.Instance.Respawn();
        }
    }
}
