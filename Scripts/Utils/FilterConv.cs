using System;
using UnityEngine;


namespace Scripts.Utils
{
    public class FilterConv
    {
        private float[,] mask;
        private int sizeMask;
        private Color minValue;
        private Color maxValue;
        private Color[,] imgProc;

        public FilterConv()
        {
            minValue = new Color(0, 0, 0, 1);
            maxValue = new Color(0, 0, 0, 1);
        }

        public void DefFilter(float[,] mask, int sizeMask = 3)
        {
            this.mask = new float[sizeMask, sizeMask];
            if (mask == null)
            {
                for (int i = 0; i < sizeMask; i++)
                {
                    for (int j = 0; j < sizeMask; j++)
                        this.mask[i, j] = 1;
                }
            }
            this.mask = mask;
        }

        public Texture2D Conv(Texture2D texture)
        {
            int desloc = (sizeMask - 1) / 2;
            imgProc = new Color[texture.width, texture.height];

            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                    imgProc[i, j] = CalcKernel(texture, i, j, desloc);
                }
            }

            NormalizeImgProc(texture.width, texture.height);

            Texture2D t = new Texture2D(texture.width, texture.height);
            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                    t.SetPixel(i, j, imgProc[i, j]);
                }
            }
            t.Apply();

            return t;
        }

        private Color CalcKernel(Texture2D texture, int x, int y, int level)
        {
            Color px;
            float sumR = 0;
            float sumG = 0;
            float sumB = 0;
            for (int i = (x - level); i <= (x + level); i++)
            {
                for (int j = (y - level); j <= (y + level); j++)
                {
                    if (InsideTexture(texture.width, texture.height, i, j))
                    {
                        px = texture.GetPixel(i, j);
                        sumR += (px.r) * mask[i, j];
                        sumG += (px.g) * mask[i, j];
                        sumB += (px.b) * mask[i, j];
                    }
                }
            }

            minValue.r = sumR < minValue.r ? sumR : minValue.r;
            minValue.g = sumG < minValue.g ? sumG : minValue.g;
            minValue.b = sumB < minValue.b ? sumB : minValue.b;

            maxValue.r = sumR > maxValue.r ? sumR : maxValue.r;
            maxValue.g = sumG > maxValue.g ? sumG : maxValue.g;
            maxValue.b = sumB > maxValue.b ? sumB : maxValue.b;

            return new Color(sumR, sumG, sumB, 1);
        }
        private static bool InsideTexture(int width, int height, int i, int j)
        {
            if (i < 0 || j < 0) return false;
            if (i >= width || j >= height) return false;
            return true;
        }

        private void NormalizeImgProc(int width, int height)
        {
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    imgProc[i, j].r = (imgProc[i, j].r + minValue.r) / maxValue.r;
                    imgProc[i, j].g = (imgProc[i, j].g + minValue.g) / maxValue.g;
                    imgProc[i, j].g = (imgProc[i, j].b + minValue.b) / maxValue.b;
                }
            }
        }
    }

}
