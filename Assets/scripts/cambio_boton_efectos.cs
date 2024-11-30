using UnityEngine;
using UnityEngine.UI;

public class cambio_boton_efectos : MonoBehaviour
{
    public Sprite EffectsOnSprite; // La imagen cuando la música está encendida
    public Sprite EffectsOffSprite; // La imagen cuando la música está apagada
    private bool isEffectsOn = true; // Estado inicial, suponemos que la música empieza encendida
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleMusic);
    }

    void ToggleMusic()
    {
        if (isEffectsOn)
        {
            // Cambiar la imagen a la del botón apagado
            button.image.sprite = EffectsOffSprite;
            // Aquí también puedes poner el código para apagar la música
        }
        else
        {
            // Cambiar la imagen a la del botón encendido
            button.image.sprite = EffectsOnSprite;
            // Aquí puedes poner el código para encender la música
        }

        // Cambia el estado de la música
        isEffectsOn = !isEffectsOn;
    }
}
