using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Effects
{
    static class Effects
    {
        public static Texture2D GrayScale(Texture2D inputTexture)
        {
            // ObtÈm a matriz de pixels da textura
            Color32[] pixels = inputTexture.GetPixels32();

            // Largura e altura da textura
            int width = inputTexture.width;
            int height = inputTexture.height;

            // Cria uma nova matriz de pixels para a textura de sa˙Åa em escala de cinza
            Color32[] grayPixels = new Color32[pixels.Length];

            // Converte os pixels para escala de cinza
            for (int i = 0; i < pixels.Length; i++)
            {
                byte grayValue = (byte)(0.2989f * pixels[i].r + 0.5870f * pixels[i].g + 0.1140f * pixels[i].b);
                grayPixels[i] = new Color32(grayValue, grayValue, grayValue, pixels[i].a);
            }

            // Cria uma nova textura com os pixels em escala de cinza
            Texture2D outputTexture = new Texture2D(width, height);
            outputTexture.SetPixels32(grayPixels);
            outputTexture.Apply();

            return outputTexture;
        }

        public static Texture2D Negative(Texture2D inputTexture, float level)
        {
            Color[] pixels = inputTexture.GetPixels();

            int width = inputTexture.width;
            int height = inputTexture.height;

            Color[] negativePixels = new Color[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                float redNegative   = (level - pixels[i].r);
                float greenNegative = (level - pixels[i].g);
                float blueNegative  = (level - pixels[i].b);

                negativePixels[i] = new Color(redNegative, greenNegative, blueNegative, pixels[i].a);
            }

            Texture2D outputTexture = new Texture2D(width, height);
            outputTexture.SetPixels(negativePixels);
            outputTexture.Apply();

            return outputTexture;

        }
        public static void ColorMap(Texture2D inputTexture) {
            Color32[] pixels = inputTexture.GetPixels32();

            //criei essa parte porque n sabia se em C# o vetor ja vinha prenchido com 0 e queria evitar falha
            int[] mapeamento = new int[255];
            for (int i =0; i < mapeamento.Length; i++)
            {
                mapeamento[i] = 0;

            }
            for (int i = 0; i < pixels.Length; i++)
            {
                mapeamento[pixels[i].r] = mapeamento[pixels[i].r] + 1;
                mapeamento[pixels[i].g] = mapeamento[pixels[i].g] + 1;
                mapeamento[pixels[i].b] = mapeamento[pixels[i].b] + 1;
            }

            
        }
        public static Texture2D TransformacaoLogaritimica(Texture2D inputTexture, double c)
        {
            Color32[] pixels = inputTexture.GetPixels32();

            Color32[] transformacaoLog = new Color32[pixels.Length];
            c = c / Math.Log(256);
            for (int i = 0; i < pixels.Length; i++)
            {
                int redTransform = (int)(c * Math.Log(1 + pixels[i].r));
                int greenTransform = (int)(c * Math.Log(1 + pixels[i].g));
                int blueTransform = (int)(c * Math.Log(1 + pixels[i].b));
                transformacaoLog[i] = new Color32((byte)(redTransform),(byte)(greenTransform),(byte)(blueTransform), pixels[i].a);

            }
            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels32(transformacaoLog);
            outputTexture.Apply();
            return outputTexture;
        }
        public static Texture2D CorrecaoGama(Texture2D inputTexture, double gama, int controler) {
            Color32[] pixels = inputTexture.GetPixels32();

            Color32[] correcaoPixels = new Color32[pixels.Length];
            for( int i = 0; i < pixels.Length; i++)
            {
                double redCorrecao = Math.Pow(pixels[i].r / controler, gama);
                double greenCorrecao = Math.Pow(pixels[i].g / controler, gama);
                double blueCorrecao = Math.Pow(pixels[i].b / controler, gama);
                correcaoPixels[i] = new Color32((byte)(redCorrecao*controler),(byte)(greenCorrecao*controler),(byte)(blueCorrecao*controler), pixels[i].a);

            }
            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels32(correcaoPixels);
            outputTexture.Apply();
            return outputTexture;
        } 
        public static Texture2D Threshold(Texture2D inputTexture, float k)
        {
            Color[] pixels = inputTexture.GetPixels();

            Color[] thresholdPixels = new Color[pixels.Length];

            for( int i = 0; i < pixels.Length; i++)
            {
                float redThreshold;
                float greenThreshold;
                float blueThreshold;
                if (pixels[i].r < k )
                {
                    redThreshold = 0;
                    greenThreshold = 0;
                    blueThreshold = 0;
                }
                else
                {
                    redThreshold = 1;
                    greenThreshold = 1;
                    blueThreshold = 1;
                }

                thresholdPixels[i] = new Color(redThreshold, greenThreshold, blueThreshold, pixels[i].a);
            }

            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels(thresholdPixels);
            outputTexture.Apply();

            return outputTexture;
        }
    }
}