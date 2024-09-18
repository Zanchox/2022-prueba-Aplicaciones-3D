using UnityEngine;
using UnityEngine.UI;

public class colorchanger2 : MonoBehaviour
{
    public Renderer cascoRenderer; // Referencia al modelo de la bicicleta
    public Button[] botonesColor2; // Botones para seleccionar colores
    public Color[] coloresDisponibles2; // Lista de colores disponibles

    void Start()
    {
        // Asegúrate de que cada botón llame a la función CambiarColor cuando se presione
        for (int i = 0; i < botonesColor2.Length; i++)
        {
            int indice = i; // Necesario para evitar problemas de referencia en el loop
            botonesColor2[i].onClick.AddListener(() => CambiarColor2(indice));
        }
    }

    // Función que cambia el color del modelo de la bici
    public void CambiarColor2(int indiceColor)
    {
        cascoRenderer.material.color = coloresDisponibles2[indiceColor];
        Debug.Log("Color cambiado a: " + coloresDisponibles2[indiceColor]);
    }
}
