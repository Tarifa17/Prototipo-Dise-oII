using UnityEngine;

public class Slots : MonoBehaviour
{
    [Tooltip("La letra correcta que debe ir en este slot")]
    public string expectedLetter;

    [HideInInspector] public bool isOccupied = false; // para no permitir dos letras

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOccupied && other.CompareTag("Letra"))
        {
            // Marcar que este slot ahora tiene algo
            isOccupied = true;

            // Alinear la letra al centro del slot
            other.transform.position = transform.position;

            // Decirle a la letra que está en slot correcto
            LetterDrag letter = other.GetComponent<LetterDrag>();
            if (letter != null)
                letter.inSlot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Letra"))
        {
            isOccupied = false;

            LetterDrag letter = other.GetComponent<LetterDrag>();
            if (letter != null)
                letter.inSlot = false;
        }
    }
}
