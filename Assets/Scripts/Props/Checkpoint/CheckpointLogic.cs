using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointLogic : MonoBehaviour
{
    public static CheckpointLogic Instance;

    public Vector3 currentCheckpoint;

    public Quaternion rotation;

    public bool activatedOnce;

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

        DontDestroyOnLoad(Instance);
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        activatedOnce = true;
        currentCheckpoint = checkpoint.transform.position;
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
