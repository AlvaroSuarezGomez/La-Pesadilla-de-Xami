using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    private Vector3 originalPosition;

    [SerializeField] private float speed;

    [SerializeField] private float fallTime;

    [SerializeField] private float riseTime;

    private enum State {
        Stay,
        Fall,
        Rise
    }

    [SerializeField] private State state;

    private void Awake()
    {
        state = State.Stay;
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (state == State.Fall)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }

        else if (state == State.Rise)
        {
            if (transform.position != originalPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            } else
            {
                state = State.Stay;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallTime);
        state = State.Fall;
        yield return new WaitForSeconds(riseTime);
        state = State.Rise;
    }
}
