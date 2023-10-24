using UnityEngine;
using UnityEngine.UI;
using Scripts.Effects;
using Scripts.Utils;
using System.Collections.Generic;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Networking;
using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class Manager : MonoBehaviour
{
    [Header("Render Control")]
    public Texture2D inputTexture;
    public Texture2D outputTexture;
    public Image inputRender;
    public Image outputRender;
    public List<Texture2D> effects;
    public int currentEffect;

    [Header("histogram Controloutput")]
    public GameObject histogramPanelOutput;
    public bool histogramPanelActive;
    public Texture2D histogramRed;
    public Texture2D histogramGreen;
    public Texture2D histogramBlue;
    public Image histogramRedOutput;
    public Image histogramGreenOutput;
    public Image histogramBlueOutput;
    public bool applyHisto;

    [Header("Dropdown Control")]
    public Dropdown dropdown; 

    [Header("Negative Control")]
    public bool negativeActive;
    public GameObject negativePanel;

    [Header("Threshold Control")]
    public bool thresholdActive;
    public float rgbLevel;
    public float rLevel;
    public float gLevel;
    public float bLevel;

    public Text thresholdTypeText;
    public int thresholdType;
    public Slider thresholdTypeSlider;
    public GameObject thresholdPanel;
    public Slider rgbSliderLevel;
    public Text rgbTextLevel;
    public Slider rSliderLevel;
    public Text rTextLevel;
    public Slider gSliderLevel;
    public Text gTextLevel;
    public Slider bSliderLevel;
    public Text bTextLevel;

    [Header("Blur Control")]
    public bool blurActive;
    public int blurLevel;
    public int blurMaxLevel;
    public GameObject blurPanel;
    public Slider blurSliderLevel;
    public Text blurTextLevel;

    [Header("Gamma Correcition Control")]
    public bool gammaActive;
    public bool gammaRGBActive;
    public float gammaRedLevel;
    public float gammaGreenLevel;
    public float gammaBlueLevel;
    public float gammaRGBLevel;
    public float gammaMaxLevel;
    public GameObject gammaPanel;
    public Toggle gammaRGBActiveToggle;
    public Slider gammaRedSliderLevel;
    public Slider gammaGreenSliderLevel;
    public Slider gammaBlueSliderLevel;
    public Slider gammaRGBSliderLevel;
    public Text gammaRedTextLevel;
    public Text gammaGreenTextLevel;
    public Text gammaBlueTextLevel;
    public Text gammaRGBTextLevel;

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

    [Header("Generic FIlter Control")]
    public bool genericActive;
    public GameObject genericFilterPanel;
    public int[,] genericMask = new int[10,10];
    int quantity = 0;
    int sizeGenericMask = 3;
    public int multiplier;
    public Slider multiplierSlide;
    public Text multiplierText;
    public List<Text> genericTextsLine1;
    public List<Text> genericTextsLine2;
    public List<Text> genericTextsLine3;
    public List<Text> genericTextsLine4;
    public List<Text> genericTextsLine5;
    public List<Text> genericTextsLine6;
    public List<Text> genericTextsLine7;
    public List<Text> genericTextsLine8;
    public List<Text> genericTextsLine9;

    [Header("Color fragmentation")]
    public bool colorFragmentationActive;
    public int limit;
    public int colorRLevel;
    public int colorGLevel;
    public int colorBLevel;
    public Color32 colorbase;
    public GameObject colorFragmentationPanel;
    public Slider limitSlider;
    public Text limitTextLevel;
    public Slider rColorSliderLevel;
    public Text rColorTextLevel;
    public Slider gColorSliderLevel;
    public Text gColorTextLevel;
    public Slider bColorSliderLevel;
    public Text bColorTextLevel;

    [Header("Chrome key")]
    public bool chromaKeyActive;
    public int chromaLimit;
    public int chromaRLevel;
    public int chromaGLevel;
    public int chromaBLevel;
    public Texture2D imgBase;
    public GameObject chromaKeyPanel;
    public Slider chromaLimitSlider;
    public Text chromaLimitTextLevel;
    public Slider rChromaSliderLevel;
    public Text rChromaTextLevel;
    public Slider gChromaSliderLevel;
    public Text gChromaTextLevel;
    public Slider bChromaSliderLevel;
    public Text bChromaTextLevel;
    public Image inputKey;

    [Header("HSV")]
    public bool hsvActive;
    public GameObject hsvPanel;
    public Slider hLevel;
    public Slider sLevel;
    public Slider vLevel;
    public Text hText;
    public Text sText;
    public Text vText;
    public int h;
    public double s;
    public double v;

    [Header("Scale")]
    public float scaleValue;
    public bool scaleActive;
    public Slider scaleSlider;
    public Slider typeTransformSlider;
    public Text scaleValueText;
    public Text typeTransformText;
    public GameObject scalePanel;
    public int typeTransform = 1;

    [Header("Rotation")]
    public int angValue;
    public bool rotationActive;
    public Slider angSlider;
    public Slider typeTransformRotSlider;
    public Text angValueText;
    public Text typeTransformRotText;
    public GameObject angPanel;
    public int typeTransformRot = 1;

    [Header("logaritimic Transformation")]

    public GameObject transformLogaritimicPanel;
    public bool transformationLogaritimicActive;
    public bool rgbLogaritmActive;
    public int rCValue;
    public int bCValue;
    public int gCValue;
    public int rgbCValue;

    public Slider rCSlider;
    public Slider bCSlider;
    public Slider gCSlider;
    public Slider rgbCSlider;

    public Text rCText;
    public Text gCText;
    public Text bCText;
    public Text rgbCText;

    public Toggle toggleRGBActive;

    [Header("Linear por Partes Control")]
    public bool piecewiseLinearActive;
    public GameObject piecewiseLinearPanel;

    [Header("Laplacian Filter")]

    public GameObject laplacianPanel;
    public bool laplacianActive;
    public int normalizeLaplace;
    public Slider normalizeLaplaceSlider;

    [Header("Filtro mediana")]

    public GameObject medianPanel;
    public bool medianActive;

    [Header("Filtro Gaussie")]

    public GameObject gaussPanel;
    public bool gaussActive;

    [Header("High Boost")]

    public GameObject highBoostPanel;
    public bool highBoostActive;
    public float coeficient;
    public int normalizeHighBoost;
    public int sizeHighBoost;
    public Slider coeficientSlider;
    public Slider normalizeHighBoostSlider;
    public Slider sizeHighBoostSlider;
    public Text coeficientText;
    public Text sizeHighBoostText;

    [Header("Furrier Transform Control")]
    public bool furrierTransformActive;
    public GameObject furrierTransformPanel;

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
        colorFragmentationActive = false;
        hsvActive = false;
        scaleActive = false;
        histogramPanelActive = false;
        transformationLogaritimicActive = false;
        rgbLogaritmActive = true;
        toggleRGBActive.isOn = rgbLogaritmActive;

        laplacianActive = false;
        medianActive = false;
        gaussActive = false;

        blurMaxLevel = 4;
        gammaMaxLevel = 2.0f;

        greyScaleMax = 5.0f;
        greyScaleWeightRed = 1.0f;
        greyScaleWeightGreen = 1.0f;
        greyScaleWeightBlue = 1.0f;
        pixelizationPixelSizeMax = 32;

        piecewiseLinearActive = false;
        furrierTransformActive = false;

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

    public void AddValue(int value){
        
        if(quantity < sizeGenericMask*sizeGenericMask)
        {
            int line = quantity/sizeGenericMask;
            int column = (quantity+1) - ((sizeGenericMask * line) + 1);
            Debug.Log(column);
            genericMask[line,column] = value*multiplier;
            quantity += 1;

            Debug.Log(column);
            switch (line)
            {
                case 0:
                    genericTextsLine1[column].text = (value * multiplier).ToString();
                    break;

                case 1:
                    genericTextsLine2[column].text = (value * multiplier).ToString();
                    break;

                case 2:
                    genericTextsLine3[column].text = (value * multiplier).ToString();
                    break;

                case 3:
                    genericTextsLine4[column].text = (value * multiplier).ToString();
                    break;

                case 4:
                    genericTextsLine5[column].text = (value * multiplier).ToString();
                    break;

                case 5:
                    genericTextsLine6[column].text = (value * multiplier).ToString();
                    break;

                case 6:
                    genericTextsLine7[column].text = (value * multiplier).ToString();
                    break;

                case 7:
                    genericTextsLine8[column].text = (value * multiplier).ToString();
                    break;

                case 8:
                    genericTextsLine9[column].text = (value * multiplier).ToString();
                    break;

                default:
                    break;
            }
        }
        else
        {
        }

    }



    public void CallGauss(int size)
    {
        outputTexture = NonLinearEffects.GaussianFilter(effects[currentEffect], size);
        RenderOutput();
    }

    public void CallLaplace(int type)
    {
        outputTexture = NonLinearEffects.FilterLaplace(type, normalizeLaplace, effects[currentEffect]);
        RenderOutput();
    }

    public void CallMedian(int size)
    {
        outputTexture = NonLinearEffects.Median(effects[currentEffect], size);
        RenderOutput();
    }

    public void DefineSize(int size){
        sizeGenericMask = size;
        quantity = 0;

        InitTextDisplay();
    }

    public void InitLineText(List<Text> list)
    {
        foreach (Text t in list)
        {
            t.text = "0";
        }
    }

    public void InitTextDisplay()
    {

        InitLineText(genericTextsLine1);
        InitLineText(genericTextsLine2);
        InitLineText(genericTextsLine3);
        InitLineText(genericTextsLine4);
        InitLineText(genericTextsLine5);
        InitLineText(genericTextsLine6);
        InitLineText(genericTextsLine7);
        InitLineText(genericTextsLine8);
        InitLineText(genericTextsLine9);
        
        
    }

    public void RenderManager()
    {
        SelectEffect();

        RenderOutput();
    }
    private void RenderInput()
    {
        if(histogramPanelActive){
            Histogram.CalcHistogram(effects[currentEffect]);
            histogramRed = Histogram.ReturnHistogramRed();
            histogramBlue = Histogram.ReturnHistogramBlue();
            histogramGreen = Histogram.ReturnHistogramGreen();
            histogramRedOutput.sprite = Sprite.Create(histogramRed,new Rect(0, 0, histogramRed.width, histogramRed.height),
            Vector2.zero);
            histogramGreenOutput.sprite = Sprite.Create(histogramGreen,new Rect(0, 0, histogramGreen.width, histogramGreen.height),
            Vector2.zero);
            histogramBlueOutput.sprite = Sprite.Create(histogramBlue,new Rect(0, 0, histogramBlue.width, histogramBlue.height),
            Vector2.zero);
        }
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
        else if (laplacianActive)
        {
        }
        else if (thresholdActive)
        {
            outputTexture = LinearEffects.Threshold(renderTexture, rLevel,gLevel,bLevel, thresholdType, rgbLevel);
        }
        else if (blurActive)
        {
            outputTexture = NonLinearEffects.Blur(renderTexture, blurLevel);
        }
        else if (gammaActive)
        {
            outputTexture = LinearEffects.GammaCorrection(renderTexture, gammaRedLevel, gammaBlueLevel, gammaGreenLevel);
        }
        else if (greyScaleActive)
        {
            outputTexture = LinearEffects.GrayScale(renderTexture, greyScaleWeightRed, greyScaleWeightGreen, greyScaleWeightBlue);
        }
        else if(histogramActive && histogramPanelActive){
            if (applyHisto)
            {
                applyHisto = false;
                outputTexture = Histogram.EqualizeHistogram(renderTexture);
            }
        }
        else if (pixelizationActive)
        {
            outputTexture = LinearEffects.Pixelization(renderTexture, pixelizationPixelSize);
        }
        else if ( sobelActive )
        {
            outputTexture = NonLinearEffects.Sobel(renderTexture, sobelValue, normalizerValue);
        }
        else if ( genericActive )
        {
            float[, ] mask = new float[sizeGenericMask, sizeGenericMask];
            for(int i = 0; i< sizeGenericMask; i++){
                for(int j = 0; j < sizeGenericMask; j++){
                    mask[i,j] = (float)genericMask[i,j];                    
                }
            }
            outputTexture = NonLinearEffects.GenericFilter(mask,sizeGenericMask, renderTexture);
            quantity = 0;
        }
        else if ( colorFragmentationActive )
        {
            outputTexture = LinearEffects.ColorFragmentation(renderTexture,colorRLevel,colorGLevel,colorBLevel, limit, colorbase);
        }
        else if ( chromaKeyActive )
        {
            if(imgBase != null){
                outputTexture = LinearEffects.ChromaKey(renderTexture,chromaRLevel,chromaGLevel,chromaBLevel, chromaLimit, imgBase);
            }
        }
        else if(hsvActive){
            outputTexture = LinearEffects.ApplyHsv(renderTexture, h,s,v);
        }
        else if(scaleActive){
            if(scaleValue > 0){
                outputTexture = LinearTransformation.Scale(renderTexture,scaleValue, typeTransform);
            }
        }
        else if(rotationActive){
            outputTexture = LinearTransformation.Rotacao(renderTexture,angValue, typeTransformRot);
        }
        else if(transformationLogaritimicActive){
            outputTexture = LinearEffects.LogartimicaTransform(renderTexture,rCValue, bCValue, gCValue);
        }
        else if (piecewiseLinearActive)
        {
            outputTexture = LinearEffects.PiecewiseLinear(renderTexture);
        }
        else if (highBoostActive)
        {
            outputTexture = NonLinearEffects.HighBoost(renderTexture, coeficient, normalizeHighBoost, sizeHighBoost);
        }
        else if ( furrierTransformActive )
        {
            //outputTexture = NonLinearEffects.FourierTransform(renderTexture);
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
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
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 9: //Generic Filter
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = true;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 10: //Color Fragmentation
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = true;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 11: //Chroma key
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = true;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 12: //HSV
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = true;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 13: //Scale
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = true;
                    rotationActive= false;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 14: //Rotation
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= true;
                    transformationLogaritimicActive = false;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 15: //Logaritimic Transform
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive= false;
                    transformationLogaritimicActive = true;
                    piecewiseLinearActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;

                case 16: //Laplacian Filter
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive = false;
                    transformationLogaritimicActive = false;
                    laplacianActive = true;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 17: //Median Active
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive = false;
                    transformationLogaritimicActive = false;
                    laplacianActive = false;
                    medianActive = true;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;
                case 18: //Gauss Filter
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive = false;
                    transformationLogaritimicActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = true;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;

                case 19: //HighBoost
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive = false;
                    transformationLogaritimicActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = true;
                    furrierTransformActive = false;
                    break;

                case 20: //Linear por partes
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive = false;
                    transformationLogaritimicActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = true;
                    highBoostActive = false;
                    furrierTransformActive = false;
                    break;

                case 21: //Transformada de Furrier
                    negativeActive = false;
                    thresholdActive = false;
                    blurActive = false;
                    gammaActive = false;
                    greyScaleActive = false;
                    pixelizationActive = false;
                    histogramActive = false;
                    sobelActive = false;
                    genericActive = false;
                    colorFragmentationActive = false;
                    chromaKeyActive = false;
                    hsvActive = false;
                    scaleActive = false;
                    rotationActive = false;
                    transformationLogaritimicActive = false;
                    laplacianActive = false;
                    medianActive = false;
                    gaussActive = false;
                    piecewiseLinearActive = false;
                    highBoostActive = false;
                    furrierTransformActive = true;
                    break;

                default:
                    break;
            }
        }
    }
    public void WeightRgbmanager(){
        greyScaleWeightRedSlider.value = 1;
        greyScaleWeightGreenSlider.value = 1;
        greyScaleWeightBlueSlider.value = 1;
        PanelManager();
    }
    public void changeColorBase(int type){
        if(type == 0){
            colorbase.r = 0;
            colorbase.g = 0;
            colorbase.b = 0;
        }else if(type== 1){
            colorbase.r = 128;
            colorbase.g = 128;
            colorbase.b = 128;
        }else if(type == 2){
            colorbase.r = 255;
            colorbase.g = 255;
            colorbase.b = 255;
        }
        colorbase.a = 255;
    }

    public void SetValue(int valueSet){
        sobelValue = valueSet;
        RenderManager();
    }
    public void HideShowHistogram(){
        if(histogramPanelActive){
            histogramPanelActive = false;
        }
        else{
            histogramPanelActive = true;
        }
        histogramPanelOutput.SetActive(histogramPanelActive);
        RenderInput();
    }

    public void ApplyHistogram()
    {
        applyHisto = true;
        RenderManager();
    }

    public void ChengeToggleRGBLogaritim()
    {
        rgbLogaritmActive = toggleRGBActive.isOn;
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
        genericFilterPanel.SetActive(genericActive);
        colorFragmentationPanel.SetActive(colorFragmentationActive);
        chromaKeyPanel.SetActive(chromaKeyActive);
        hsvPanel.SetActive(hsvActive);
        scalePanel.SetActive(scaleActive);
        angPanel.SetActive(rotationActive);
        histogramPanelOutput.SetActive(histogramPanelActive);
        transformLogaritimicPanel.SetActive(transformationLogaritimicActive);
        piecewiseLinearPanel.SetActive(piecewiseLinearActive);
        laplacianPanel.SetActive(laplacianActive);
        medianPanel.SetActive(medianActive);
        gaussPanel.SetActive(gaussActive);
        highBoostPanel.SetActive(highBoostActive);
        furrierTransformPanel.SetActive(furrierTransformActive);

        if ( negativeActive )
        {   
        }
        else if( thresholdActive)
        {
            thresholdType = (int)thresholdTypeSlider.value;
            if(thresholdType == 1)
            {
                rgbLevel = rgbSliderLevel.value;

                rLevel = rgbLevel;
                gLevel = rgbLevel;
                bLevel = rgbLevel;

                rSliderLevel.value = rLevel;
                gSliderLevel.value = gLevel;
                bSliderLevel.value = bLevel;
            }
            else
            {
                rLevel = rSliderLevel.value;
                gLevel = gSliderLevel.value;
                bLevel = bSliderLevel.value;

                rgbLevel = (rLevel + gLevel + bLevel) / 3;

                rgbSliderLevel.value = rgbLevel;
            }
            rTextLevel.text = rLevel.ToString();
            gTextLevel.text = gLevel.ToString();
            bTextLevel.text = bLevel.ToString();
            rgbTextLevel.text = rgbLevel.ToString();
            thresholdTypeText.text = thresholdType.ToString();
        }
        else if( blurActive )
        {
            blurLevel = (int) (blurSliderLevel.value * blurMaxLevel);
            blurSliderLevel.value = (float) blurLevel/blurMaxLevel;
            blurTextLevel.text = blurLevel.ToString();

        }
        else if( gammaActive )
        {
            gammaRGBActive = gammaRGBActiveToggle.isOn;

            if (gammaRGBActive)
            {
                gammaRGBLevel = gammaRGBSliderLevel.value * gammaMaxLevel;

                gammaRedLevel = gammaRGBLevel;
                gammaGreenLevel = gammaRGBLevel;
                gammaBlueLevel = gammaRGBLevel;

                gammaRedSliderLevel.value = gammaRedLevel / gammaMaxLevel;
                gammaGreenSliderLevel.value = gammaGreenLevel / gammaMaxLevel;
                gammaBlueSliderLevel.value = gammaBlueLevel / gammaMaxLevel;
            }
            else
            {
                gammaRedLevel = gammaRedSliderLevel.value * gammaMaxLevel;
                gammaGreenLevel = gammaGreenSliderLevel.value * gammaMaxLevel;
                gammaBlueLevel = gammaBlueSliderLevel.value * gammaMaxLevel;

                gammaRGBLevel = (gammaRedLevel + gammaGreenLevel + gammaBlueLevel) / gammaMaxLevel;

                gammaRGBSliderLevel.value = gammaRGBLevel;

            }

            gammaRedTextLevel.text = gammaRedLevel.ToString();
            gammaGreenTextLevel.text = gammaGreenLevel.ToString();
            gammaBlueTextLevel.text = gammaBlueLevel.ToString();
            gammaRGBTextLevel.text = gammaRGBLevel.ToString();
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
        else if(genericActive){
            multiplier = (int)multiplierSlide.value;
            multiplierText.text = multiplier.ToString();
        }
        else if( colorFragmentationActive )
        {   
            colorRLevel = (int)rColorSliderLevel.value;
            rColorTextLevel.text = colorRLevel.ToString();
            colorGLevel = (int)gColorSliderLevel.value;
            gColorTextLevel.text = colorGLevel.ToString();
            colorBLevel = (int)bColorSliderLevel.value;
            bColorTextLevel.text = colorBLevel.ToString();
            limit = (int)limitSlider.value;
            limitTextLevel.text = limit.ToString();

        }else if(chromaKeyActive){
            chromaRLevel = (int)rChromaSliderLevel.value;
            rChromaTextLevel.text = chromaRLevel.ToString();
            chromaGLevel = (int)gChromaSliderLevel.value;
            gChromaTextLevel.text = chromaGLevel.ToString();
            chromaBLevel = (int)bChromaSliderLevel.value;
            bChromaTextLevel.text = chromaBLevel.ToString();
            chromaLimit = (int)chromaLimitSlider.value;
            chromaLimitTextLevel.text = chromaLimit.ToString();

        }else if(hsvActive){
            h = (int)hLevel.value;
            hText.text = h.ToString();
            s = Math.Round(sLevel.value, 2);
            sText.text = s.ToString();
            v = Math.Round(vLevel.value, 2);
            vText.text = v.ToString();
        }
        else if(scaleActive){
            scaleValue = scaleSlider.value;
            scaleValueText.text = scaleValue.ToString();
            typeTransform = (int)typeTransformSlider.value;
            typeTransformText.text = typeTransform.ToString();
        }
        else if(rotationActive){
            angValue = (int)angSlider.value;
            angValueText.text = angValue.ToString();
            typeTransformRot = (int)typeTransformRotSlider.value;
            typeTransformRotText.text = typeTransformRot.ToString();
        }
        else if(transformationLogaritimicActive){
            if(rgbLogaritmActive == false)
            {
                rCValue = (int)rCSlider.value;
                rCText.text = rCValue.ToString();
                bCValue = (int)bCSlider.value;
                bCText.text = bCValue.ToString();
                gCValue = (int)gCSlider.value;
                gCText.text = gCValue.ToString();

                //Aqui faz a média pra fazer o rgb
                rgbCSlider.value = (float) (rCValue+gCValue+bCValue) / 3;
            }
            else
            {
                rgbCValue = (int)rgbCSlider.value;
                rCValue = rgbCValue;
                gCValue = rgbCValue;
                bCValue = rgbCValue;

                rCSlider.value = (float) rgbCValue;
                gCSlider.value = (float) rgbCValue;
                bCSlider.value = (float) rgbCValue;
            }
            
            rgbCText.text = rgbCValue.ToString();
            rCText.text = rCValue.ToString();
            bCText.text = bCValue.ToString();
            gCText.text = gCValue.ToString();
        }
        if (piecewiseLinearActive)
        {

        }
        else if (laplacianActive)
        {
            normalizeLaplace = (int)normalizeLaplaceSlider.value;
        }
        else if (highBoostActive)
        {
            normalizeHighBoost = (int)normalizeHighBoostSlider.value;
            coeficient = coeficientSlider.value;
            coeficientText.text = coeficient.ToString();
            sizeHighBoost = (int)sizeHighBoostSlider.value;
            sizeHighBoostText.text = sizeHighBoost.ToString();
        }
        else if (furrierTransformActive)
        {

        }
    }
    public void GetImagem(){
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //Load image from local path with UWR
            StartCoroutine(LoadImage(path));
        });

        //Load Image from Local
        IEnumerator LoadImage(string path)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                }
                else
                {
                    inputTexture = DownloadHandlerTexture.GetContent(uwr);
                }
            }
            currentEffect = 0;
            effects[currentEffect] = inputTexture;
            RenderInput();
        }

    }
    public void SaveImage(){
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;

        new FileBrowser().SaveFileBrowser(bp,"teste",".png", path =>
        {
            Texture2D finalTexture = new Texture2D(effects[currentEffect].width, effects[currentEffect].height);
            Color[] cores = effects[currentEffect].GetPixels();
            finalTexture.SetPixels(cores);
            byte[] bytes = finalTexture.EncodeToJPG();
            File.WriteAllBytes(path, bytes);
        });
    }





    public void GetImagemChroma(){
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //Load image from local path with UWR
            StartCoroutine(LoadImageChroma(path));
        });

        //Load Image from Local
        IEnumerator LoadImageChroma(string path)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                }
                else
                {

                }
            } 
        }

    }
}

//image handle
