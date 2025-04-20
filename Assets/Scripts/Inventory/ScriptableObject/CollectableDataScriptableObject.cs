using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Inventory
{
    [CreateAssetMenu(fileName = "CollectableDataSO",menuName = "Inventory/CollectableData")]
    public class CollectableDataScriptableObject : ScriptableObject
    {
        [SerializeField]private int itemID;
        [SerializeField]private string itemName;
        [SerializeField]private Sprite itemIcon; 
        [SerializeField]private string description;
        [SerializeField]private int cost;
        [SerializeField]private int itemCount;
        public int ItemID => itemID;
        public string ItemName => itemName;
        public string ItemDescription => description;
        public Sprite ItemIcon => itemIcon;
        public int ItemCost => cost;
        public int ItemCount => itemCount;

        public void AddSameItemCount(int count = 1)
        {
            itemCount += count;
        }
        public void SubtractSameItemCount(int count = 1)
        {
            itemCount -= count;
        }
    }
}