using Scripts.Utils;
using System;
using UnityEngine;

namespace Scripts.Effects
{
    static class NonLinearEffects
    {

        public static Texture2D Blur(Texture2D inputTexture, int level = 1)
        {
            //Preparando o filtro de desfoque
            //int size = (level * 2) + 1;
            //int qtdPixels = (int) Math.Pow(size, 2);
            //float[,] filter = new float[size, size];

            //for (int i = 0; i < size; i++)
            //{
            //    for(int j = 0; j < size; j++)
            //    {
            //        filter[i, j] = (1 / qtdPixels);
            //    }
            //}

            //FilterConv fConv = new FilterConv();
            //fConv.DefFilter(filter, size);

            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            //outputTexture = fConv.Conv(inputTexture);

            int width = inputTexture.width;
            int height = inputTexture.height;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    outputTexture.SetPixel(
                        i, j,
                        CalcKernel(inputTexture, i, j, level)
                    );
                }
            }

            outputTexture.Apply();

            return outputTexture;

        }

        private static Color CalcKernel(Texture2D texture, int x, int y, int level)
        {

            int qtdPixels = (int) Math.Pow((level * 2) + 1, 2);

            Color px;
            float sumR = 0;
            float sumG = 0;
            float sumB = 0;
            for (int i = (x-level); i <= (x+level); i++)
            {
                for(int j = (y-level); j <= (y+level); j++)
                {
                    if(InsideTexture(texture.width, texture.height, i, j))
                    {
                        px = texture.GetPixel(i, j);
                        sumR += (px.r)*((float)1/qtdPixels);
                        sumG += (px.g)*((float)1/qtdPixels);
                        sumB += (px.b)*((float)1/qtdPixels);
                    }
                }
            }

            return new Color(sumR, sumG, sumB, 1);
        }

        private static bool InsideTexture(int width, int height, int i, int j)
        {
            if(i < 0 || j < 0) return false;
            if (i >= width || j >= height) return false;
            return true;
        }
    }
}