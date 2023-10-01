using UnityEngine;
using UnityEngine.UI;
using Scripts.Effects;
using Scripts.Utils;
using System.Collections.Generic;
using UnityEngine.Windows;
using UnityEngine.UIElements.Experimental;

public class Manager : MonoBehaviour
{
    [Header("Render Control")]
    public Texture2D inputTexture;
    public Texture2D outputTexture;
    public Image inputRender;
    public Image outputRender;
    public List<Texture2D> effects;
    public int currentEffect;

    [Header("Dropdown Control")]
    public Dropdown dropdown; 

    [Header("Negative Control")]
    public bool negativeActive;
    public GameObject negativePanel;

    [Header("Threshold Control")]
    public bool thresholdActive;
    public float thresholdLevel;
    public GameObject thresholdPanel;
    public Slider thresholdSliderLevel;
    public Text thresholdTextLevel;

    [Header("Blur Control")]
    public bool blurActive;
    public int blurLevel;
    public int blurMaxLevel;
    public GameObject blurPanel;
    public Slider blurSliderLevel;
    public Text blurTextLevel;

    [Header("Gamma Correcition Control")]
    public bool gammaActive;
    public float gammaLevel;
    public float gammaMaxLevel;
    public GameObject gammaPanel;
    public Slider gammaSliderLevel;
    public Text gammaTextLevel;

    [Header("Grey Scale Control")]
    public bool greyScaleActive;
    public float greyScaleMax;
    public float greyScaleWeightRed;
    public float greyScaleWeightGreen;
    public float greyScaleWeightBlue;
    public GameObject greyScalePanel;
    public Slider greyScaleWeightRedSlider;
    public Slider greyScaleWeightGreenSlider;
    public Slider greyScaleWeightBlueSlider;
    public Text greyScaleWeightRedText;
    public Text greyScaleWeightGreenText;
    public Text greyScaleWeightBlueText;

    [Header("Pixelization Control")]
    public bool pixelizationActive;
    public int pixelizationPixelSize;
    public int pixelizationPixelSizeMax;
    public GameObject pixelizationPanel;
    public Slider pixelizationPixelSizeSlider;
    public Text pixelizationPixelSizeText;

    [Header("Hitogram Control")]
    public bool histogramActive;
    public GameObject histogramPanel;
    public Histogram histogramObj;

    [Header("Sobel Control")]
    public bool sobelActive;
    public GameObject sobelPanel;
    public Slider normalizerSlider;
    public int normalizerValue;
    public int sobelValue;

    private void Start()
    {
        negativeActive = false;
        thresholdActive = false;
        blurActive = false;
        gammaActive = false;
        greyScaleActive = false;
        pixelizationActive = false;
        histogramActive = false;
        sobelActive = false;

        //histogramObj = new Histogram();

        blurMaxLevel = 4;
        gammaMaxLevel = 2.0f;

        greyScaleMax = 5.0f;
        greyScaleWeightRed = 1.0f;
        greyScaleWeightGreen = 1.0f;
        greyScaleWeightBlue = 1.0f;
        pixelizationPixelSizeMax = 32;

        effects = new List<Texture2D>();
        currentEffect = 0;
        if(inputRender != null)
        {
            if(inputTexture == null)
            {
                inputTexture = LinearEffects.ColorFromDist(new Texture2D(300, 300));
            }

            effects.Add(inputTexture);
        }

        RenderInput();
        RenderManager();

        UpdatePanels();
    }



    public void RenderManager()
    {
        SelectEffect();

        RenderOutput();
    }

    private void RenderInput()
    {
        if(inputRender != null)
        {
            Texture2D renderTexture = effects[currentEffect];
            inputRender.sprite = Sprite.Create(
                    renderTexture,
                    new Rect(0, 0, renderTexture.width, renderTexture.height),
                    Vector2.zero
            );
        }
    }

    private void RenderOutput()
    {
        if (outputRender != null)
        {
            outputRender.sprite = Sprite.Create(
                    outputTexture,
                    new Rect(0, 0, outputTexture.width, outputTexture.height),
                    Vector2.zero
            );
        }
    }

