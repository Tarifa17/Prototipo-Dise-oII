using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LetterDrag : MonoBehaviour
{
    [HideInInspector] public bool inSlot = false;

    private void Awake()
    {
        gameObject.tag = "Letra"; // Para que los slots la detecten
    }
}
