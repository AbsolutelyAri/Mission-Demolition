/*
 * Created by: Ben Krieger
 * Created on: 2/16/2022
 * 
 * Last edited by: 
 * Last edited on: 
 * 
 * Description: makes a rigidbody component sleep, used on castle components
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //the object this script is attached to must have a Rigidbody

public class RigidSleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.Sleep();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
