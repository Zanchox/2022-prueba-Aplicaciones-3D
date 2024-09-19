using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float laneDistance = 4f; // Distancia entre cada carril
    public float laneSwitchSpeed = 50f; // Velocidad del cambio de carril
    private int currentLane = 1; // Carril actual (0: izquierda, 1: medio, 2: derecha)
    private int previousLane = 1; // Carril anterior al choque
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
            previousLane = currentLane;
            currentLane--;
            SetTargetPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
        {
            previousLane = currentLane;
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

    // Método para volver al carril anterior
    public void ResetToLane()
    {
        currentLane = previousLane; // Vuelve al carril anterior
        SetTargetPosition(); // Ajustar la posición del jugador
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
            HandleObstacleCollision(other);
        }
        else if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject); // Recolección de monedas
        }
    }

    // Manejo de colisión con obstáculo
    void HandleObstacleCollision(Collider obstacle)
    {
        Vector3 obstaclePosition = obstacle.transform.position;
        Vector3 playerPosition = transform.position;

        // Si el jugador se choca de frente
        if (Mathf.Abs(obstaclePosition.x - playerPosition.x) < 0.1f)
        {
            LoseGame(); // Pierde si se choca de frente
        }
        else
        {
            StartShaking(shakeTime); // Si es un choque lateral, el jugador tiembla
            ResetToLane(); // Regresa al carril anterior
        }
    }

    // Manejo de la recolección de monedas
    void CollectCoin(GameObject coin)
    {
        coin.SetActive(false); // Desactivar la moneda
        ObjectsController objectsController = FindObjectOfType<ObjectsController>();
        objectsController.AddCoin(); // Llama al método AddCoin en ObjectsController para sumar las monedas
        StartCoroutine(objectsController.RespawnCoin(coin)); // Reaparecer la moneda después de 3 segundos
    }
}
