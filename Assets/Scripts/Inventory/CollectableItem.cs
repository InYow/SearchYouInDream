using UnityEngine;

namespace Inventory
{
    public class CollectableItem : CollectableItemAbstract
    {
        public override void PickUp()
        {
            if (InventoryManager.instance.CanAddItem())
            {
                InventoryManager.instance.AddItemToInventory(collectableData);
                Invoke(nameof(DelayDestroy),0.5f);
            }
            else
            {
                Debug.LogError("You do not have enough resources to pick up");
            }
        }

        protected override void DelayDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}