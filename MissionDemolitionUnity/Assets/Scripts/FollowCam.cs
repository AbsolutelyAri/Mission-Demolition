/*
 * Created by: Ben Krieger
 * Created on: 2/9/2022 cuz we're speedrunners B)
 * 
 * Last edited by: 
 * Last edited on:
 * 
 * Description: make the camera follow the projectile that was fired
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    static public GameObject PoI; //static point of interest 

    public float camZ; //desired Z position of the camera

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (PoI == null) return; //if we have no point of interest then don't bother

        Vector3 destination = PoI.transform.position;
        destination.z = camZ; //correct the Z value to the desired z
        transform.position = destination;
    }
}
