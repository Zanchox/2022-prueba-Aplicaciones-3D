using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float laneDistance = 4f; // Distancia entre cada carril
    public float laneSwitchSpeed = 50f; // Velocidad del cambio de carril (aumenta el valor para saltar más rápido)
    private int currentLane = 1; // Carril actual (0: izquierda, 1: medio, 2: derecha)
    private Vector3 targetPosition; // Posición objetivo cuando se mueve al siguiente carril
    private bool isSlowingDown = false;

    public bool isShaking = false; // Estado del jugador (temblando)
    private Coroutine shakeCoroutine;
    private float shakeTime = 2.0f; // Tiempo que dura el temblor

    void Start()
    {
        // Posición inicial en el carril central
        targetPosition = transform.position;
    }

    void Update()
    {
        // Movimiento entre carriles con las teclas A y D
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
            SetTargetPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
        {
            currentLane++;
            SetTargetPosition();
        }

        // Mover al ciclista de manera rápida a la nueva posición
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneSwitchSpeed);

        // Frenar (ralentizar el entorno)
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isSlowingDown)
            {
                Time.timeScale = 0.5f; // Ralentizar el tiempo
                isSlowingDown = true;
            }
        }
        else
        {
            if (isSlowingDown)
            {
                Time.timeScale = 1f; // Restablecer velocidad normal
                isSlowingDown = false;
            }
        }
    }

    void SetTargetPosition()
    {
        // Cambiar la posición objetivo para el siguiente carril
        targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
    }

    // Método para comenzar a temblar
    public void StartShaking(float shakeDuration)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine); // Si ya está temblando, detener el temblor actual
        }
        shakeCoroutine = StartCoroutine(Shake(shakeDuration)); // Iniciar el temblor
    }

    // Corrutina para gestionar el temblor del jugador
    private IEnumerator Shake(float shakeDuration)
    {
        isShaking = true;
        Debug.Log("El jugador está temblando");
        yield return new WaitForSeconds(shakeDuration);
        isShaking = false;
        Debug.Log("El jugador deja de temblar");
    }

    // Método que se llama cuando el jugador pierde
    public void LoseGame()
    {
        Debug.Log("Perdiste el juego");
        // Aquí puedes agregar lógica para manejar la pérdida, como reiniciar el nivel, mostrar un mensaje, etc.
    }

    // Manejo de colisiones
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision();
        }
        else if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject);
        }
    }

    // Manejo de colisión con obstáculo
    void HandleObstacleCollision()
    {
        if (isShaking)
        {
            // El jugador golpea por segunda vez mientras tiembla
            Debug.Log("Jugador se cae y pierde");
            LoseGame(); // Llama al método que maneje la pérdida del jugador
        }
        else
        {
            // El jugador tiembla al golpear el obstáculo
            Debug.Log("Jugador está temblando");
            StartShaking(shakeTime); // Llama al método para que el jugador empiece a temblar
        }
    }

    // Manejo de la recolección de monedas
    void CollectCoin(GameObject coin)
    {
        coin.SetActive(false); // Desactivar la moneda
        GameHUDView hudView = FindObjectOfType<GameHUDView>();
        hudView.AddCoin(); // Llama al método AddCoin en GameHUDView
    }
}
