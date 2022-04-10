using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Vector3 position;
    public Vector3 Position { get { return position; } set { position = value; } }
    [SerializeField]
    private float destroyTime;
    [SerializeField] 
    private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, position, bulletSpeed * Time.deltaTime);
        Debug.Log(position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
