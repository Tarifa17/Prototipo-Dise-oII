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
        // Ordenar los objetos por su posición en el eje X (de izquierda a derecha)
        var ordered = items.OrderBy(i => i.transform.position.x).ToArray();

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

        // Si están en el orden correcto, comprobar que haya espacio entre ellos
        if (isOrdered)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                float distance = Vector3.Distance(items[i].transform.position, items[i + 1].transform.position);
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
