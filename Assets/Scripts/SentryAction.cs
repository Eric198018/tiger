using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SentryAction : MonoBehaviour
{

    private float speed = 5f;
    private float speed2 = 50f;
    private bool isDetected1 = false;
    private bool onWork = true;
    private Controller _controller;
    

    public GameObject RayPoint1;
    public GameObject Standard;
    public GameObject shootPoint;
    

    
    public Slider SentryHp;

    public Transform upHolder;
    public Transform pitch1;
    public Transform upHolderRp;
    public Transform Getangle;

    public Rigidbody Paodan;

    public TextMeshProUGUI Detect;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0, 1f);
        _controller = GameObject.Find("Standard").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(_controller.Standard.value);
        if (transform.position.x> 9 && onWork == true)
        {
            
            speed = 5f;
        }
        else if (transform.position.x < -9 && onWork == true)
        {
            speed = -5f;
        }
        
        
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed );
        
        RayDetect();
        pitch1Follow();

        if (SentryHp.value == 0  || _controller.Standard.value == 0)
        {
            onWork = false;
        }

        if (onWork == false)
        {
            speed = 0;
        }

        if (isDetected1 == true)
        {
            Detect.gameObject.SetActive(true);
        }

        else if(isDetected1 == false)
        {
            Detect.gameObject.SetActive(false);
        }






    }

    void RayDetect()
    {
        RaycastHit hitInfo;
        if ((Standard.transform.position.z - RayPoint1.transform.position.z) > 5 && onWork == true )
        {
            if (Physics.Raycast(RayPoint1.transform.position, Standard.transform.position - RayPoint1.transform.position,
                    out hitInfo, 40f))
            {
                isDetected1 = true;
                
            }

            else
            {
                isDetected1 = false;
            }
        }

        else
        {
            isDetected1 = false;
        }
        
        
    }
    
    
    void pitch1Follow()
    {
        if (isDetected1 == true && onWork == true )
        {
            Vector3 targetDir1 = Getangle.position - upHolderRp.position;
            Quaternion rotation1 = Quaternion.LookRotation(targetDir1, Vector3.up);
            upHolder.rotation = rotation1;
            upHolder.right = upHolder.forward;

            Vector3 targetDir2 = Standard.transform.position - upHolderRp.position;
            Quaternion rotation2 = Quaternion.LookRotation(targetDir2, Vector3.right);
            pitch1.rotation = rotation2;
            pitch1.forward = -pitch1.forward;
        }
    }

    void Shoot()
    {
        if (isDetected1 == true && onWork ==true)
        {
            Rigidbody p = Instantiate(Paodan, shootPoint.transform.position, shootPoint.transform.rotation);
            p.velocity = -shootPoint.transform.forward * speed2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paodan"))
        {
            SentryHp.value -= 0.1f;
            Debug.Log(SentryHp.value);
        }
    }
}
