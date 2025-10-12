using UnityEngine;

public class LetterManager : MonoBehaviour
{
    [Header("Contenedores de letras")]
    public LetterContainer[] contenedores;

    [Header("Botón de Confirmar (objeto 3D o UI)")]
    public GameObject botonConfirmar;

    private void Start()
    {
        if (botonConfirmar != null)
            botonConfirmar.SetActive(false); // El botón empieza oculto
    }

    private void Update()
    {
        VerificarProgreso();
    }

    private void VerificarProgreso()
    {
        if (contenedores == null || contenedores.Length == 0)
            return;

        // Si todos los contenedores están llenos, mostrar el botón
        bool todasLlenas = true;

        foreach (var contenedor in contenedores)
        {
            if (!contenedor.IsFilled())
            {
                todasLlenas = false;
                break;
            }
        }

        // Activar/desactivar botón según resultado
        if (botonConfirmar != null && botonConfirmar.activeSelf != todasLlenas)
            botonConfirmar.SetActive(todasLlenas);
    }

    // Este método será llamado cuando se toque el botón con el puntero
    public void ConfirmarPalabra()
    {
        Debug.Log("✅ Palabra completada correctamente. ¡Felicitaciones!");
        // Acá podés cargar otra escena, mostrar animación, etc.
    }
}
