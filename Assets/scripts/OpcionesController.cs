using UnityEngine;

public class OpcionesController : MonoBehaviour
{
    public GameObject panelOpciones;
    public GameObject panelCreditos;

    public void MostrarOpciones()
    {
        panelOpciones.SetActive(true);
    }

    public void OcultarOpciones()
    {
        panelOpciones.SetActive(false);
    }

    public void MostrarCreditos()
    {
        panelCreditos.SetActive(true);
    }

    public void OcultarCreditos()
    {
        panelCreditos.SetActive(false);
    }
}
