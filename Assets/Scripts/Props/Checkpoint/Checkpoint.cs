using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool activated = true;

    [SerializeField]
    Renderer rend;

    [SerializeField]
    Texture texture;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && activated)
        {
            LevelManager.Instance.SetCheckpoint(this);
            LevelManager.Instance.rotation = Camera.main.transform.rotation;
            
            activated = false;
        }
    }

    private void Update()
    {
        if (!activated)
        {
            rend.material.mainTexture = texture;
        }
    }
}
