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

        public static Texture2D Threshold(Texture2D inputTexture, float r, float g, float b, int type, float value)
        {
            Color[] pixels = inputTexture.GetPixels();

            Color[] thresholdPixels = new Color[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                float redThreshold = 0;
                float greenThreshold = 0;
                float blueThreshold = 0;
                if(type == 1){
                    if (pixels[i].r < value)
                    {
                        redThreshold = 0;
                    }
                    else
                    {
                        redThreshold = 1;
                    }
                    if (pixels[i].g < value)
                    {
                        greenThreshold = 0;
                    }
                    else
                    {
                        greenThreshold = 1;
                    }
                    if (pixels[i].b < value)
                    {
                        blueThreshold = 0;
                    }
                    else
                    {
                        blueThreshold = 1;
                    }
                }else if(type == 2){
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
                }

                thresholdPixels[i] = new Color(redThreshold, greenThreshold, blueThreshold, pixels[i].a);
            }

            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels(thresholdPixels);
            outputTexture.Apply();

            return outputTexture;
        }

        public static Texture2D GammaCorrection(Texture2D inputTexture, float gammaRed, float gammaBlue, float gammaGreen)
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
                    pixel.r = (float)Math.Pow(pixel.r, gammaRed);
                    pixel.g = (float)Math.Pow(pixel.g, gammaGreen);
                    pixel.b = (float)Math.Pow(pixel.b, gammaBlue);
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
        public static Texture2D LogartimicaTransform(Texture2D inputTexture, double rC, double bC,double gC )
        {
            Color32[] pixels = inputTexture.GetPixels32();

            Color32[] transformacaoLog = new Color32[pixels.Length];
            rC = rC / Math.Log(256);
            bC = bC / Math.Log(256);
            gC = gC / Math.Log(256);
            
            for (int i = 0; i < pixels.Length; i++)
            {
                int redTransform = (int)(rC * Math.Log(1 + pixels[i].r));
                int greenTransform = (int)(bC * Math.Log(1 + pixels[i].g));
                int blueTransform = (int)(gC * Math.Log(1 + pixels[i].b));
                transformacaoLog[i] = new Color32((byte)(redTransform),(byte)(greenTransform),(byte)(blueTransform), pixels[i].a);

            }
            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels32(transformacaoLog);
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
                        fragmentationPixels[i,j] = baseImg.GetPixel(xText, yText);
                    }else{
                        fragmentationPixels[i,j] = inputTexture.GetPixel(xText,yText);
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

        public static Texture2D ApplyHsv(Texture2D inputTexture, int h, double s, double v){
            Color[] pixels = inputTexture.GetPixels();
            Color[] saida = new Color[pixels.Length];

            for(int i =0; i < pixels.Length; i++){
                double[] coresHsv = LinearEffects.RgbToHsv(pixels[i].r,pixels[i].g, pixels[i].b);
                coresHsv[0] += h;
                if(coresHsv[0] > 360){
                    coresHsv[0] = 360;
                }else if(coresHsv[0] < 0){
                    coresHsv[0] = 0;
                }
                coresHsv[1] += s;
                if(coresHsv[1] > 1){
                    coresHsv[1] = 1;
                }else if(coresHsv[1] < 0){
                    coresHsv[1] = 0;
                }
                coresHsv[2] += v;
                if(coresHsv[2] > 1){
                    coresHsv[2] = 1;
                }else if(coresHsv[2] < 0){
                    coresHsv[2] = 0;
                }
                double[] coresRgb = LinearEffects.HsvToRgb(coresHsv[0],coresHsv[1],coresHsv[2]);
                saida[i] = new Color((float)Math.Round(coresRgb[0],2),(float)Math.Round(coresRgb[1],2),(float)Math.Round(coresRgb[2],2), 1);
            }
            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            outputTexture.SetPixels(saida);
            outputTexture.Apply();
            return outputTexture;
        }
        public static double[] HsvToRgb(double h, double s, double v){
        double H = h / 360.0;
        double S = s;
        double V = v;

        double[] rgb = new double[3];

        if (S == 0)
        {
            rgb[0] = (int)Math.Round(V * 255);
            rgb[1] = (int)Math.Round(V * 255);
            rgb[2] = (int)Math.Round(V * 255);
        }
        else
        {
            H *= 6; 
            int i = (int)Math.Floor(H);
            double f = H - i;

            int p = (int)Math.Round(255 * (V * (1 - S)));
            int q = (int)Math.Round(255 * (V * (1 - S * f)));
            int t = (int)Math.Round(255 * (V * (1 - S * (1 - f))));

            V = (int)Math.Round(255 * V);

            switch (i)
            {
                case 0:
                    rgb[0] = (int)V;
                    rgb[1] = t;
                    rgb[2] = p;
                    break;
                case 1:
                    rgb[0] = q;
                    rgb[1] = (int)V;
                    rgb[2] = p;
                    break;
                case 2:
                    rgb[0] = p;
                    rgb[1] = (int)V;
                    rgb[2] = t;
                    break;
                case 3:
                    rgb[0] = p;
                    rgb[1] = q;
                    rgb[2] = (int)V;
                    break;
                case 4:
                    rgb[0] = t;
                    rgb[1] = p;
                    rgb[2] = (int)V;
                    break;
                default:
                    rgb[0] = (int)V;
                    rgb[1] = p;
                    rgb[2] = q;
                    break;
            }
        }
        rgb[0] = Math.Round(rgb[0]/255,2);
        rgb[1] = Math.Round(rgb[1]/255,2);
        rgb[2] = Math.Round(rgb[2]/255,2);

        return rgb;
    }
        public static double[] RgbToHsv(double r, double g, double b){
            double R = Math.Round(r,3);
            double G = Math.Round(g,3);
            double B = Math.Round(b,3);

            double[] hsv = new double[3];

            double max = Math.Max(R, Math.Max(G, B));
            double min = Math.Min(R, Math.Min(G, B));

            // Calculando a Matiz (H)
            if (max == min){
                hsv[0] = 0;
            }
            else if (max == R){
                hsv[0] = (int)((60 * ((G - B) / (max - min)) + 360) % 360);
            }
            else if (max == G){
                hsv[0] = (int)((60 * ((B - R) / (max - min)) + 120) % 360);
            }
            else if (max == B){
                hsv[0] = (int)((60 * ((R - G) / (max - min)) + 240) % 360);
            }

            // Calculando a Saturação (S)
            if (max == 0)
            {
                hsv[1] = 0;
            }
            else
            {
                hsv[1] = Math.Round((1 - (min / max)), 2);
            }

            // Calculando o Valor (V)
            hsv[2] = max;
            return hsv;
        }

    }

}

