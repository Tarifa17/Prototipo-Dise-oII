using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TrackerCursor : MonoBehaviour
{
    private void Awake()
    {
        // El Rigidbody debe ser kinematic para que el tracker lo mueva sin físicas
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        // Asegurarse que el collider sea trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        // Tag opcional para identificarlo en otros scripts
        gameObject.tag = "Cursor";
    }

    // Si querés debug visual en consola
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Cursor tocó: " + other.name);
    }
}
