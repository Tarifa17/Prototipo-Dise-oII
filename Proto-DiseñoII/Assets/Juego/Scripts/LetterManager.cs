using UnityEngine;

public class LetterContainer : MonoBehaviour
{
    [Header("Letra que debe ir en este contenedor (ejemplo: 'A')")]
    public string correctLetter;

    [Header("Material cuando la letra correcta entra")]
    public Material correctMaterial;

    [Header("Renderer del contenedor")]
    public Renderer contenedorRenderer;

    private bool filled = false; // Para evitar múltiples detecciones

    private void Start()
    {
        if (contenedorRenderer == null)
            contenedorRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si ya tiene letra colocada, no hacer nada
        if (filled) return;

        // Verifica si el objeto que entra tiene componente TouchableLetter
        TouchableLetter letra = other.GetComponent<TouchableLetter>();

        if (letra != null)
        {
            // Comparamos la letra del objeto con la letra esperada
            if (letra.letterName == correctLetter)
            {
                Debug.Log("✅ Letra correcta: " + letra.letterName);

                // Cambiar material del contenedor
                if (correctMaterial != null && contenedorRenderer != null)
                    contenedorRenderer.material = correctMaterial;

                // Destruir la letra
                Destroy(other.gameObject);

                filled = true; // Marcar como lleno
            }
            else
            {
                Debug.Log("❌ Incorrecto -10 puntos (" + letra.letterName + ")");
            }
        }
    }
}
