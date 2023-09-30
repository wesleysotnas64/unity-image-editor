using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public List<Transform> points;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //GeneratePoints();
        lineRenderer.positionCount = points.Count;
        //GenerateLine();
    }

    //private void GenerateLine()
    //{
    //    for (int i = 0; i < points.Count; i++)
    //    {
    //        lineRenderer.SetPosition(i, points[i].position);
    //    }
    //}

    private void Update()
    {
        //GenerateLine();
        //if(Input.GetMouseButton(0))
        //{
        //    Vector3 mousePosition = Input.mousePosition;
        //    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //    mousePosition.z = 0;
        //    transform.position = mousePosition;
        //    GenerateLine();
        //}
        //if (Input.GetKeyDown("space"))
        //{
        //    GeneratePoints();
        //}

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }

    //private void GeneratePoints()
    //{
    //    points = new List<Vector3>();
    //    int y;
    //    for (int x = 0; x < 8; x++)
    //    {
    //        y = Random.Range(0, 5);
    //        points.Add(new Vector3(x, y, 0));
    //    }
    //}
}
