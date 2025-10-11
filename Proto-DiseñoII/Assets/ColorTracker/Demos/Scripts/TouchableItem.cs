using UnityEngine;

public class TouchableItem : MonoBehaviour
{
    void Start()
    {
        var allItems = FindObjectsByType<TouchableItem>(FindObjectsSortMode.None);
        foreach (var item in allItems)
        {
            if (item != this)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), item.GetComponent<Collider>());
            }
        }
    }
}
