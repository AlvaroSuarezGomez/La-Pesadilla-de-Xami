using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject levelSelector;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject principalMenu;
    [SerializeField] private GameObject optionsFirstButton, levelSelectorFirstButton;
    [SerializeField] private AudioSource audioButton;
    //audioButton.Play();
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(playM);
        optionsButton.onClick.AddListener(optionsM);
        exitButton.onClick.AddListener(exitM);
    }

    // Update is called once per frame
    void playM()
    {
        audioButton.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelSelectorFirstButton);
        principalMenu.SetActive(false);
        levelSelector.SetActive(true);
    }
    void optionsM()
    {
        audioButton.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        principalMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    void exitM()
    {
        audioButton.Play();
        Application.Quit();
    }

}
