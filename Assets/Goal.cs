using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Xami.Data;
using Xami.Player;
using Xami.Vehicles;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private InputActionReference continueAction;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private bool activated;

    [SerializeField] private List<string> tags = new List<string>();

    [SerializeField] private Timer timer;

    [SerializeField] private GameObject recordMessage;
    [SerializeField] private GameObject returnMessage;

    [SerializeField] private float messageWait = 0.5f;

    private float orScale;

    // Start is called before the first frame update
    private void Awake()
    {
        orScale = transform.localScale.x;
        continueAction.action.performed += Continue_Action_Performed;
    }

    private void Continue_Action_Performed(InputAction.CallbackContext obj)
    {
        if (activated)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (tags.Contains(other.gameObject.tag))
        {
            Debug.Log("Funciona");
            if (other.gameObject.GetComponent<Movement>() != null)
            {
                other.gameObject.GetComponent<Movement>().CanMove = false;
            }

            if (other.gameObject.GetComponent<Vehicle>() != null)
            {
                other.gameObject.GetComponent<Vehicle>().Activated = false;
            }

            timer.activated = false;

            if ((ScoreManager.Instance.Score.scores.Count <= SceneManager.GetActiveScene().buildIndex) || ((timer.LapTime < ScoreManager.Instance.Score.scores[SceneManager.GetActiveScene().buildIndex]) || ScoreManager.Instance.Score.scores[SceneManager.GetActiveScene().buildIndex] <= 0f))
            {
                timer.SaveTime();
                StartCoroutine(ShowMessage());
                activated = true;
            }
            StartCoroutine(EndLevel());
        }
    }

    private IEnumerator EndLevel()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - orScale / 100, transform.localScale.y - orScale / 100, transform.localScale.z - orScale / 100);
            yield return new WaitForSeconds(0.01f);
        }
        if (!activated)
        {
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator ShowMessage()
    {
        returnMessage.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            recordMessage.SetActive(true);
            yield return new WaitForSeconds(messageWait);
            recordMessage.SetActive(false);
            yield return new WaitForSeconds(messageWait);
        }
    }
}
