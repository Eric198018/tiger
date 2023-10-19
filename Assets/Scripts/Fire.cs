using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Rigidbody Paodan;
    public GameObject shootPoint;
    public TextMeshProUGUI Heat;

    private float maxHeat = 15;
    private float nowHeat = 0;
    private float cooling = 2.5f;

    private bool OutHeat = false;
    private bool isOnFire = false;

    private float speed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire2", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        ComputeHeat();
        Cooling();
        Fire1();
        onFire();
        updateHeat();
        

        nowHeat -= Time.deltaTime * cooling ;
        
    }


    void ComputeHeat()
    {
        if (nowHeat > maxHeat)
        {
            OutHeat = true;
            
        }
        else
        {
            OutHeat = false;
        }
    }

    void Cooling()
    {
        if (nowHeat <= 0)
        {
            cooling = 0;
        }

        else
        {
            cooling = 2.5f;
        }
    }

    void onFire()
    {
        if (Input.GetMouseButton(1))
        {
            isOnFire = true;
        }

        else
        {
            isOnFire = false;
        }
    }

    void Fire1()
    {
        if (Input.GetButtonDown("Fire1") && !OutHeat)
        {
            Rigidbody p = Instantiate(Paodan, shootPoint.transform.position, shootPoint.transform.rotation);
            p.velocity = -shootPoint.transform.forward * speed;
            nowHeat += 1;
        }
    }

    void Fire2()
    {
        if (isOnFire == true  && !OutHeat)
        {
            Rigidbody p = Instantiate(Paodan, shootPoint.transform.position, shootPoint.transform.rotation);
            p.velocity = -shootPoint.transform.forward * speed;
            nowHeat += 1;
        }
    }

    void updateHeat()
    {
        if (!OutHeat)
        {
            Heat.text = "Heat = " + (int)nowHeat;
        }

        else
        {
            Heat.text = "OutHeat";
        }
        
    }
}
