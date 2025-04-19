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
            //InventoryManager.ins
        }
    }
}
