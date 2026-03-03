using UnityEngine;
using UnityEngine.U2D;      // SpriteShapeRenderer
using UnityEngine.UI;       // UI Image & Button

public class colorscipt : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteShapeRenderer spriteShapeRenderer;

    public Image uiImage;
    public Button uiButton;

    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;

    void Start()
    {
        ApplyColor();
    }

    void Update()
    {
        ApplyColor();
    }

    void ApplyColor()
    {
        Color selectedColor = color1;

        switch (colormanager.Instance.color)
        {
            case 1:
                selectedColor = color1;
                break;
            case 2:
                selectedColor = color2;
                break;
            case 3:
                selectedColor = color3;
                break;
            case 4:
                selectedColor = color4;
                break;
        }

        // SpriteRenderer
        if (spriteRenderer != null)
            spriteRenderer.color = selectedColor;

        // SpriteShapeRenderer
        if (spriteShapeRenderer != null)
            spriteShapeRenderer.color = selectedColor;

        // UI Image
        if (uiImage != null)
            uiImage.color = selectedColor;

        // UI Button (changes the button's main graphic color)
        if (uiButton != null && uiButton.targetGraphic != null)
            uiButton.targetGraphic.color = selectedColor;
    }
}