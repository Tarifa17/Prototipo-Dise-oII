using UnityEngine;
using System.Linq;

public class OrderManager : MonoBehaviour
{
    [Header("Asigna los cubos aquí (en el orden correcto)")]
    public TouchableItem[] items;

    [Header("Configuración de orden")]
    [Tooltip("Distancia mínima permitida entre cubos para considerarlos 'no tocándose'")]
    public float minDistance = 1.5f;

    [Header("Colores de estado")]
    public Color orderedColor = Color.green;
    public Color unorderedColor = Color.red;

    void Update()
    {
        CheckOrder();
    }

    void CheckOrder()
    {
        // Ordenar los objetos primero por X, luego por Y
        var ordered = items
            .OrderBy(i => i.transform.position.x)
            .ThenBy(i => i.transform.position.y)
            .ToArray();

        bool isOrdered = true;

        // Verificar si el orden actual coincide con el esperado
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != ordered[i])
            {
                isOrdered = false;
                break;
            }
        }

        // Si el orden es correcto, comprobar que haya espacio suficiente entre todos
        if (isOrdered)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                Vector2 posA = new Vector2(items[i].transform.position.x, items[i].transform.position.y);
                Vector2 posB = new Vector2(items[i + 1].transform.position.x, items[i + 1].transform.position.y);
                float distance = Vector2.Distance(posA, posB);

                if (distance < minDistance)
                {
                    isOrdered = false; // Están demasiado cerca
                    break;
                }
            }
        }

        // Aplicar color según el estado
        foreach (var item in items)
        {
            var renderer = item.GetComponent<Renderer>();
            renderer.material.color = isOrdered ? orderedColor : unorderedColor;
        }
    }
}
