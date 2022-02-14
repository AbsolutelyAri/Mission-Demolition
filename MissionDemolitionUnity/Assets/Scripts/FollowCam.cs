/*
 * Created by: Ben Krieger
 * Created on: 2/9/2022 cuz we're speedrunners B)
 * 
 * Last edited by: 2/14/2022
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

    [Header("Set in Inspector")]
    public float easing = 0.05f; //percentage we want to ease
    public Vector2 minXAndY = Vector2.zero;

    public float camZ; //desired Z position of the camera

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (PoI == null) return; //if we have no point of interest then don't bother

        Vector3 destination = PoI.transform.position;

        destination.x = Mathf.Max(minXAndY.x, destination.x);
        destination.y = Mathf.Max(minXAndY.y, destination.y); //if the x or y are lower than the minimum then set destination to the minimum

        destination = Vector3.Lerp(transform.position, destination, easing); //interpolate between current position and position of the projectile based on easing

        destination.z = camZ; //correct the Z value to the desired z
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }
}
