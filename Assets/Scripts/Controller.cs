using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public WheelCollider leftF;
    public WheelCollider leftB;
    public WheelCollider rightF;
    public WheelCollider rightB;
    
    public Transform LeftFTransform;
    public Transform LeftBTransform;
    public Transform RightFTransform;
    public Transform RightBTransform;
    public Transform Pivot;
    public Transform Pitch;
    public Transform GO1;
    public Transform GO2;

    public Slider Standard;
    public TextMeshProUGUI Lose;
    public Button Restart;
    
    private float motorTorque = 3500f;
    private float brakeTorque = 500f;

    private float angle1 = 90f;
    private float angle2 = 45f;

    private float elevation = 0;
    private int viewNum = 1;
    public GameObject Cam;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        MoveForwardOrBack();;
        MoveLeftOrRight();
        cwRotation();
        csRotation();
        ChangeView();
        
        LeftFTransform.Rotate(leftF.rpm * 6 * Time.deltaTime * Vector3.forward);
        LeftBTransform.Rotate(leftB.rpm * 6 * Time.deltaTime * Vector3.forward);
        RightFTransform.Rotate(rightF.rpm * 6 * Time.deltaTime * Vector3.forward);
        RightBTransform.Rotate(rightB.rpm * 6 * Time.deltaTime * Vector3.forward);
        
        //刹车
        bool isBraking = Input.GetKey(KeyCode.Space);
        
        leftF.brakeTorque = isBraking ? brakeTorque : 0f;
        leftB.brakeTorque = isBraking ? brakeTorque : 0f;
        rightF.brakeTorque = isBraking ? brakeTorque : 0f;
        rightB.brakeTorque = isBraking ? brakeTorque : 0f;

        if (Standard.value == 0)
        {
            Lose.gameObject.SetActive(true);
            Restart.gameObject.SetActive(true);
        }
           
        
    }

    void MoveForwardOrBack()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey((KeyCode.D)))
        {
            leftF.steerAngle = 0 ;
            leftB.steerAngle = 2 * angle1;
            rightF.steerAngle = 0;
            rightB.steerAngle = 2 * angle1;
            float verticalInput = -Input.GetAxisRaw("Vertical");
            leftF.motorTorque = verticalInput * motorTorque;
            leftB.motorTorque = -verticalInput * motorTorque;
            rightB.motorTorque = -verticalInput * motorTorque;
            rightF.motorTorque = verticalInput * motorTorque;
            
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            leftF.motorTorque = 0;
            leftB.motorTorque = 0;
            rightB.motorTorque = 0;
            rightF.motorTorque = 0;
        }
    }
    
    

    void MoveLeftOrRight()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            
            leftF.steerAngle = angle1 ;
            leftB.steerAngle = angle1;
            rightF.steerAngle = -angle1;
            rightB.steerAngle = 3 * angle1;
            float horizontalInput = -Input.GetAxisRaw("Horizontal");
            leftF.motorTorque =  horizontalInput * motorTorque;
            leftB.motorTorque = horizontalInput * motorTorque;
            rightB.motorTorque = -horizontalInput * motorTorque;
            rightF.motorTorque = -horizontalInput * motorTorque;

        }
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            leftF.motorTorque = 0;
            leftB.motorTorque = 0;
            rightB.motorTorque = 0;
            rightF.motorTorque = 0;
        }
        
    }
    
    
    
    

    void cwRotation()
    {
        if ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))|| (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)))
        {
            motorTorque = 5000f;
            float horizontalInput = -Input.GetAxisRaw("Horizontal");
            leftF.steerAngle = angle2 ;
            leftB.steerAngle = 3*angle2;
            rightF.steerAngle = -angle2;
            rightB.steerAngle = -3*angle2;
            leftF.motorTorque =  horizontalInput * motorTorque;
            leftB.motorTorque = -horizontalInput * motorTorque;
            rightB.motorTorque = horizontalInput * motorTorque;
            rightF.motorTorque = -horizontalInput * motorTorque;
        }
        else
        {
            motorTorque = 3500f;
        }
        
    }
    
    void csRotation()
    {
        if ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))|| (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)))
        {
            motorTorque = 5000f;
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            leftF.steerAngle = angle2 ;
            leftB.steerAngle = 3*angle2;
            rightF.steerAngle = -angle2;
            rightB.steerAngle = -3*angle2;
            leftF.motorTorque =  horizontalInput * motorTorque;
            leftB.motorTorque = -horizontalInput * motorTorque;
            rightB.motorTorque = horizontalInput * motorTorque;
            rightF.motorTorque = -horizontalInput * motorTorque;
        }
        else
        {
            motorTorque = 3500f;
        }
        
    }

    void Rotate()
    {
        float v = Input.GetAxis("Mouse ScrollWheel") * 20.0f;
        elevation += v;
        if (elevation > 40f || elevation < -20f)
        {
            if (elevation > 40)
            {
                elevation = 40;
            }
            else if(elevation<-20)
            {
                elevation = -20;
            }
            Pitch.RotateAround(Pivot.position, GO2.position - GO1.position, 0  );
        }
        else
        {
            Pitch.RotateAround(Pivot.position, GO2.position - GO1.position, v );
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paodan2"))
        {
            Standard.value -= 0.05f;
        }
    }

    void ChangeView()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            viewNum++;
        }

        if (viewNum >= 3)
        {
            viewNum = 1;
        }

        if (viewNum == 1)
        {
            Cam.transform.localPosition = new Vector3(0, 0.474f, 0.3856f);
        }

        if (viewNum == 2)
        {
            Cam.transform.localPosition = new Vector3(0f, 0.287f, -0.167f);

        }
    }
}