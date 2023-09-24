using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image img;

    private Color defaultColor;
    private Color onHoverColor;

    private bool isHover;

    void Start()
    {
        defaultColor = new Color(1,1,1);
        onHoverColor = new Color(0,1,0);
        img = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;
        img.color = Color.cyan;
    }

    void Update()
    {
        if(IsHover)
        {
            if (Input.GetMouseButton(0))
            {
                MouseDrag();
            }
        }
    }

    private void MouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        transform.position = mousePosition;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = onHoverColor;
        IsHover = true;
        if(img != null)
        {
            img.color = onHoverColor;
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = defaultColor;
        IsHover = false;
    }

    public bool IsHover
    {
        get { return isHover; }
        private set { this.isHover = value; }
    }
}
