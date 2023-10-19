using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paodan : MonoBehaviour
{
    public int a = 0;
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "Plane")
        {
            a += 1;
            if (a >= 2)
            {
                Destroy(gameObject);
            }
        }

        
    }
}
