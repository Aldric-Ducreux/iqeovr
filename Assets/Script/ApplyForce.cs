using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public Rigidbody cube;

    public void jump()
    {
        cube.AddForce(0, 100, 0);
    }

    void Update()
    {
        
        //if (Input.GetButtonDown("Fire") && GetComponent<Renderer>().material.color == Color.red)
        //{
        //    cube.AddForce(0, 100, 0);
        //}

        //if (Input.GetButtonDown("FireAndroid") && GetComponent<Renderer>().material.color == Color.red)
        //{
        //    cube.AddForce(0, 100, 0);
        //}

    }

}
