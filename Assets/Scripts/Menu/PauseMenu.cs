using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private bool activated;
    [SerializeField] private InputActionReference pauseInputAction;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        pauseInputAction.action.performed += Pause_Action_Performed;
        resumeButton.onClick.AddListener(HideMenu);
        exitButton.onClick.AddListener(Exit);
    }

    private void OnDestroy()
    {
        pauseInputAction.action.performed -= Pause_Action_Performed;
    }

    private void Update()
    {
        Debug.Log("a");
        if (activated)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            pauseCanvas.SetActive(true);
        } else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseCanvas.SetActive(false);
        }
    }

    private void Exit()
    {
        SceneManager.LoadScene(0);
    }

    private void HideMenu()
    {
        activated = false;
    }

    private void Pause_Action_Performed(InputAction.CallbackContext obj)
    {
        Debug.Log("a");
        if (!activated)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (resumeButton != null)
            {
                EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
            }
            activated = true;
        } else
        {
            activated = false;
        }
    }
}
