using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    [Header("Referencia al manager de contenedores")]
    public LetterManager letterManager;

    [Header("Tiempo necesario para confirmar (segundos)")]
    public float hoverTime = 1.2f;

    private float hoverTimer = 0f;
    private bool isHovering = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cursor"))
        {
            isHovering = true;
            hoverTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cursor"))
        {
            isHovering = false;
            hoverTimer = 0f;
        }
    }

    private void Update()
    {
        if (isHovering)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= hoverTime)
            {
                Confirmar();
                isHovering = false;
            }
        }
    }

    private void Confirmar()
    {
        if (letterManager != null)
        {
            letterManager.ConfirmarPalabra();
            Debug.Log("🖐 Botón confirmado con puntero");
        }
        else
        {
            Debug.LogWarning("⚠️ No hay LetterManager asignado al botón.");
        }
    }
}
