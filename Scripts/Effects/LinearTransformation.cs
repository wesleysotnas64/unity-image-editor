using System.Collections;
using UnityEngine;
using System;

public class LinearTransformation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Color InterpolacaoLinearRotacao(Texture2D inputTexture, double x, double y){
        double xComplement = Convert.ToDouble(x - Convert.ToInt32(x));
        double yComplement = Convert.ToDouble(y - Convert.ToInt32(y));
        xComplement = Math.Round(xComplement,3);
        yComplement = Math.Round(yComplement,3);
        Color posX1 = inputTexture.GetPixel((int)x, (int)y);
        Color posX2 = inputTexture.GetPixel((int)x + 1, (int)y);
        Color posX3 = inputTexture.GetPixel((int)x, (int)y+1);
        Color posX4 = inputTexture.GetPixel((int)x + 1, (int)y+1);
        Color[] finalInterpolation = new Color[2];
        finalInterpolation[0].r = (float)((1-xComplement)*posX1.r + (xComplement)*posX2.r);
        finalInterpolation[0].g = (float)((1-xComplement)*posX1.g + (xComplement)*posX2.g);
        finalInterpolation[0].b = (float)((1-xComplement)*posX1.b + (xComplement)*posX2.b);
        finalInterpolation[1].r = (float)((1-xComplement)*posX3.r + (xComplement)*posX4.r);
        finalInterpolation[1].g = (float)((1-xComplement)*posX3.g + (xComplement)*posX4.g);
        finalInterpolation[1].b = (float)((1-xComplement)*posX3.b + (xComplement)*posX4.b);
        Color corFinal = new Color();
        corFinal.r = (float)((1-yComplement) * finalInterpolation[0].r + (yComplement) * finalInterpolation[1].r);
        corFinal.g = (float)((1-yComplement) * finalInterpolation[0].g + (yComplement) * finalInterpolation[1].g);
        corFinal.b = (float)((1-yComplement) * finalInterpolation[0].b + (yComplement) * finalInterpolation[1].b);
        corFinal.a = 1;
        return corFinal;

    }
    public static Texture2D Rotacao(Texture2D inputTexture, int angValue, int typeTransform){
        float cosseno = (float)Math.Round(Math.Cos((Math.PI/180) * angValue),2);
        float seno = (float)Math.Round(Math.Sin((Math.PI/180) * angValue),2);
        int[] center = new int[2];
        center[0] = (int)inputTexture.width/2;
        center[1] = (int)inputTexture.height/2;
        double[ , ] matrizRotacao = new double[2,2];
        matrizRotacao[0,0] = cosseno;
        matrizRotacao[0,1] = -seno;
        matrizRotacao[1,0] = seno;
        matrizRotacao[1,1] = cosseno;
        int[ , ] matrizPositionsX = new int[inputTexture.height,inputTexture.width]; 
        int[ , ] matrizPositionsY = new int[inputTexture.height,inputTexture.width]; 
        int[] maxPos = new int[2];
        int[] minPos = new int[2];

        for(int i = 0; i < inputTexture.height; i++){
            for(int j = 0; j < inputTexture.width; j++){
                matrizPositionsX[i,j] = Convert.ToInt32((j * matrizRotacao[1,0]) + (i * matrizRotacao[1,1]));
                matrizPositionsY[i,j] = Convert.ToInt32((j * matrizRotacao[0,0]) + (i *matrizRotacao[0,1])); 
            }
        }
        for(int i = 0; i <inputTexture.height; i++){
            for(int j = 0; j< inputTexture.width;j++){
                if(matrizPositionsX[i, j] > maxPos[0]){
                    maxPos[0] = matrizPositionsX[i, j];
                }
                if(matrizPositionsX[i, j] < minPos[0]){
                    minPos[0] = matrizPositionsX[i, j];
                }
                if(matrizPositionsY[i, j] > maxPos[1]){
                    maxPos[1] = matrizPositionsY[i, j];
                }
                if(matrizPositionsY[i, j] < minPos[1]){
                    minPos[1] = matrizPositionsY[i, j];
                }
            }
        }
        Texture2D outputTexture = new Texture2D(maxPos[1] - minPos[1],maxPos[0] - minPos[0]);
        Color black = new Color(0,0,0,1);
        for(int i =0; i < outputTexture.height; i++){
            for(int j=0; j < outputTexture.width;j++){
                outputTexture.SetPixel(i,j, black);
            }
        }
        if(typeTransform == 1){
            for(int i =0; i < inputTexture.height; i++){
                for(int j=0; j < inputTexture.width;j++){
                    int xCord = Convert.ToInt32((j * matrizRotacao[0,0]) + (i * matrizRotacao[0,1]));
                    int yCord = Convert.ToInt32((j * matrizRotacao[1,0]) + (i * matrizRotacao[1,1]));
                    outputTexture.SetPixel(xCord,yCord, inputTexture.GetPixel(j,i));
                }
            }
        }else if(typeTransform == 2){
            for(int i =0; i < inputTexture.height; i++){
                for(int j=0; j < inputTexture.width;j++){
                    int xCord = Convert.ToInt32(((j) * matrizRotacao[0,0]) + ((i) * matrizRotacao[0,1]));
                    int yCord = Convert.ToInt32(((j) * matrizRotacao[1,0]) + ((i) * matrizRotacao[1,1]));
                    double xCordCalc = ((j) * matrizRotacao[0,0] + (i) * matrizRotacao[0,1]);
                    double yCordCalc = ((j) * matrizRotacao[1,0] + (i) * matrizRotacao[1,1]);
                    if(xCordCalc > 0 && yCordCalc > 0){
                        outputTexture.SetPixel(xCord,yCord, InterpolacaoLinearRotacao(inputTexture,xCordCalc, yCordCalc));
                    }
                }
            }
        }
        outputTexture.Apply();
        return outputTexture;
    }
    public static Color InterpolacaoLinear(Texture2D inputTexture, int x, int y, double scale){
        int[] positions = new int[2];
        double xPos = x/scale;
        float xComplement = (float)Convert.ToDouble(xPos - Convert.ToInt32(xPos));
        double yPos = y/scale;
        float yComplement = (float)Convert.ToDouble(yPos - Convert.ToInt32(yPos)); 
        Color posX1 = inputTexture.GetPixel((int)xPos, (int)yPos);
        Color posX2 = inputTexture.GetPixel((int)xPos + 1, (int)yPos);
        Color posX3 = inputTexture.GetPixel((int)xPos, (int)yPos+1);
        Color posX4 = inputTexture.GetPixel((int)xPos + 1, (int)yPos+1);
        Color[] finalInterpolation = new Color[2];
        finalInterpolation[0].r = (float)((1-xComplement)*posX1.r + (xComplement)*posX2.r);
        finalInterpolation[0].g = (float)((1-xComplement)*posX1.g + (xComplement)*posX2.g);
        finalInterpolation[0].b = (float)((1-xComplement)*posX1.b + (xComplement)*posX2.b);
        finalInterpolation[1].r = (float)((1-xComplement)*posX3.r + (xComplement)*posX4.r);
        finalInterpolation[1].g = (float)((1-xComplement)*posX3.g + (xComplement)*posX4.g);
        finalInterpolation[1].b = (float)((1-xComplement)*posX3.b + (xComplement)*posX4.b);
        Color corFinal = new Color();
        corFinal.r = (float)((1-yComplement) * finalInterpolation[0].r + (yComplement) * finalInterpolation[1].r);
        corFinal.g = (float)((1-yComplement) * finalInterpolation[0].g + (yComplement) * finalInterpolation[1].g);
        corFinal.b = (float)((1-yComplement) * finalInterpolation[0].b + (yComplement) * finalInterpolation[1].b);
        corFinal.a = 1;
        return corFinal;

    }
    public static int[] InterpolacaoDoMaisProximo(int x, int y, double scale){
        int[] positions = new int[2];
        positions[0] = Convert.ToInt32(x/scale);
        positions[1] = Convert.ToInt32(y/scale);
        return positions;
    }   
    public static Texture2D Scale(Texture2D inputTexture, double scaleValue, int typeTransform){
        int width = Convert.ToInt32(Convert.ToDouble(inputTexture.width)*scaleValue);
        int height = Convert.ToInt32(Convert.ToDouble(inputTexture.height)*scaleValue);
        Texture2D outputTexture = new Texture2D(width,height);
        if(typeTransform == 1){
            for(int i = 0; i < width; i++){
                for(int j = 0; j< height; j++){
                    int[] positions = InterpolacaoDoMaisProximo(i,j,scaleValue);
                    outputTexture.SetPixel(i, j, inputTexture.GetPixel(positions[0],positions[1]));
                }
            }
        }else if(typeTransform == 2){
            for(int i = 0; i < width; i++){
                for(int j = 0; j< height; j++){
                    outputTexture.SetPixel(i, j,InterpolacaoLinear(inputTexture,i,j, scaleValue));
                }
            }
        }
        outputTexture.Apply();
        return outputTexture;
    }
}
