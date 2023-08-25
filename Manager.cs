using UnityEngine;
using UnityEngine.UI;

using Scripts.Effects;

public class Manager : MonoBehaviour
{
    [Header("Render Control")]
    public Texture2D inputTexture;
    public Texture2D outputTexture;
    public Image inputRender;
    public Image outputRender;

    [Header("Dropdown Control")]
    public Dropdown dropdown; 

    [Header("Negative Control")]
    public bool negativeActive;
    public float negativeLevel;
    public GameObject negativePanel;
    public Slider negativeSliderLevel;
    public Text negativeTextLevel;

    [Header("Threshold Control")]
    public bool thresholdActive;
    public float thresholdLevel;
    public GameObject thresholdPanel;
    public Slider thresholdSliderLevel;
    public Text thresholdTextLevel;

    [Header("Gama Correction Control")]
    public bool gamaCorrectionActive;
    public float gamaCorrectionLevel;
    public int gamaControler;
    public Slider gamaControlerSlider;
    public GameObject gamaCorrectionPanel;
    public Slider gamaCorrectionSlider;
    public Text gamaCorrectionTextLevel;
    public Text gamaCorrectionControlerTextLevel;

    [Header("Transformacao Logaritimica Control")]
    public bool transformationLogaritimicActive;
    public double transformationLogaritimicLevel;
    public GameObject transformationLogaritimicPanel;
    public Slider transformationLogaritimicSlider;
    public Text transformationLogaritimicTextLevel;

    private void Start()
    {
        negativeActive = false;
        thresholdActive = false;
        gamaCorrectionActive = false;
        transformationLogaritimicActive = false;

        if (inputRender != null)
        {
            inputRender.sprite = Sprite.Create(
                    inputTexture,
                    new Rect(0, 0, inputTexture.width, inputTexture.height),
                    Vector2.zero
            );

            outputRender.sprite = inputRender.sprite;

        }
    }

    private void Update()
    {
        DropdownManager();
        PanelManager();

        if(Input.GetMouseButtonUp(0))
        {
            RenderManager();
        }
    }
    public void CreateMap(Texture2D inputTexture) {
        Effects.ColorMap(inputTexture);
    }
    public void RenderManager()
    {
        if (inputTexture != null)
        {
            //Renderiza no painel de entrada
            if (inputRender != null)
            {
                inputRender.sprite = Sprite.Create(
                        inputTexture,
                        new Rect(0, 0, inputTexture.width, inputTexture.height),
                        Vector2.zero
                );
            }

            //Chama o efeitos da classe Effects
            SelectEffect();

            //Renderiza no painel de sa˙Åa
            if (outputRender != null)
            {
                outputRender.sprite = Sprite.Create(
                        outputTexture,
                        new Rect(0, 0, outputTexture.width, outputTexture.height),
                        Vector2.zero
                );
            }

        }
    }

    private void SelectEffect()
    {
        if (negativeActive)
        {
            outputTexture = Effects.Negative(inputTexture, negativeLevel);
        }
        else if (thresholdActive)
        {
            outputTexture = Effects.Threshold(inputTexture, thresholdLevel);
        }
        else if (gamaCorrectionActive)
        {
            outputTexture = Effects.CorrecaoGama(inputTexture, gamaCorrectionLevel, gamaControler);
        }
        else if (transformationLogaritimicActive)
        {
            outputTexture = Effects.TransformacaoLogaritimica(inputTexture, transformationLogaritimicLevel);
        }
        else
        {  
            outputTexture = inputTexture;
        }
    }

    //Seleciona painel.
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
                    gamaCorrectionActive = false;
                    transformationLogaritimicActive = false;
                    break;

                case 1: //Negativo
                    negativeActive = true;
                    thresholdActive = false;
                    gamaCorrectionActive = false;
                    transformationLogaritimicActive = false;
                    break;

                case 2: // Threshold
                    negativeActive = false;
                    thresholdActive = true;
                    gamaCorrectionActive = false;
                    transformationLogaritimicActive = false;
                    break;
                case 3: // CorrecaoGama
                    negativeActive = false;
                    thresholdActive = false;
                    gamaCorrectionActive = true;
                    transformationLogaritimicActive = false;
                    break;
                case 4: // TransformationLogaritimic
                    negativeActive = false;
                    thresholdActive = false;
                    gamaCorrectionActive = false;
                    transformationLogaritimicActive = true;
                    break;
                default:
                    break;
            }
        }
    }

    //Obtem os valores dos paineis.
    private void PanelManager()
    {
        //Altera visibilidade dos painÈis.
        negativePanel.SetActive(negativeActive);
        thresholdPanel.SetActive(thresholdActive);
        gamaCorrectionPanel.SetActive(gamaCorrectionActive);
        transformationLogaritimicPanel.SetActive(transformationLogaritimicActive);

        // Consome e retorna para interface os valores de acordo com o painel ativo.
        if ( negativeActive )
        {   //Exemplo
            negativeLevel = negativeSliderLevel.value; //Consome valor do slider.
            negativeTextLevel.text = negativeLevel.ToString(); // Altera texto ao lado do slider.
        }
        else if( thresholdActive)
        {
            thresholdLevel = thresholdSliderLevel.value;
            thresholdTextLevel.text = thresholdLevel.ToString();
        }
        else if (gamaCorrectionActive)
        {
            gamaControler = (int)gamaControlerSlider.value;
            gamaCorrectionControlerTextLevel.text = gamaControler.ToString();
            gamaCorrectionLevel = gamaCorrectionSlider.value;
            gamaCorrectionTextLevel.text = gamaCorrectionLevel.ToString();
        }
        else if (transformationLogaritimicActive)
        {
            transformationLogaritimicLevel = transformationLogaritimicSlider.value;
            transformationLogaritimicTextLevel.text = transformationLogaritimicLevel.ToString();
        }
    }

}
