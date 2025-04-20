using System.Collections.Generic;
using Derek.Scripts.UI.Panel;
using Inventory;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UI.UISystem.UIFramework;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : UICanvas
{

    public InventorySlot[] bagPackSlots;
    
    [ShowInInspector]public Dictionary<InventorySlot,int> bagSlotDataCache = new Dictionary<InventorySlot,int>();
    
    public override void OnCanvasEnter(UIManager manager)
    {
        base.OnCanvasEnter(manager);
        InitialBagSlotUI();
    }
    
    /// <summary>
    /// 根据数据初始化UI
    /// </summary>
    private void InitialBagSlotUI()
    {
        var inventoryData= InventoryManager.instance.GetInventoryData();
        int dataLength = inventoryData.Count;
        if (bagSlotDataCache.Count == 0)
        {
            for (int i=0; i<bagPackSlots.Length; ++i)
            {
                if (i < dataLength)
                {
                    bagPackSlots[i].SetSlotData(inventoryData[i].Value);
                }
                else
                {
                    bagPackSlots[i].SetSlotData(null);
                }
            }
        }
        else
        {
            List<InventorySlot> emptySlots = new List<InventorySlot>();
            foreach (var slot in bagPackSlots)
            {
                if (!bagSlotDataCache.ContainsKey(slot))
                {
                    emptySlots.Add(slot);
                    slot.SetSlotData(null);
                }
                else
                {
                    var data = inventoryData.Find(
                        (d) => d.Key == bagSlotDataCache[slot]);
                    slot.SetSlotData(data.Value);
                }
            }

            int index = 0;
            int emptySlotsCount = emptySlots.Count;
            foreach(var item in inventoryData)
            {
                if (!bagSlotDataCache.ContainsValue(item.Key))
                {
                    emptySlots[index].SetSlotData(item.Value);
                    index++;
                }
            }
        }
    }

    public override void OnCanvasExit(UIManager manager)
    {
        base.OnCanvasExit(manager);

        SaveBagSlotUI();
        //Unbind Event
    }

    private void SaveBagSlotUI()
    {
        bagSlotDataCache.Clear();
        for (int i=0; i<bagPackSlots.Length; ++i)
        {
            int id = bagPackSlots[i].GetSlotDataID();
            if (id > 0)
            {
                bagSlotDataCache.Add(bagPackSlots[i],id);
            }
        }
    }
}
