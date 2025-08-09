using UnityEngine;

public class ColorSwitchController : MonoBehaviour
{
    [SerializeField] private ParticleSystem colorSplashPrefab;

    private Color[] colors = { new Color(0.9294118f, 0.1098039f, 0.1411765f), new Color(0.1333333f, 0.6941177f, 0.2980392f), new Color(0f, 0.6352941f, 0.9058824f) };

    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem colorSplashInstance;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        colorSplashInstance = Instantiate(colorSplashPrefab, transform.position, Quaternion.identity, transform);

        PlayerController.OnColorSwtiched += OnColorSwitched;

        var main = colorSplashInstance.main;
        switch (spriteRenderer.sprite.name)
        {
            case "ColorRed":
                main.startColor = colors[0];
                break;
            case "ColorGreen":
                main.startColor = colors[1];
                break;
            case "ColorBlue":
                main.startColor = colors[2];
                break;
        }
    }

    private void OnColorSwitched(Vector3 position, string color)
    {
        if (transform.position.Equals(position))
        {
            col.enabled = false;
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 0f;
            spriteRenderer.color = spriteColor;

            colorSplashInstance.Play();
        }
        else
        {
            col.enabled = true;
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 1f;
            spriteRenderer.color = spriteColor;
        }
    }

    private void OnDisable()
    {
        PlayerController.OnColorSwtiched -= OnColorSwitched;
    }
}
