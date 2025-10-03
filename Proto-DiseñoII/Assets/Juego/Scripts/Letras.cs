using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Letras : MonoBehaviour
{
    private bool isGrabbed = false;       // indica si la letra está agarrada
    private Transform cursorTransform;    // referencia al cursor (tracker)

    private Rigidbody2D rb;
    [HideInInspector] public bool inSlot = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.tag = "Letra";
    }

    void Update()
    {
        if (isGrabbed && cursorTransform != null)
        {
            // La letra sigue la posición del cursor
            rb.MovePosition(cursorTransform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cuando el cursor toca la letra → la agarra
        if (other.CompareTag("Cursor"))   // poné al cursor el tag "Cursor"
        {
            isGrabbed = true;
            cursorTransform = other.transform;
            rb.isKinematic = true; // desactivamos físicas mientras la arrastramos
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Cuando el cursor se aleja → suelta la letra
        if (other.CompareTag("Cursor"))
        {
            isGrabbed = false;
            cursorTransform = null;
            rb.isKinematic = false; // vuelve a usar físicas normales
        }
    }
}
