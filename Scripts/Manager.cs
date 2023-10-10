using UnityEngine;
using UnityEngine.UI;
using Scripts.Effects;
using Scripts.Utils;
using System.Collections.Generic;
using UnityEngine.Windows;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Networking;
using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;


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

    [Header("Generic FIlter Control")]
    public bool genericActive;
    public GameObject genericFilterPanel;
    public int[,] genericMask = new int[10,10];
    int quantity = 0;
    int sizeGenericMask = 3;
    public int multiplier;
    public Slider multiplierSlide;
    public Text multiplierText;

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
    public float s;
    public float v;

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

    public void AddValue(int value){
        if(quantity < sizeGenericMask*sizeGenericMask){
            int line = quantity/sizeGenericMask;
            int column = quantity / sizeGenericMask;
            genericMask[line,column] = value*multiplier;
            quantity += 1;
        }else{
            Debug.Log("Num vo aguentar mais...tira...tira caraio");
        }
    }

    public void DefineSize(int size){
        sizeGenericMask = size;
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
        else if (thresholdActive)
        {
            outputTexture = LinearEffects.Threshold(renderTexture, rLevel,gLevel,bLevel);
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
        else if(histogramActive){
            outputTexture = Histogram.EqualizeHistogram(renderTexture);
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
                    break;
                default:
                    break;
            }
        }
    }
    public void RgbManager(){
        rSliderLevel.value = rgbSliderLevel.value;
        gSliderLevel.value = rgbSliderLevel.value;
        bSliderLevel.value = rgbSliderLevel.value;
        rgbTextLevel.text = rgbSliderLevel.value.ToString();
        PanelManager();
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
        if( negativeActive )
        {   
        }
        else if( thresholdActive)
        {
            rLevel = rSliderLevel.value;
            rTextLevel.text = rLevel.ToString();
            gLevel = gSliderLevel.value;
            gTextLevel.text = gLevel.ToString();
            bLevel = bSliderLevel.value;
            bTextLevel.text = bLevel.ToString();
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
            s = sLevel.value;
            sText.text = s.ToString();
            v = vLevel.value;
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
                    Debug.Log(uwr.error);
                }
                else
                {
                    inputTexture = DownloadHandlerTexture.GetContent(uwr);
                    //placeholderImage is an Image Component of Unity UI
                }
            }
            currentEffect = 0;
            effects[currentEffect] = inputTexture;
            RenderInput();
        }

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
                    Debug.Log(uwr.error);
                }
                else
                {
                    imgBase = DownloadHandlerTexture.GetContent(uwr);
                    //placeholderImage is an Image Component of Unity UI
                }
            }
            inputKey.sprite = Sprite.Create(
                imgBase,
                new Rect(0, 0, imgBase.width, imgBase.height),
                Vector2.zero
            );
            
        }

    }
}

//image handle
