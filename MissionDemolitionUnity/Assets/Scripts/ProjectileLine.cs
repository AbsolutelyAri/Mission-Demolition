/*
 * Created by: Ben Krieger
 * Created on: 2/16/2022
 * 
 * Last edited by: 
 * Last edited on: 
 * 
 * Description: makes the line renderer create a line behind the last projectile
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; //Singleton variable

    [Header("Set in inspector")]
    public float minDistance = 0.1f;
    public Vector2 lineWidth = new Vector2(0.0f, 0.5f);

    private LineRenderer line;
    private GameObject linePoI;
    private List<Vector3> points;

    private void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        line.SetWidth(lineWidth.x, lineWidth.y);
        points = new List<Vector3>();
    }

    public GameObject PoI
    {
        get { return (linePoI); }

        set
        {
            linePoI = value;
            if(linePoI != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoints();
            }
        }
    }

    public void Clear() //clears out this object
    {
        linePoI = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoints() //method to add points to the list and the line renderer. Will skip over a point if its too close to the LaunchPos
    {
        Vector3 pt = linePoI.transform.position;
        if(points.Count > 0 && (pt-lastPoint).magnitude < minDistance)
        {
            return; //if the point isn't far enough from the last point then we don't add a point
        }

        if(points.Count == 0)
        {
            Vector3 launchPosDiff = pt - SlingshotScript.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector3 lastPoint //returns the last point in the list
    {
        get 
        { 
            if(points == null)
            {
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }

    private void FixedUpdate()
    {
        if(PoI == null)
        {
            if (FollowCam.PoI != null)
            {
                if (FollowCam.PoI.tag == "Projectile")
                {
                    PoI = FollowCam.PoI;
                }
                else
                {
                    return;
                }
            }
            else return;
        }

        AddPoints();
        if(FollowCam.PoI == null)
        {
            PoI = null;
        }
    }
}
