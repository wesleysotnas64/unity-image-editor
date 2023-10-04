using System;
using UnityEngine;

namespace Scripts.Effects
{
    static class LinearEffects
    {
        public static Texture2D Pixelization(Texture2D inputTexture, int pixelSize)
        {
            Color[] pixels = inputTexture.GetPixels();
            Color[] pixelatedPixels = new Color[pixels.Length];

            Color c = new Color();
            for (int i = 0; i < pixels.Length; i++)
            {
                c = pixels[i];

                c *= pixelSize;
                c.r = (float) Math.Floor(c.r);
                c.g = (float) Math.Floor(c.g);
                c.b = (float) Math.Floor(c.b);
                c /= pixelSize;

                c.a = 1;
                pixelatedPixels[i] = c;
            }

            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels(pixelatedPixels);
            outputTexture.Apply();

            return outputTexture;
        }

        public static Texture2D ColorFromDist(Texture2D inputTexture)
        {
            int width = inputTexture.width;
            int height = inputTexture.height;
            Texture2D outputTexture = new Texture2D(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    outputTexture.
                        SetPixel(
                            i, j,
                            new Color((float)i / width, (float)j / height, 0, 1)
                        );
                }
            }

            outputTexture.Apply();

            return outputTexture;
        }

        public static Texture2D GrayScale(Texture2D inputTexture, float weightRed = 1, float weightGreen = 1, float weightBlue = 1)
        {
            Color32[] pixels = inputTexture.GetPixels32();

            int width = inputTexture.width;
            int height = inputTexture.height;

            Color32[] grayPixels = new Color32[pixels.Length];

            for (int i = 0; i < pixels.Length; i++)
            {
                byte channelAverage = (byte)(((pixels[i].r * weightRed) + (pixels[i].g * weightGreen) + (pixels[i].b * weightBlue))/(weightRed+weightGreen+weightBlue));
                grayPixels[i] = new Color32(channelAverage, channelAverage, channelAverage, pixels[i].a);
            }

            Texture2D outputTexture = new Texture2D(width, height);
            outputTexture.SetPixels32(grayPixels);
            outputTexture.Apply();

            return outputTexture;
        }

        public static Texture2D Negative(Texture2D inputTexture)
        {
            Color[] pixels = inputTexture.GetPixels();

            int width = inputTexture.width;
            int height = inputTexture.height;

            Color[] negativePixels = new Color[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                float redNegative = (1 - pixels[i].r);
                float greenNegative = (1 - pixels[i].g);
                float blueNegative = (1 - pixels[i].b);

                negativePixels[i] = new Color(redNegative, greenNegative, blueNegative, pixels[i].a);
            }

            Texture2D outputTexture = new Texture2D(width, height);
            outputTexture.SetPixels(negativePixels);
            outputTexture.Apply();

            return outputTexture;

        }

        public static Texture2D Threshold(Texture2D inputTexture, float r, float g, float b)
        {
            Color[] pixels = inputTexture.GetPixels();

            Color[] thresholdPixels = new Color[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                float redThreshold;
                float greenThreshold;
                float blueThreshold;
                if (pixels[i].r < r)
                {
                    redThreshold = 0;
                }
                else
                {
                    redThreshold = 1;
                }
                if (pixels[i].g < g)
                {
                    greenThreshold = 0;
                }
                else
                {
                    greenThreshold = 1;
                }
                if (pixels[i].b < b)
                {
                    blueThreshold = 0;
                }
                else
                {
                    blueThreshold = 1;
                }

                thresholdPixels[i] = new Color(redThreshold, greenThreshold, blueThreshold, pixels[i].a);
            }

            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels(thresholdPixels);
            outputTexture.Apply();

            return outputTexture;
        }

