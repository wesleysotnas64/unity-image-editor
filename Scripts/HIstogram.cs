using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Histogram : MonoBehaviour
{
    public static int[] redChannel;
    public static int[] greenChannel;
    public static int[] blueChannel;
    
    public static int maxRed = 0;
    public static int maxGreen = 0;
    public static int maxBlue = 0;


    public static void CalcHistogram(Texture2D texture)
    {
        Color32[] pixels = texture.GetPixels32();
        redChannel = new int[256];
        blueChannel = new int[256];
        greenChannel = new int[256];
        for(int i = 0; i < pixels.Length; i++)
        {
            redChannel[pixels[i].r] += 1;
            greenChannel[pixels[i].g] += 1;
            blueChannel[pixels[i].b] += 1;
        }
        for(int i = 0; i < 256; i++)
        {
            if(redChannel[i] > maxRed){
                maxRed = redChannel[i];
            }else if(greenChannel[i] > maxGreen){
                maxGreen = greenChannel[i];
            }else if(blueChannel[i] > maxBlue){
                maxBlue = blueChannel[i];
            }
        }
    }
    public static Texture2D ReturnHistogramRed(){
        Texture2D saida = new Texture2D(3315, 2000);
        int espaco = (int)saida.width/255;
        Debug.Log(espaco);
        int cont = 0;
        int pos = 0;
        Color black = new Color(0,0,0,1);
        for(int i = 0; i<= saida.height; i++){
            for(int j = 0; j <= saida.width; j++){
                saida.SetPixel(j,i,black);
            }
        }
        for(int j = 0; j <= saida.width; j++){
            cont += 1;
            int valor = redChannel[pos]%2000;
            for(int i =0; i <= valor; i++){
                saida.SetPixel(j,i, new Color(1,0,0,1));
            }
            if(cont >= espaco){
                cont = 0;
                pos+=1;
            }
        }
        saida.Apply();
        return saida;
    }
        public static Texture2D ReturnHistogramGreen(){
        Texture2D saida = new Texture2D(3315, 2000);
        
        int espaco = (int)saida.width/255;
        int cont = 0;
        int pos = 0;
        Color black = new Color(0,0,0,1);
        for(int i = 0; i<= saida.height; i++){
            for(int j = 0; j <= saida.width; j++){
                saida.SetPixel(j,i,black);
            }
        }
        for(int j = 0; j <= saida.width; j++){
            cont += 1;
            int valor = greenChannel[pos]%2000;
            for(int i =0; i <= valor; i++){
                saida.SetPixel(j,i, new Color(0,1,0,1));
            }
            if(cont >= espaco){
                cont = 0;
                pos+=1;
            }
        }
        saida.Apply();
        return saida;
    }
        public static Texture2D ReturnHistogramBlue(){
        Texture2D saida = new Texture2D(3315, 2000);
        int espaco = (int)saida.width/255;
        Debug.Log(espaco);
        int cont = 0;
        int pos = 0;
        Color black = new Color(0,0,0,1);
        for(int i = 0; i<= saida.height; i++){
            for(int j = 0; j <= saida.width; j++){
                saida.SetPixel(j,i,black);
            }
        }
        for(int j = 0; j <= saida.width; j++){
            cont += 1;
            int valor = blueChannel[pos]%2000;
            for(int i =0; i <= valor; i++){
                saida.SetPixel(j,i, new Color(0,0,1,1));
            }
            if(cont >= espaco){
                cont = 0;
                pos+=1;
            }
        }
        saida.Apply();
        return saida;
    }

    public static Texture2D EqualizeHistogram(Texture2D texture)
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
            Debug.Log(valor);

            valor = (greenChannel[i] / length) * 255;
            greenEqualized[i] = (int)valor;

            valor = (blueChannel[i] / length) * 255;
            blueEqualized[i] = (int)valor;
        }

        Color32[] imgEqualized = new Color32[length];
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
