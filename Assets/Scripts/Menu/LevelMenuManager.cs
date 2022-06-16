using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private GameObject levelSelector;
    [SerializeField] private GameObject principalMenu;
    [SerializeField] private Image[] imagelist;

    [SerializeField] private GameObject mainMenuFirstButton;
    [SerializeField] private AudioSource audioButton;
    //audioButton.Play();
    private int actualscene = 1;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(playM);
        exitButton.onClick.AddListener(returnM);
        leftButton.onClick.AddListener(leftButtonM);
        rightButton.onClick.AddListener(rightButtonM);
    }

    void playM()
    {
        audioButton.Play();
        SceneManager.LoadScene(actualscene);
    }

    void returnM()
    {
        audioButton.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        levelSelector.SetActive(false);
        principalMenu.SetActive(true);
    }
    void leftButtonM()
    {
        audioButton.Play();
        actualscene--;
        if(actualscene < 1)
        {
            actualscene = imagelist.Length;
        }
        for(int i = 0; i<imagelist.Length; i++)
        {
            if(i+1 == actualscene)
            {
                imagelist[i].enabled = true;
            }
            else
            {
                imagelist[i].enabled = false;
            }
        }
    }
    void rightButtonM()
    {
        audioButton.Play();
        actualscene++;
        if (actualscene > imagelist.Length)
        {
            actualscene = 1;
        }
        for (int i = 0; i < imagelist.Length; i++)
        {
            if (i + 1 == actualscene)
            {
                imagelist[i].enabled = true;
            }
            else
            {
                imagelist[i].enabled = false;
            }
        }
    }
}
