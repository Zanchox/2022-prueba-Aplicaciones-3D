using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar y cerrar el juego
using TMPro; // Para los textos del UI

public class GameController : MonoBehaviour
{
    public GameObject gameOverUI; // Referencia al UI de Fin de Juego
    public GameObject pauseUI; // Referencia al UI de Pausa
    public GameHUDView gameHUDView; // Referencia al HUD para acceder a las monedas y distancia
    public PlayerController playerController; // Referencia al PlayerController

    private bool isPaused = false;

    void Update()
    {
        // Detectar si el jugador ha presionado la tecla "escape" para pausar o despausar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    // Llamar este m�todo cuando el jugador pierda
    public void OnPlayerDeath()
    {
        Time.timeScale = 0f; // Detener el tiempo del juego
        gameOverUI.SetActive(true); // Mostrar la pantalla de Fin de Juego

        // Actualizar la UI con la distancia recorrida y monedas obtenidas
        gameOverUI.transform.Find("DistanceText").GetComponent<TextMeshProUGUI>().text = "Distancia: " + Mathf.Floor(gameHUDView.GetDistanceTravelled()) + "m";
        gameOverUI.transform.Find("CoinsText").GetComponent<TextMeshProUGUI>().text = "Monedas: " + gameHUDView.GetCoinsCollected();
    }

    // M�todo para reiniciar la partida
    public void RestartGame()
    {
        Time.timeScale = 1f; // Restaurar la velocidad del tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar la escena actual
    }

    // M�todo para salir al men� principal (por ahora, mostrar mensaje en consola)
    public void GoToMainMenu()
    {
        Debug.Log("Se envi� al men� principal.");
        // Aqu� podr�as cargar la escena del men� principal usando:
        SceneManager.LoadScene("Pantalla_Principal");
    }

    // M�todo para cerrar el juego
    public void QuitGame()
    {
        Debug.Log("Cerrando el juego.");
        Application.Quit();
    }

    // Pausar el juego
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Detener el tiempo
        pauseUI.SetActive(true); // Mostrar la UI de pausa
    }

    // Reanudar el juego
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Restaurar el tiempo
        pauseUI.SetActive(false); // Ocultar la UI de pausa
    }
}
