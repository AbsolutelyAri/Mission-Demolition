/*
 * Created by: Ben Krieger
 * Created on: 2/14/2022
 * 
 * Last edited by: 
 * Last edited on:
 * 
 * Description: generate a sky full of clouds
 * 
 * 
 * Note: finish by Wed, 2/16/2022
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    //variables

    [Header("set in inspector")]
    public int numberOfClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPositionMinimum = new Vector3(-50, -5, 10);
    public Vector3 cloudPositionMaximum = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1f;
    public float cloudScaleMax = 3f;
    public float cloudSpeedMultiplier = 0.5f;

    private GameObject[] cloudInstances;

    private void Awake()
    {
        cloudInstances = new GameObject[numberOfClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");

        GameObject cloud;


        for(int i = 0; i < numberOfClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);

            //position the cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPositionMinimum.x, cloudPositionMaximum.x);
            cPos.y = Random.Range(cloudPositionMinimum.y, cloudPositionMaximum.y);

            //scale the cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(cloudPositionMinimum.y, cPos.y, scaleU); //makes smaller clouds with smaller scale closer to the ground

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
