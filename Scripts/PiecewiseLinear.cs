using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PiecewiseLinear : MonoBehaviour
{

    public GameObject BaseGoPoint;
    public List<GameObject> gOPoints;
    public Vector3 firstPosition;

    private void Start()
    {
        firstPosition = gOPoints[0].GetComponent<RectTransform>().localPosition;
    }

    public void AddPoint()
    {
        int count = gOPoints.Count;
        GameObject point = Instantiate(BaseGoPoint);
        point.transform.SetParent(transform);
        point.GetComponent<RectTransform>().localPosition = firstPosition + new Vector3(0, -40 * count, 0);
        point.GetComponent<RectTransform>().localScale = Vector3.one;
        gOPoints.Add(point);
    }

    public void RmvPoint(GameObject go)
    {
        gOPoints.Remove(go);

        Organize();
    }


    public void Organize()
    {
        int i = 0;
        foreach(GameObject go in gOPoints)
        {
            go.GetComponent<RectTransform>().localPosition = firstPosition + new Vector3(0, -40 * i, 0);
            i++;
        }
    }

    public float VerifyInterval(float x)
    {
        List<GameObject> list = new List<GameObject>();
        list = gOPoints;
        list.Sort((a, b) => a.GetComponent<Point>().values.x.CompareTo(b.GetComponent<Point>().values.x));

        float a;
        float b;
        float y = x;

        Vector2 p1;
        Vector2 p2;
        for(int i = 0; i < list.Count-1; i++)
        {
            p1 = list[i].GetComponent<Point>().values;
            p2 = list[i + 1].GetComponent<Point>().values;
            if ( x >= p1.x && x <= p2.x)
            {
                a = (p2.y - p1.y) / (p2.x - p1.x);
                //b = p2.y - a * p1.x;
                b = ((p1.y*p2.x)-(p2.y*p1.x)) /(p2.x - p1.x);

                return (a * x) + b;
            }
        }

        return y;
    }
}
