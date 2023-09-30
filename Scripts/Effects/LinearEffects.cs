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

        public static Texture2D Threshold(Texture2D inputTexture, float k)
        {
            Color[] pixels = inputTexture.GetPixels();

            Color[] thresholdPixels = new Color[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                float redThreshold;
                float greenThreshold;
                float blueThreshold;
                if (pixels[i].r < k)
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

    }

}

