using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

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

    public void Respawn(float time)
    {
        StartCoroutine(WaitAndRespawn(time));
    }

    private IEnumerator WaitAndRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
