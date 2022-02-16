/*
 * Created by: Ben Krieger
 * Created on: 2/9/2022
 * 
 * Last edited by: 
 * Last edited on:
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotScript : MonoBehaviour
{

    static private SlingshotScript S;

    [Header("Set dynamically")]
    public GameObject launchPoint; //point where projectile launches from
    public Vector3 LaunchPos;
    public GameObject projectile; //instance of the projectile prefab
    public bool aimingMode; //is the player actively aiming?
    public Rigidbody projRB; //rigidbody of the projectile

    static public Vector3 LAUNCH_POS
    {
        get
        {
            if(S == null)
            {
                return Vector3.zero;
            }
            return S.LaunchPos;
        }
    }

    [Header("Set in inspector")]
    public GameObject prefabProjectile; //projectile that will be fired
    public float velocityMultiplier = 8f;


    private void Awake()
    {
        S = this; //set singleton to this object

        //Find the LaunchPoint
        Transform LaunchPointTrans = transform.Find("LaunchPoint"); //holy crap the launch point is trans, this game has more LGBT+ characters than Disney
        launchPoint = LaunchPointTrans.gameObject;
        //Deactivate it so the halo turns off
        launchPoint.SetActive(false);

        LaunchPos = LaunchPointTrans.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return; //if not aiming exit the update

        //get mouse position from 2D coords
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - LaunchPos; //find change in mouse position from launch point
        float maxMagnitude = this.GetComponent<SphereCollider>().radius; //set the maximum distance to the size of the sphere collider

        if(mouseDelta.magnitude > maxMagnitude) //if the mouse is further away than the sphere collider
        {
            mouseDelta.Normalize(); //set to a direction vector of magnitude 1
            mouseDelta *= maxMagnitude; //multiply direction vector by the maximum magnitude
        }

        Vector3 projectilePos = LaunchPos + mouseDelta;
        projectile.transform.position = projectilePos;


        if (Input.GetMouseButtonUp(0)) //check if mouse has been released, doing it in Update for the sake of timing
        {
            aimingMode = false; 
            projRB.isKinematic = false; //turn physics back on
            projRB.velocity = -mouseDelta * velocityMultiplier;
            FollowCam.PoI = projectile; //set the camera point of interest to the projectile
            projectile = null; //empty the reference 
        }
    }

    // detect when mouse enters slingshot area, toggle launchPoint
    private void OnMouseEnter()
    {
        //print("OnMouseEnter triggered");
        launchPoint.SetActive(true);
    }

    // detect when mouse leaves the slingshot area
    private void OnMouseExit()
    {
        //print("OnMouseExit triggered");
        launchPoint.SetActive(false);
    }

    // detect when left click is held, enter aiming mode, spawn a projectile, store its rigidbody
    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject; // create the projectile instance
        projectile.transform.position = LaunchPos; //place at the launch point
        projRB = projectile.GetComponent<Rigidbody>(); //store the rigidbody
        projRB.isKinematic = true; //make the projectile stay still
    }
}
