using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLogic : MonoBehaviour
{
    public static CheckpointLogic Instance;

    private Transform player;

    private Checkpoint currentCheckpoint;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void Respawn()
    {
        player.position = currentCheckpoint.transform.position;
    }
}
