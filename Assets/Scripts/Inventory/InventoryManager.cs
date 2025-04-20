using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        #region Singleton
        private static InventoryManager _instance;
        public static InventoryManager instance => _instance;
        #endregion
        //记录物品数量；
        private Dictionary<int,CollectableDataScriptableObject> inventory = new Dictionary<int,CollectableDataScriptableObject>();
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(_instance.gameObject);
            }
            _instance = this;
            
        }

        private void Start()
        {
            inventory.Clear();
        }

        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="itemData"></param>
        public void AddItemToInventory(CollectableDataScriptableObject itemData)
        {
            int id = itemData.ItemID;
            if (inventory.ContainsKey(id))
            {
                inventory[id].AddSameItemCount();
            }
            else
            {
                inventory.Add(id,itemData);
            }
        }

        /// <summary>
        /// 移除背包物品
        /// </summary>
        /// <param name="id">物品id</param>
        /// <returns></returns>
        public bool RemoveItemFromInventory(int id)
        {
            if (inventory.ContainsKey(id))
            {
                inventory[id].SubtractSameItemCount();
                if (inventory[id].ItemCount == 0)
                {
                    inventory.Remove(id);
                }
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 移除背包物品
        /// </summary>
        /// <param name="itemData">物品SO</param>
        /// <returns></returns>
        public bool RemoveItemFromInventory(CollectableDataScriptableObject itemData)
        {
            if (inventory.ContainsValue(itemData))
            {
                inventory[itemData.ItemID].SubtractSameItemCount();
                if (inventory[itemData.ItemID].ItemCount == 0)
                {
                    inventory.Remove(itemData.ItemID);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取背包数据
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<int,CollectableDataScriptableObject>[] GetInventoryData()
        {
            return inventory.ToArray();
        }
        
    }
}
