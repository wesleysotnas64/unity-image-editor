using Scripts.Utils;
using System;
using UnityEngine;


namespace Scripts.Effects
{
    static class NonLinearEffects
    {
        public static Texture2D GaussianFilter(Texture2D inputTexture, int size){
            int somatorio = 0;
            float[,] mask = new float[size,size];
            if(size == 3){
                mask[0,0] = 1.0f;
                mask[0,1] = 2.0f;
                mask[0,2] = 1.0f;
                mask[1,0] = 2.0f;
                mask[2,0] = 1.0f;
                for(int i = 1; i < size;i++){
                    for(int j = 1; j < size;j++){
                        mask[i,j] = mask[0,j] * mask[i,0];
                    }
                }
                for(int i = 0; i < size;i++){
                    for(int j = 0; j < size;j++){
                        somatorio += (int)mask[i,j];
                    }
                }
                for(int i = 0; i < size;i++){
                    for(int j = 0; j < size;j++){
                       mask[i,j] = mask[i,j]/somatorio; 
                    }
                }
            }
            else if(size == 5){
                mask[0,0] = 1.0f;
                mask[0,1] = 4.0f;
                mask[0,2] = 6.0f;
                mask[0,3] = 4.0f;
                mask[0,4] = 1.0f;
                mask[1,0] = 4.0f;
                mask[2,0] = 6.0f;
                mask[3,0] = 4.0f;
                mask[4,0] = 1.0f;
                for(int i = 1; i < size;i++){
                    for(int j = 1; j < size;j++){
                        mask[i,j] = mask[0,j] * mask[i,0];
                    }
                }
                for(int i = 0; i < size;i++){
                    for(int j = 0; j < size;j++){
                        somatorio += (int)mask[i,j];
                    }
                }
                for(int i = 0; i < size;i++){
                    for(int j = 0; j < size;j++){
                       mask[i,j] = mask[i,j]/somatorio; 
                    }
                }
            }
            else if(size == 7){
                mask[0,0] = 1.0f;
                mask[0,1] = 6.0f;
                mask[0,2] = 15.0f;
                mask[0,3] = 20.0f;
                mask[0,4] = 15.0f;
                mask[0,5] = 6.0f;
                mask[0,6] = 1.0f;
                mask[1,0] = 6.0f;
                mask[2,0] = 15.0f;
                mask[3,0] = 20.0f;
                mask[4,0] = 15.0f;
                mask[5,0] = 6.0f;
                mask[6,0] = 1.0f;
                for(int i = 1; i < size;i++){
                    for(int j = 1; j < size;j++){
                        mask[i,j] = mask[0,j] * mask[i,0];
                    }
                }
                for(int i = 0; i < size;i++){
                    for(int j = 0; j < size;j++){
                        somatorio += (int)mask[i,j];
                    }
                }
                for(int i = 0; i < size;i++){
                    for(int j = 0; j < size;j++){
                       mask[i,j] = mask[i,j]/somatorio; 
                    }
                }
            }
            else if(size == 9){
                mask[0,0] = 1.0f;
                mask[0,1] = 8.0f;
                mask[0,2] = 28.0f;
                mask[0,3] = 56.0f;
                mask[0,4] = 70.0f;
                mask[0,5] = 56.0f;
                mask[0,6] = 28.0f;
                mask[0,7] = 8.0f;
                mask[0,8] = 1.0f;
                mask[1,0] = 8.0f;
                mask[2,0] = 28.0f;
                mask[3,0] = 56.0f;
                mask[4,0] = 70.0f;
                mask[5,0] = 56.0f;
                mask[6,0] = 28.0f;
                mask[7,0] = 8.0f;
                mask[8,0] = 1.0f;
                for(int i = 1; i < size;i++){
                    for(int j = 1; j < size;j++){
                        mask[i,j] = mask[0,j] * mask[i,0];
                    }
                }
                for(int i = 0; i < size;i++){
                    for(int j = 0; j < size;j++){
                       somatorio += (int)mask[i,j]; 
                    }
                }
                for(int i = 0; i < size;i++){
                    for(int j = 0; j < size;j++){
                       mask[i,j] = mask[i,j]/somatorio; 
                    }
                }
            }
            FilterConv gauss = new FilterConv();
            gauss.DefFilter(mask,size);
            gauss.SetImg(inputTexture);
            gauss.ApplyConv();
            Texture2D outputTexture = gauss.ReturnImage();
            outputTexture.Apply();
            return outputTexture;
        }
        public static Texture2D Median(Texture2D inputTexture, int size){
            float[,] mask = new float[size,size];
            for(int i =0; i < size; i++){
                for(int j =0; j < size; j++){
                    mask[i,j] = 1;
                }
            }
            FilterConv median = new FilterConv();
            median.DefFilter(mask,size);
            median.SetImg(inputTexture);
            median.ApplyMedian();
            Texture2D outputTexture = median.ReturnImage();
            outputTexture.Apply();
            return outputTexture;
        }
        public static Texture2D HighBoost(Texture2D inputTexture, float coeficient, int normalize, int size){
            size = (size*2)+1;
            Texture2D output = new Texture2D(inputTexture.width, inputTexture.height);
            Texture2D blurImage = GaussianFilter(inputTexture,size);
            Color[,] realce = new Color[inputTexture.height, inputTexture.width];
            Color[,] image = new Color[inputTexture.height, inputTexture.width];
            Color[,] final = new Color[inputTexture.height, inputTexture.width];
            int xText;
            int ytext = 0;
            for(int i = 0; i< inputTexture.height; i++){
                xText = 0;
                for(int j = 0; j< inputTexture.width; j++){
                    realce[i,j] = inputTexture.GetPixel(xText,ytext) - blurImage.GetPixel(xText,ytext);
                    image[i,j] = inputTexture.GetPixel(xText,ytext);
                    final[i,j] = image[i,j] - (coeficient*realce[i,j]);
                    xText++;
                }
                ytext++;
            }
            output.Apply();
            FilterConv finalOutput = new FilterConv();
            finalOutput.SetImg(output);
            finalOutput.PassImage(final);
            if(normalize == 1){
               finalOutput.NormalizeImgProc(); 
            }
            output = finalOutput.ReturnImage();
            output.Apply();
            return output;


        }
        public static Texture2D FilterLaplace(int type,int normalize, Texture2D inputTexture){
            float[,] mask = new float[3,3];
            if(type == 1){
                mask[0,0] = 0;
                mask[0,1] = 1.0f;
                mask[0,2] = 0;
                mask[1,0] = 1.0f;
                mask[1,1] = -4.0f;
                mask[1,2] = 1.0f;
                mask[2,0] = 0;
                mask[2,1] = 1.0f;
                mask[2,2] = 0;
            }
            if(type == 2){
                mask[0,0] = 1.0f;
                mask[0,1] = 1.0f;
                mask[0,2] = 1.0f;
                mask[1,0] = 1.0f;
                mask[1,1] = -8.0f;
                mask[1,2] = 1.0f;
                mask[2,0] = 1.0f;
                mask[2,1] = 1.0f;
                mask[2,2] = 1.0f;
            }
            if(type == 3){
                mask[0,0] = 0;
                mask[0,1] = -1.0f;
                mask[0,2] = 0;
                mask[1,0] = -1.0f;
                mask[1,1] = 4.0f;
                mask[1,2] = -1.0f;
                mask[2,0] = 0;
                mask[2,1] = -1.0f;
                mask[2,2] = 0;
            }
            if(type == 4){
                mask[0,0] = -1.0f;
                mask[0,1] = -1.0f;
                mask[0,2] = -1.0f;
                mask[1,0] = -1.0f;
                mask[1,1] = 8.0f;
                mask[1,2] = -1.0f;
                mask[2,0] = -1.0f;
                mask[2,1] = -1.0f;
                mask[2,2] = -1.0f;
            }
            FilterConv laplace = new FilterConv();
            laplace.DefFilter(mask,3);
            laplace.SetImg(inputTexture);
            laplace.ApplyConv();
            if(normalize == 2){
                laplace.NormalizeImgProc();
            }
            Texture2D outputTexture = laplace.ReturnImage();
            outputTexture.Apply();
            return outputTexture;
        }
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