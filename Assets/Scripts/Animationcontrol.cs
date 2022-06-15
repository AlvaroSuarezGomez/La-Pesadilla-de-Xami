using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animationcontrol : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int numSprites;
    [SerializeField] private Image imagen;
    private int num = 0;
    private float waittime = 0.1f;
    private bool waiting = false;
    // Start is called before the first frame update
    void Start()
    {
        imagen.sprite = sprites[num];
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            waiting = true;
            StartCoroutine(next());
        }
    }
    IEnumerator next()
    {
        yield return new WaitForSeconds(waittime);
        
        if(num >= numSprites-1)
        {
            num = 0;
        }
        else
        {
            num++;
        }
        imagen.sprite = sprites[num];
        waiting = false;
    }
}
