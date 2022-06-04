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
    [SerializeField]
    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parent.transform.LookAt(Position);
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        parent.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
