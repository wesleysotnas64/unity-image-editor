using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIstogram : MonoBehaviour
{
    public int[] redChannel   = new int[256];
    public int[] greenChannel = new int[256];
    public int[] blueChannel  = new int[256];

    public void CalcHistogram(Texture2D texture)
    {
        Color32[] pixels =texture.GetPixels32();

        for(int i = 0; i < pixels.Length; i++)
        {
            redChannel[pixels[i].r] += 1;
            redChannel[pixels[i].g] += 1;
            redChannel[pixels[i].b] += 1;
        }

    }

    public void NormalizeHistogram()
    {

    }
}
