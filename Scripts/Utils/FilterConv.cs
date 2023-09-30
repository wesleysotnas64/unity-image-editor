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
        private Color[,] img;

        public FilterConv()
        {
            minValue = new Color(99999,99999,99999,1);
            maxValue = new Color(-99999, -99999, -99999, 1);
        }

        public void DefFilter(float[,] mask, int sizeMask)
        {
            this.sizeMask = sizeMask;
            this.mask = new float[sizeMask, sizeMask];
            this.mask = mask;
        }
        public Texture2D Finalizador(Texture2D texture){
            imgProc = new Color[texture.height, texture.width];
            int xText;
            int yText = 0;
            for (int i = 0; i < texture.height; i++)
            {
                xText = 0;
                for (int j = 0; j < texture.width; j++)
                {
                    imgProc[i, j] = texture.GetPixel(xText, yText);
                    xText++;
                }
                yText++;
            }
            for(int i = 0; i < texture.height;i++){
                for(int j = 0; j < texture.width; j++)
                {
                    minValue.r = imgProc[i,j].r < minValue.r ? imgProc[i,j].r : minValue.r;
                    minValue.g = imgProc[i,j].g < minValue.g ? imgProc[i,j].g : minValue.g;
                    minValue.b = imgProc[i,j].b < minValue.b ? imgProc[i,j].b : minValue.b;
                }
            }
            NormalizeImgProc(texture.width, texture.height);
            return SetarPixels(texture);
        }
        private Texture2D SetarPixels(Texture2D texture){
            Texture2D final = new Texture2D(texture.width, texture.height);
            int xText;
            int yText = 0;
            for (int i = 0; i < texture.height; i++)
            {
                xText = 0;
                for (int j = 0; j < texture.width; j++)
                {
                    final.SetPixel(xText, yText, imgProc[i, j]);
                    xText++;
                }
                yText++;
            }
            return final;
        }
        public Texture2D Conv(Texture2D texture)
        {
            int desloc = (sizeMask - 1) / 2;
            Debug.Log(desloc);
            imgProc = new Color[texture.height, texture.width];
            img = new Color[texture.height, texture.width];

            int xText;
            int yText = 0;
            for (int i = 0; i < texture.height; i++)
            {
                xText = 0;
                for (int j = 0; j < texture.width; j++)
                {
                    img[i, j] = texture.GetPixel(xText, yText);
                    xText++;
                }
                yText++;
            }

            for (int i = 0; i < texture.height; i++)
            {
                for (int j = 0; j < texture.width; j++)
                {
                    imgProc[i, j] = CalcKernel(texture.width, texture.height, i, j, desloc);
                }
            }

            NormalizeImgProc(texture.width, texture.height);
            Debug.Log(minValue);
            Debug.Log(maxValue);
            Texture2D t = new Texture2D(texture.width, texture.height);
            
            yText = 0;
            for (int i = 0; i < texture.height; i++)
            {
                xText = 0;
                for (int j = 0; j < texture.width; j++)
                {
                    t.SetPixel(xText, yText, imgProc[i, j]);
                    xText++;
                }
                yText++;
            }
            return t;
        }

        private Color CalcKernel(int width, int height, int i, int j, int desloc)
        {
            Color px;
            float sumR = 0;
            float sumG = 0;
            float sumB = 0;
            int iMask = 0;
            int jMask;
            for (int iImg = (i - desloc); iImg <= (i + desloc); iImg++)
            {
                jMask = 0;
                for (int jImg = (j - desloc); jImg <= (j + desloc); jImg++)
                {
                    if (InsideTexture(width, height, iImg, jImg))
                    {
                        px = img[iImg, jImg];
                        sumR += (px.r) * this.mask[iMask, jMask];
                        sumG += (px.g) * this.mask[iMask, jMask];
                        sumB += (px.b) * this.mask[iMask, jMask];
                    }

                    jMask++;
                }
                iMask++;
            }

            minValue.r = sumR < minValue.r ? sumR : minValue.r;
            minValue.g = sumG < minValue.g ? sumG : minValue.g;
            minValue.b = sumB < minValue.b ? sumB : minValue.b;
            //Debug.Log(sumR +" | "+ sumG +" | "+ sumB);
            return new Color(sumR, sumG, sumB, 1);
        }
        private static bool InsideTexture(int width, int height, int i, int j)
        {
            if (i < 0 || j < 0) return false;
            if (i >= height || j >= width) return false;
            return true;
        }

        public void NormalizeImgProc(int width, int height)
        {
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    imgProc[i, j].r = imgProc[i, j].r - (float)minValue.r;
                    imgProc[i, j].g = imgProc[i, j].g - (float)minValue.g;
                    imgProc[i, j].b = imgProc[i, j].b - (float)minValue.b;
                    maxValue.r = imgProc[i, j].r > maxValue.r ? imgProc[i, j].r : maxValue.r;
                    maxValue.g = imgProc[i, j].g > maxValue.g ? imgProc[i, j].g : maxValue.g;
                    maxValue.b = imgProc[i, j].b > maxValue.b ? imgProc[i, j].b : maxValue.b;
                }
            }
            Debug.Log(maxValue);
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    imgProc[i, j].r = imgProc[i, j].r / maxValue.r;
                    imgProc[i, j].g = imgProc[i, j].g / maxValue.g;
                    imgProc[i, j].b = imgProc[i, j].b / maxValue.b;
                }
            }
        }
    }

}
