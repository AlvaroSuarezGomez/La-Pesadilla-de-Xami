using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavouir : MonoBehaviour
{
    private Vector3 position;
    public Vector3 Position { get { return position; } set { value = position; } }
    [SerializeField]
    private float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
