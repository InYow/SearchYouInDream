using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// 场景中的可放入背包的物体
    /// </summary>
    public abstract class CollectableItem : MonoBehaviour
    {
        [SerializeField]private CollectableDataScriptableObject collectableData;

        public void PickUp()
        {
            InventoryManager.instance.AddItemToInventory(collectableData);
            Invoke(nameof(DelayDestroy),0.5f);
        }

        private void DelayDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}
