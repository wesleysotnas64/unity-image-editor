using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public Color c;
    void Start()
    {
        c = new Color(-5, 1, 1, 1);
        Debug.Log(c.ToString());

        Texture2D t = new Texture2D(4, 4);
        t.SetPixel(0, 0, c);
        Debug.Log(t.GetPixel(0,0).ToString());
    }

}
