using UnityEngine;
using UnityEngine.UI;

public class cambio_boton_musica : MonoBehaviour
{
    public Sprite musicOnSprite; // La imagen cuando la música está encendida
    public Sprite musicOffSprite; // La imagen cuando la música está apagada
    private bool isMusicOn = true; // Estado inicial, suponemos que la música empieza encendida
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleMusic);
    }

    void ToggleMusic()
    {
        if (isMusicOn)
        {
            // Cambiar la imagen a la del botón apagado
            button.image.sprite = musicOffSprite;
            // Aquí también puedes poner el código para apagar la música
        }
        else
        {
            // Cambiar la imagen a la del botón encendido
            button.image.sprite = musicOnSprite;
            // Aquí puedes poner el código para encender la música
        }

        // Cambia el estado de la música
        isMusicOn = !isMusicOn;
    }
}
