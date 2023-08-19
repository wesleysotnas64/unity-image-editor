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

    private void Start()
    {
        negativeActive = false;
        thresholdActive = false;

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

            //Renderiza no painel de saída
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
                    break;

                case 1: //Negativo
                    negativeActive = true;
                    thresholdActive = false;
                    break;

                case 2: // Threshold
                    negativeActive = false;
                    thresholdActive = true;
                    break;

                default:
                    break;
            }
        }
    }

    //Obtem os valores dos paineis.
    private void PanelManager()
    {
        //Altera visibilidade dos painéis.
        negativePanel.SetActive(negativeActive);
        thresholdPanel.SetActive(thresholdActive);
        
        // Consome e retorna para interface os valores de acordo com o painel ativo.
        if( negativeActive )
        {   //Exemplo
            negativeLevel = negativeSliderLevel.value; //Consome valor do slider.
            negativeTextLevel.text = negativeLevel.ToString(); // Altera texto ao lado do slider.
        }
        else if( thresholdActive)
        {
            thresholdLevel = thresholdSliderLevel.value;
            thresholdTextLevel.text = thresholdLevel.ToString();
        }
    }

}
