using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecewiseLinear : MonoBehaviour
{
    public GameObject point;
    public List<GameObject> points;
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.positionCount = points.Count;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && VerifyHover() == false)
        {

        }
    }

    private void CreateAndInsertPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        if( mousePosition.x >= -130 &&
            mousePosition.y >=  -90 &&
            mousePosition.x <=  130 &&
            mousePosition.y <=   90)
        {
            int index = GetInterval(mousePosition.x);

            GameObject p = Instantiate(point);
            p.transform.parent = transform;
            p.transform.position = mousePosition;
            points.Insert(index, p);

        }

    }

    private bool VerifyHover()
    {
        //foreach(GameObject p in points)
        //{
        //    if(p.GetComponent<Point>().IsHover)
        //    {
        //        return true;
        //    }
        //}

        return false;
    }

    private int GetInterval(float xPosition)
    {
        for (int i = 0; i < points.Count; i++)
        {
            if((xPosition > points[i].transform.position.x) && (xPosition < points[i+1].transform.position.x))
            {
                return i + 1;
            }
        }

        return 0;
    }

}
