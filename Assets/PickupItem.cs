using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField]private CollectableItem collectableItem;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (collectableItem != null)
            {
                collectableItem.PickUp();
                collectableItem = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collectable = other.GetComponent<CollectableItem>();
        if (collectable)
        {
            collectableItem = collectable;
        }
    }
}
