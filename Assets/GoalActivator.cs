using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalActivator : MonoBehaviour
{
    [SerializeField] private RaceGoal goal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            goal.activated = true;
        }
    }
}
