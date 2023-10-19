using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Basis : MonoBehaviour
{
    public Slider Base;
    private SentryAction sentryAction;
    public TextMeshProUGUI NotProtect;
    public TextMeshProUGUI Win;
    public Button Restart;
        
    // Start is called before the first frame update
    void Start()
    {
        sentryAction = GameObject.Find("Sentry").GetComponent<SentryAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sentryAction.SentryHp.value == 0)
        {
            NotProtect.gameObject.SetActive(true);
        }

        if (Base.value == 0)
        {
            Win.gameObject.SetActive(true);
            Restart.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paodan") && sentryAction.SentryHp.value == 0)
        {
            Base.value -= 0.1f;
        }
    }
}
