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
    public static Texture2D Scale(Texture2D inputTexture, double scaleValue, int typeTransform){
        int width = Convert.ToInt32(Convert.ToDouble(inputTexture.width)*scaleValue);
        int height = Convert.ToInt32(Convert.ToDouble(inputTexture.height)*scaleValue);
        Texture2D outputTexture = new Texture2D(width,height);
        if(typeTransform == 1){
            for(int i = 0; i < width; i++){
                for(int j = 0; j< height; j++){
                    int heightOriginal = Convert.ToInt32(j/scaleValue);
                    int widthOriginal = Convert.ToInt32(i/scaleValue);
                    outputTexture.SetPixel(i, j, inputTexture.GetPixel(widthOriginal,heightOriginal));
                }
            }
        }
        outputTexture.Apply();
        return outputTexture;
    }
}
