using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Histogram : MonoBehaviour
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

    public Texture2D EqualizeHistogram(Texture2D texture)
    {
        float valor;
        Color32[] pixels = texture.GetPixels32();
        int length = pixels.Length;
        int[] redEqualized = new int[256];
        int[] greenEqualized = new int[256];
        int[] blueEqualized = new int[256];

        for(int i = 0; i < 256; i++)
        {
            valor = (redChannel[i] / length) * 255;
            redEqualized[i] = (int)valor;

            valor = (greenChannel[i] / length) * 255;
            greenEqualized[i] = (int)valor;

            valor = (blueChannel[i] / length) * 255;
            blueEqualized[i] = (int)valor;
        }

        Color32[] imgEqualized = new Color32[256];
        for(int i = 0; i < length; i++)
        {
            imgEqualized[i].r = (byte) redEqualized[pixels[i].r];
            imgEqualized[i].g = (byte) greenEqualized[pixels[i].g];
            imgEqualized[i].b = (byte) blueEqualized[pixels[i].b];
        }

        Texture2D output = new Texture2D(texture.width, texture.height);
        output.SetPixels32(imgEqualized);
        output.Apply();

        CalcHistogram(output);

        return output;
    }
}
