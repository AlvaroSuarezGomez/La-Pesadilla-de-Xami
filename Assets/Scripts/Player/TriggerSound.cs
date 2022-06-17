using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private InputActionReference tauntActionReference;

    private void Awake()
    {
        tauntActionReference.action.performed += Action_performed;
    }

    private void OnDestroy()
    {
        tauntActionReference.action.performed -= Action_performed;
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        audioSource.Play();
    }
}
