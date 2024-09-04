using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float laneDistance = 3f; // Distancia entre cada carril
    public float laneSwitchSpeed = 50f; // Velocidad del cambio de carril (aumenta el valor para saltar más rápido)
    private int currentLane = 1; // Carril actual (0: izquierda, 1: medio, 2: derecha)
    private Vector3 targetPosition; // Posición objetivo cuando se mueve al siguiente carril
    private bool isSlowingDown = false;

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
}
