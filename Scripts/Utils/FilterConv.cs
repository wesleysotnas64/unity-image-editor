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
        private int height;
        private int width;
        private Color[,] img;
        private Texture2D texture;

        public FilterConv()
        {
            minValue = new Color(99999,99999,99999,1);
            maxValue = new Color(-99999, -99999, -99999, 1);
        }

        public Color CalcKernelMedian(int width, int height, int i, int j, int desloc)
        {
            Color px;
            int pos = 0;
            float[] colors = new float[sizeMask * sizeMask];
            for (int iImg = (i - desloc); iImg <= (i + desloc); iImg++)
            {
                for (int jImg = (j - desloc); jImg <= (j + desloc); jImg++)
                {
                    if (InsideTexture(width, height, iImg, jImg))
                    {
                        px = img[iImg, jImg];
                        colors[pos] = px.r;
                        pos += 1;
                    }
                }
            }
            Array.Sort(colors);
            Color color = new Color(colors[(sizeMask * sizeMask) / 2], colors[(sizeMask * sizeMask) / 2], colors[(sizeMask * sizeMask) / 2], 1);
            return color;
        }

        public void ApplyMedian()
        {
            int desloc = (sizeMask - 1) / 2;
            imgProc = new Color[this.height, this.width];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    imgProc[i, j] = CalcKernelMedian(this.width, this.height, i, j, desloc);
                }
            }
        }

        public Texture2D ReturnImage(){
            Texture2D final = new Texture2D(this.width, this.height);
            int xText;
            int yText = 0;
            for (int i = 0; i < this.height; i++)
            {
                xText = 0;
                for (int j = 0; j < this.width; j++)
                {
                    final.SetPixel(xText, yText, imgProc[i, j]);
                    xText++;
                }
                yText++;
            }
            final.Apply();
            return final;
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

            return new Color(sumR, sumG, sumB, 1);
        }
        private static bool InsideTexture(int width, int height, int i, int j)
        {
            if (i < 0 || j < 0) return false;
            if (i >= height || j >= width) return false;
            return true;
        }

        public void NormalizeImgProc()
        {
            for(int i = 0; i < this.height; i++)
            {
                for(int j = 0; j < this.width; j++)
                {
                    imgProc[i, j].r = imgProc[i, j].r - (float)minValue.r;
                    imgProc[i, j].g = imgProc[i, j].g - (float)minValue.g;
                    imgProc[i, j].b = imgProc[i, j].b - (float)minValue.b;
                    maxValue.r = imgProc[i, j].r > maxValue.r ? imgProc[i, j].r : maxValue.r;
                    maxValue.g = imgProc[i, j].g > maxValue.g ? imgProc[i, j].g : maxValue.g;
                    maxValue.b = imgProc[i, j].b > maxValue.b ? imgProc[i, j].b : maxValue.b;
                }
            }
            for(int i = 0; i < this.height; i++)
            {
                for(int j = 0; j < this.width; j++)
                {
                    imgProc[i, j].r = imgProc[i, j].r / maxValue.r;
                    imgProc[i, j].g = imgProc[i, j].g / maxValue.g;
                    imgProc[i, j].b = imgProc[i, j].b / maxValue.b;
                }
            }
        }

        public void PassImage(Color[,] image){
            imgProc = new Color[this.height, this.width];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    imgProc[i, j] = image[i, j];

                    minValue.r = imgProc[i, j].r < minValue.r ? imgProc[i, j].r : minValue.r;
                    minValue.g = imgProc[i, j].g < minValue.g ? imgProc[i, j].g : minValue.g;
                    minValue.b = imgProc[i, j].b < minValue.b ? imgProc[i, j].b : minValue.b;
                }
            }
        }
        public Color[,] getImgProc(){
            return this.imgProc;
        }
        
        public void ApplyConv()
        {
            int desloc = (sizeMask - 1) / 2;
            imgProc = new Color[this.height, this.width];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    imgProc[i, j] = CalcKernel(this.width, this.height, i, j, desloc);
                }
            }
        }
        public void SetImg(Texture2D texture){
            this.width = texture.width;
            this.height = texture.height;
            this.texture = texture;
            img = new Color[this.height, this.width];
            int xText;
            int yText = 0;
            for (int i = 0; i < this.height; i++)
            {
                xText = 0;
                for (int j = 0; j < this.width; j++)
                {
                    img[i, j] = this.texture.GetPixel(xText, yText);
                    xText++;
                }
                yText++;
            }
        }

        public void DefFilter(float[,] mask, int sizeMask)
        {
            this.sizeMask = sizeMask;
            this.mask = new float[sizeMask, sizeMask];
            this.mask = mask;
        }

        public Color[,] GetImg()
        {
            return this.img;
        }
    }

}
