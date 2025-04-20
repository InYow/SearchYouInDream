using Inventory.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour,IInventorySlot
    {
        public int GetSlotDataID()
        {
            var slotItem = GetComponentInChildren<SlotItem>();
            if (slotItem != null)
            {
                return slotItem.CollectableData == null ? 0 : slotItem.CollectableData.ItemID;
            }

            return 0;
        }
        
        public void SetSlotData(CollectableDataScriptableObject collectableData)
        {
            var slotItem = GetComponentInChildren<SlotItem>();
            if (slotItem != null)
            {
                slotItem.CollectableData = collectableData;
            }
        }
        
    }
}
