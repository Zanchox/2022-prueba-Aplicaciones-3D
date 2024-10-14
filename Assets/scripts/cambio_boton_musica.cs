using UnityEngine;
using UnityEngine.UI;

public class cambio_boton_musica : MonoBehaviour
{
    public Sprite musicOnSprite; // La imagen cuando la m�sica est� encendida
    public Sprite musicOffSprite; // La imagen cuando la m�sica est� apagada
    private bool isMusicOn = true; // Estado inicial, suponemos que la m�sica empieza encendida
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
            // Cambiar la imagen a la del bot�n apagado
            button.image.sprite = musicOffSprite;
            // Aqu� tambi�n puedes poner el c�digo para apagar la m�sica
        }
        else
        {
            // Cambiar la imagen a la del bot�n encendido
            button.image.sprite = musicOnSprite;
            // Aqu� puedes poner el c�digo para encender la m�sica
        }

        // Cambia el estado de la m�sica
        isMusicOn = !isMusicOn;
    }
}
