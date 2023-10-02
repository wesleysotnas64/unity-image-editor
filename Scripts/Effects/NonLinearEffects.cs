using Scripts.Utils;
using System;
using UnityEngine;


namespace Scripts.Effects
{
    static class NonLinearEffects
    {

        public static Texture2D GenericFilter(float[,]mask,int size, Texture2D inputTexture){
            FilterConv generic = new FilterConv();
            generic.DefFilter(mask, size);
            generic.SetImg(inputTexture);
            generic.ApplyConv();
            Texture2D outputTexture = generic.ReturnImage();
            outputTexture.Apply();
            return outputTexture;
        }
        public static Texture2D Sobel(Texture2D inputTexture, int type, int normalize){
            Texture2D outputTexture = new Texture2D(inputTexture.width, inputTexture.height);
            float[,] sobelHorizontal = new float[3, 3];
            sobelHorizontal[0, 0] = -1;
            sobelHorizontal[0, 1] = -2;
            sobelHorizontal[0, 2] = -1;
            sobelHorizontal[1, 0] = 0;
            sobelHorizontal[1, 1] = 0;
            sobelHorizontal[1, 2] = 0;
            sobelHorizontal[2, 0] = 1;
            sobelHorizontal[2, 1] = 2;
            sobelHorizontal[2, 2] = 1;
            float[,] sobelVertical = new float[3, 3];
            sobelVertical[0, 0] = -1;
            sobelVertical[0, 1] = 0;
            sobelVertical[0, 2] = 1;
            sobelVertical[1, 0] = -2;
            sobelVertical[1, 1] = 0;
            sobelVertical[1, 2] = 2;
            sobelVertical[2, 0] = -1;
            sobelVertical[2, 1] = 0;
            sobelVertical[2, 2] = 1; 
            if(type == 1){
                FilterConv fConv = new FilterConv();
                fConv.DefFilter(sobelVertical, 3);
                fConv.SetImg(inputTexture);
                fConv.ApplyConv();
                if(normalize == 1){
                    fConv.NormalizeImgProc();
                }
                outputTexture = fConv.ReturnImage();
            }
            else if(type == 2){
                FilterConv fConv = new FilterConv();
                fConv.DefFilter(sobelHorizontal, 3);
                fConv.SetImg(inputTexture);
                fConv.ApplyConv();
                if(normalize == 1){
                    fConv.NormalizeImgProc();
                }
                outputTexture = fConv.ReturnImage();
            }else{
                FilterConv vertical = new FilterConv();
                FilterConv horizontal = new FilterConv();
                vertical.DefFilter(sobelVertical, 3);
                horizontal.DefFilter(sobelHorizontal,3);
                vertical.SetImg(inputTexture);
                horizontal.SetImg(inputTexture);
                vertical.ApplyConv();
                horizontal.ApplyConv();
                Color[,] verticalColor = vertical.getImgProc();
                Color[,] horizontalColor = horizontal.getImgProc();
                Color[,] juncaoColor = new Color[inputTexture.height, inputTexture.width];
                for(int i = 0; i < inputTexture.height;i++){
                    for(int j = 0; j < inputTexture.width; j++){
                        juncaoColor[i,j] = verticalColor[i,j] + horizontalColor[i,j];
                    }
                }
                FilterConv ambos = new FilterConv();
                ambos.SetImg(inputTexture);
                ambos.PassImage(juncaoColor);
                if(normalize == 1){
                    ambos.NormalizeImgProc();
                }
                outputTexture = ambos.ReturnImage();

            }
            outputTexture.Apply();
            return outputTexture;
        }

        public static Texture2D Blur(Texture2D inputTexture, int level)
        {
            int size = (level * 2) + 1;
            int qtdPixels = (int) Math.Pow(size, 2);
            float[,] mask = new float[size,size];

            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    mask[i, j] = (float)1 / qtdPixels;
                }
            }

            FilterConv blur = new FilterConv();
            blur.DefFilter(mask, size);
            blur.SetImg(inputTexture);
            blur.ApplyConv();
            Texture2D outputTexture = blur.ReturnImage();
            
            outputTexture.Apply();

            return outputTexture;

        }
    }
}