using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

public class PickupItem : MonoBehaviour
{
    [FormerlySerializedAs("collectableItem")] [SerializeField]private CollectableItemAbstract collectableItemAbstract;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (collectableItemAbstract != null)
            {
                collectableItemAbstract.PickUp();
                collectableItemAbstract = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collectable = other.GetComponent<CollectableItemAbstract>();
        if (collectable)
        {
            collectableItemAbstract = collectable;
        }
    }
}
