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
    private float actualtime;
    // Start is called before the first frame update
    void Start()
    {
        imagen.sprite = sprites[num];
        waiting = false;
        actualtime = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            waiting = true;
            actualtime = 0;
        } else
        {
            if(actualtime >= waittime)
            {
                actualtime = 0;
                waiting = false;
                StartCoroutine(next());
            }
            else
            {
                actualtime += Time.deltaTime;
            }
        }
    }
    IEnumerator next()
    {
        yield return null;
        
        if(num >= numSprites-1)
        {
            num = 0;
        }
        else
        {
            num++;
        }
        imagen.sprite = sprites[num];
    }
}