    private void SelectEffect()
    {
        Texture2D renderTexture = effects[currentEffect];
        if (negativeActive)
        {
            outputTexture = LinearEffects.Negative(renderTexture);
        }
        else if (thresholdActive)
        {
            outputTexture = LinearEffects.Threshold(renderTexture, thresholdLevel);
        }
        else if (blurActive)
        {
            outputTexture = NonLinearEffects.Blur(renderTexture, blurLevel);
        }
        else if (gammaActive)
        {
            outputTexture = LinearEffects.GammaCorrection(renderTexture, gammaLevel);
        }
        else if (greyScaleActive)
        {
            outputTexture = LinearEffects.GrayScale(renderTexture, greyScaleWeightRed, greyScaleWeightGreen, greyScaleWeightBlue);
        }
        else if (pixelizationActive)
        {
            outputTexture = LinearEffects.Pixelization(renderTexture, pixelizationPixelSize);
        }
        else if ( histogramActive )
        {
            histogramObj = new Histogram();
            histogramObj.CalcHistogram(renderTexture);
            outputTexture = histogramObj.EqualizeHistogram(renderTexture);
        }
        else if ( sobelActive )
        {
            outputTexture = NonLinearEffects.Sobel(renderTexture, sobelValue, normalizerValue);
        }
        else
        {
            //outputTexture = renderTexture;
            outputTexture = LinearEffects.ColorFromDist(renderTexture);
        }
    }

    public void Undo()
    {
        if (currentEffect > 0)
        {
            currentEffect--;
        }

        UpdatePanels();
        RenderInput();
        RenderManager();
    }

    public void Redo()
    {
        if(currentEffect < effects.Count - 1)
        {
            currentEffect++;
        }
        UpdatePanels();
        RenderInput();
        RenderManager();
    }

    public void ApplyEffect()
    {
        effects.Add(outputTexture);
        currentEffect = effects.Count - 1;

        UpdatePanels();
        RenderInput();
        RenderManager();

    }

    public void UpdatePanels()
    {
        DropdownManager();
        PanelManager();
    }

    private void DropdownManager()
    {
        if (dropdown != null)
        {
            int option = dropdown.value;

            switch (option)
            {
                case 0: // Desativado
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    break;

                case 1: //Negativo
                    negativeActive = true;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    break;

                case 2: // Threshold
                    negativeActive = false;
                    thresholdActive = true;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    break;

                case 3: //Blur
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = true;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    break;

                case 4: //Gamma Correction
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = true;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    break;

                case 5: //Grey Scale
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = true;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    break;

                case 6: //Pixelization
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = true;
                    histogramActive = false;
                    sobelActive = false;
                    break;

                case 7: //Histogram
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = true;
                    sobelActive = false;
                    break;

                case 8: //Sobel
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = true;
                    break;

                default:
                    break;
            }
        }
    }
    public void SetValue(int valueSet){
            sobelValue = valueSet; 
    }
    private void PanelManager()
    {
        //Altera visibilidade dos pain�is.
        negativePanel.SetActive(negativeActive);
        thresholdPanel.SetActive(thresholdActive);
        blurPanel.SetActive(blurActive);
        gammaPanel.SetActive(gammaActive);
        greyScalePanel.SetActive(greyScaleActive);
        pixelizationPanel.SetActive(pixelizationActive);
        histogramPanel.SetActive(histogramActive);
        sobelPanel.SetActive(sobelActive);
        
        if( negativeActive )
        {   
        }
        else if( thresholdActive)
        {
            thresholdLevel = thresholdSliderLevel.value;
            thresholdTextLevel.text = thresholdLevel.ToString();
        }
        else if( blurActive )
        {
            blurLevel = (int) (blurSliderLevel.value * blurMaxLevel);
            blurSliderLevel.value = (float) blurLevel/blurMaxLevel;
            blurTextLevel.text = blurLevel.ToString();

        }
        else if( gammaActive )
        {
            gammaLevel = gammaSliderLevel.value * gammaMaxLevel;
            gammaSliderLevel.value = gammaLevel/gammaMaxLevel;
            gammaTextLevel.text = gammaLevel.ToString();
        }
        else if ( greyScaleActive )
        {
            greyScaleWeightRed = greyScaleWeightRedSlider.value * greyScaleMax;
            greyScaleWeightRedText.text = (greyScaleWeightRed/greyScaleMax).ToString();

            greyScaleWeightGreen = greyScaleWeightGreenSlider.value * greyScaleMax;
            greyScaleWeightGreenText.text = (greyScaleWeightGreen / greyScaleMax).ToString();

            greyScaleWeightBlue = greyScaleWeightBlueSlider.value * greyScaleMax;
            greyScaleWeightBlueText.text = (greyScaleWeightBlue / greyScaleMax).ToString();
        }
        else if ( pixelizationActive )
        {
            pixelizationPixelSize = (int) (pixelizationPixelSizeSlider.value * pixelizationPixelSizeMax);
            pixelizationPixelSizeText.text = pixelizationPixelSize.ToString();
        }
        else if( sobelActive )
        {   
            normalizerValue = (int)normalizerSlider.value;
        }
    }

}
