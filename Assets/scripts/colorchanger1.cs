using UnityEngine;
using UnityEngine.UI;

public class colorchanger1 : MonoBehaviour
{
    public Renderer biciRenderer; // Referencia al modelo de la bicicleta
    public Button[] botonesColor; // Botones para seleccionar colores
    public Color[] coloresDisponibles; // Lista de colores disponibles

    void Start()
    {
        // Asegúrate de que cada botón llame a la función CambiarColor cuando se presione
        for (int i = 0; i < botonesColor.Length; i++)
        {
            int indice = i; // Necesario para evitar problemas de referencia en el loop
            botonesColor[i].onClick.AddListener(() => CambiarColor(indice));
        }
    }

    // Función que cambia el color del modelo de la bici
    public void CambiarColor(int indiceColor)
    {
        biciRenderer.material.color = coloresDisponibles[indiceColor];
        Debug.Log("Color cambiado a: " + coloresDisponibles[indiceColor]);
    }
}