        public static Texture2D GammaCorrection(Texture2D inputTexture, float gamma)
        {
            int width = inputTexture.width;
            int height = inputTexture.height;
            Texture2D outputTexture = new Texture2D(width, height);
            Color pixel;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixel = inputTexture.GetPixel(i, j);
                    pixel.r = (float)Math.Pow(pixel.r, gamma);
                    pixel.g = (float)Math.Pow(pixel.g, gamma);
                    pixel.b = (float)Math.Pow(pixel.b, gamma);
                    outputTexture.SetPixel(i, j, pixel);
                }
            }

            outputTexture.Apply();

            return outputTexture;
        }
        public static Texture2D ColorFragmentation(Texture2D inputTexture, int r, int g, int b, int limit, Color32 baseColor)
        {
            Color32[] pixels = inputTexture.GetPixels32();

            Color32[] fragmentationPixels = new Color32[pixels.Length];
            Debug.Log(baseColor);
            for (int i = 0; i < pixels.Length; i++)
            {
                int distance = (int)Math.Sqrt(Math.Pow(pixels[i].r-r,2)+Math.Pow(pixels[i].g-g,2)+Math.Pow(pixels[i].b-b,2));
                if(distance <=limit){
                    fragmentationPixels[i] = pixels[i];
                }else{
                    fragmentationPixels[i] = baseColor; 
                }
            }

            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels32(fragmentationPixels);
            outputTexture.Apply();

            return outputTexture;
        }
        public static Texture2D ChromaKey(Texture2D inputTexture, int r, int g, int b, int limit, Texture2D baseImg)
        {

            Color32[,] fragmentationPixels = new Color32[inputTexture.height, inputTexture.width];
            int xText;
            int yText = 0;
            for (int i = 0; i < inputTexture.height; i++)
            {
                xText = 0;
                for(int j = 0; j< inputTexture.width;j++){
                    int distance = (int)Math.Sqrt(Math.Pow((inputTexture.GetPixel(xText,yText).r*255)-r,2)+Math.Pow((inputTexture.GetPixel(xText,yText).g*255)-g,2)+Math.Pow((inputTexture.GetPixel(xText,yText).b*255)-b,2));
                    if(distance <= limit){
                        fragmentationPixels[i,j] = inputTexture.GetPixel(xText,yText);
                    }else{
                        fragmentationPixels[i,j] = baseImg.GetPixel(xText, yText); 
                    }
                    xText++;
                }
                yText++;
            }

            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            for (int i = 0; i < inputTexture.height; i++)
            {
                xText = 0;
                for(int j = 0; j< inputTexture.width;j++){
                    outputTexture.SetPixel(xText,yText, fragmentationPixels[i,j]);
                    xText++;
                }
                yText++;
            }
            outputTexture.Apply();
            return outputTexture;
        }

        public static Texture2D ApplyHsv(Texture2D inputTexture, int h, float s, float v){
            Color[] pixels = inputTexture.GetPixels();
            Color[] saida = new Color[pixels.Length];
            double somadorRed = 0;
            double somadorGreen = 1.125;
            double somadorBlue = 0;
            double[] valor = new double[3];
            valor[0] = 255;
            valor[1] = 0;
            valor[2] = 0; 
            for(int i = 0; i <= h;i++){
                if(i == 61){
                    somadorGreen = 1.125;
                    somadorRed = -2.25;
                    somadorBlue = 0;
                }else if(i == 121){
                    somadorBlue = 1.125;
                    somadorGreen = 0;
                    somadorRed = 0;
                }
                else if(i == 181){
                    somadorBlue = 1.125;
                    somadorGreen = -2.25;
                    somadorRed = 0;
                }
                else if(i == 241){
                    somadorBlue = 0;
                    somadorGreen = 0;
                    somadorRed= 1.125;
                }else if(i == 301){
                    somadorBlue = -2.25;
                    somadorGreen = 0;
                    somadorRed = 1.125;
                }
                valor[0] += somadorRed;
                valor[1] += somadorGreen; 
                valor[2] += somadorBlue;
            }
            valor[0] = 255 - valor[0];
            valor[1] = 255 - valor[1];
            valor[2] = 255 - valor[2];
            for(int i =0; i < pixels.Length; i++){
                if(pixels[i].r*255 > valor[0]){
                    pixels[i].r = 1;
                }else{
                    pixels[i].r = 0;
                }
                if(pixels[i].g*255 > valor[1]){
                    pixels[i].g = 1;
                }else{
                    pixels[i].g = 0;
                }
                if(pixels[i].b*255 > valor[2]){
                    pixels[i].b = 1;
                }else{
                    pixels[i].b = 0;
                }
                float somaRed = (255 - (pixels[i].r*255))*s;
                float somaGreen = (255 - (pixels[i].g*255))*s;
                float somaBlue = (255 - (pixels[i].b*255))*s;
                float redColor = (somaRed + pixels[i].r*255)*v;
                float greenColor = (somaGreen + pixels[i].g*255)*v;
                float blueColor = (somaBlue + pixels[i].b*255)*v;
                saida[i] = new Color(redColor/255, greenColor/255, blueColor/255, pixels[i].a);
            }
            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels(saida);
            outputTexture.Apply();
            return outputTexture;
        }

    }

}

