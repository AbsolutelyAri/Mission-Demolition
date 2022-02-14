/*
 * Created by: Ben Krieger
 * Created on: 2/14/2022
 * 
 * Last edited by: 
 * Last edited on:
 * 
 * Description: randomly generate a cloud
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    //Variables
    [Header("set in inspector")]
    public GameObject cloudSphere;
    public int minSpheres = 6;
    public int maxSpheres = 12;
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public float scaleYMin = 2f;

    private List<GameObject> spheres;


    // Start is called before the first frame update
    void Start()
    {
        GenerateCloud();
    }

    // Update is called once per frame
    void Update()
    {
        /*for debug purposes, generate clouds when space pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
        */
    }

    //regenerate the cloud
    void Restart()
    {
        foreach(GameObject sp in spheres)
        {
            Destroy(sp);
        }
        GenerateCloud();
    }

    void GenerateCloud()
    {
        spheres = new List<GameObject>();
        int num = Random.Range(minSpheres, maxSpheres);

        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere);
            spheres.Add(sp);

            Transform spTrans = sp.transform; //sp is trans pog
            spTrans.SetParent(this.transform);

            //Randomly assign a position for sp
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            //Randomly scale sphere. Why would I ever want to use vector2's like this?
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            //Adjust y scale by x distance from parent object center
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);
            spTrans.localScale = scale;
        }
    }
}
