using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject principalMenu;
    [SerializeField] private GameObject levelMenu;
    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
        principalMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
