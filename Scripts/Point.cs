using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    public GameObject piecewiseLinear;

    public GameObject gOPoint;
    public Slider xSlider;
    public Slider ySlider;
    public Text xText;
    public Text yText;

    public Vector2 values;

    void Start()
    {
        GetAllValues();
        piecewiseLinear = GameObject.Find("Linear por Partes");
    }

    public void GetAllValues()
    {
        values.x = xSlider.value;
        values.y = ySlider.value;

        xText.text = values.x.ToString();
        yText.text = values.y.ToString();

        GameObject.Find("Manager").GetComponent<Manager>().RenderManager();
    }

    public void DeleteThisPoint()
    {
        piecewiseLinear.GetComponent<PiecewiseLinear>().RmvPoint(this.gameObject);
        Destroy(gOPoint);
    }

}
