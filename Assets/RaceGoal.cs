using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceGoal : MonoBehaviour
{
    public bool activated;

    [SerializeField] private Timer timer;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Vehicle") && activated)
        {
            timer.SaveTime();
            timer.ResetTimer();
            activated = false;
        }
    }
}
