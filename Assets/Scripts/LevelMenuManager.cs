using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private GameObject levelSelector;
    [SerializeField] private GameObject principalMenu;
    [SerializeField] private Image[] imagelist;
    private int actualscene = 1;
    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(returnM);
        leftButton.onClick.AddListener(leftButtonM);
        rightButton.onClick.AddListener(rightButtonM);
    }

    void returnM()
    {
        levelSelector.SetActive(false);
        principalMenu.SetActive(true);
    }
    void leftButtonM()
    {
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
