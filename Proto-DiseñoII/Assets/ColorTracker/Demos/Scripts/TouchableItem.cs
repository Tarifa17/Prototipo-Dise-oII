using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// 
/// </summary>
public class TouchableItem : MonoBehaviour, IPointerEnterHandler
{
    private Rigidbody body;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        // Buscar todos los objetos con este script
        var allItems = FindObjectsByType<TouchableItem>(FindObjectsSortMode.None);

        // Ignorar colisiones con los demás TouchableItem
        foreach (var item in allItems)
        {
            if (item != this)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), item.GetComponent<Collider>());
            }
        }
    }	

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Check that this object has a Rigidbody component attached.
        if (!body) return;
        // eventData.delta contains the TrackerResult.linearVelocity data. 
        // You can used to know the force direction.
        Vector2 reducedForce = eventData.delta * 0.17f;
        body.AddForce(reducedForce, ForceMode.Impulse);
    }

   
}
