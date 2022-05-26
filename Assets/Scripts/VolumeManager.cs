using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] AudioSource[] audios;
    // Start is called before the first frame update
    public void setVolume(float v)
    {
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].volume= v;
        }
    }
}
