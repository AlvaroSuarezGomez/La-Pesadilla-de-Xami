using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button refreshButton;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject principalMenu;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private Toggle audiocheck;
    [SerializeField] private float musicPre;
    [SerializeField] private float sfxPre;
    [SerializeField] private AudioMixer music;
    [SerializeField] private AudioMixer sfx;

    [SerializeField] private GameObject mainMenuFirstButton;
    // Start is called before the first frame update
    void Start()
    {
        musicVolume.value = musicPre;
        sfxVolume.value = sfxPre;
        music.SetFloat("MusicVolume", Mathf.Log10(musicVolume.value) * 20);
        music.SetFloat("SoundVolume", Mathf.Log10(sfxVolume.value) * 20);
        exitButton.onClick.AddListener(returnM);
        refreshButton.onClick.AddListener(refreshM);
        musicVolume.onValueChanged.AddListener(musicM);
        sfxVolume.onValueChanged.AddListener(sfxM);
        audiocheck.onValueChanged.AddListener(audiocheckM);
    }
    void musicM(float value)
    {
        if (audiocheck.isOn)
        {
            music.SetFloat("MusicVolume", Mathf.Log10(musicVolume.value) * 20);
        }
        else
        {
            musicVolume.value = 0;
        }
    }
    void sfxM(float value)
    {
        if (audiocheck.isOn)
        {
            music.SetFloat("SoundVolume", Mathf.Log10(sfxVolume.value) * 20);
        }
        else
        {
            sfxVolume.value = 0;
        }
    }
    void audiocheckM(bool on)
    {
        if (!audiocheck.isOn)
        {
            musicVolume.value = 0;
            sfxVolume.value = 0;
            music.SetFloat("MusicVolume", Mathf.Log10(musicVolume.value) * 20);
            music.SetFloat("SoundVolume", Mathf.Log10(sfxVolume.value) * 20);
        }
    }
    void returnM()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        optionsMenu.SetActive(false);
        principalMenu.SetActive(true);
    }
    void refreshM()
    {
        audiocheck.isOn = true;
        musicVolume.value = musicPre;
        sfxVolume.value = sfxPre;
        music.SetFloat("MusicVolume", Mathf.Log10(musicVolume.value) * 20);
        music.SetFloat("SoundVolume", Mathf.Log10(sfxVolume.value) * 20);

    }
}

