using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    bool activated = true;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && activated)
        {
            
            CheckpointLogic.Instance.SetCheckpoint(this);
            activated = false;
        }
    }
}
