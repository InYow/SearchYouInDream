using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// 场景中的可放入背包的物体
    /// </summary>
    public abstract class CollectableItemAbstract : MonoBehaviour
    {
        [SerializeField]protected CollectableDataScriptableObject collectableData;

        public abstract void PickUp();

        protected abstract void DelayDestroy();
    }
}
