using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar nuevas escenas

public class ButtonManager : MonoBehaviour
{
    public void OnJugarButtonClicked() //nombres para el script "jugar" todo lo demas es igual
    {
        Debug.Log("Boton Jugar presionado");
    }

    public void OnOpcionesButtonClicked()
    {
        Debug.Log("Bton Opciones presionado");
        // Aquí puedes cargar otra escena si es necesario
    }

    public void OnGarajeButtonClicked()
    {
        Debug.Log("Boton Garaje presionado");
        SceneManager.LoadScene("Escena_Garaje");
    }

    public void OnJugadorButtonClicked()
    {
        Debug.Log("Boton Jugador Presionado");
        // Aquí puedes cargar otra escena si es necesario
    }

    /*public void OnQuitButtonClicked()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }*/
}