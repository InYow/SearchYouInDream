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
        
        public int ItemID => itemID;
        public string ItemName => itemName;
        public string ItemDescription => description;
        public Sprite ItemIcon => itemIcon;
        public int ItemCost => cost;
    }
}