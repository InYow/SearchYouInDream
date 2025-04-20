using System;
using Inventory.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory
{
    public class SlotItem : MonoBehaviour,IInventoryItem,
        IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
    {
        public GameObject descriptionPanel;
        public TextMeshProUGUI descriptionText;
        public Image itemImage;
        public TextMeshProUGUI countText;
        
        private Vector2 offset;
        private Transform originalParent;
        private Vector3 originalPosition;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        
        private CollectableDataScriptableObject collectableData;
    
        public CollectableDataScriptableObject CollectableData
        {
            get => collectableData;
            set
            {
                collectableData = value;
                if (collectableData != null)
                {
                    itemImage.sprite = collectableData.ItemIcon;
                    descriptionText.text = collectableData.ItemDescription;
                    countText.text = collectableData.ItemCount.ToString();
                    
                    descriptionText.gameObject.SetActive(true);
                    countText.gameObject.SetActive(true);
                }
                else
                {
                    itemImage.sprite = null;
                    descriptionText.gameObject.SetActive(false);
                    countText.gameObject.SetActive(false);
                }
            }
        }
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            
            offset = new Vector2(-rectTransform.rect.width/2,rectTransform.rect.height/2);
            
            descriptionPanel.SetActive(false);   
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("OnPointerEnter");
            if (!eventData.dragging && collectableData !=null)
            {
                descriptionPanel.SetActive(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("OnPointerExit");
            descriptionPanel.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (collectableData == null)
            {
                return;
            }
            
            //Debug.Log("OnBeginDrag");
            originalPosition = transform.position;
            originalParent = transform.parent;

            transform.SetParent(transform.parent.parent.parent);
            
            descriptionPanel.SetActive(false);
            canvasGroup.blocksRaycasts = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (collectableData == null)
            {
                return;
            }
            //Debug.Log("OnDrag");
            transform.position = eventData.position + offset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (collectableData == null)
            {
                return;
            }
            
            //Debug.Log("OnEndDrag");
            var hitGameObject = eventData.pointerCurrentRaycast.gameObject;
            
            if (hitGameObject != null)
            {
                var slotItem = hitGameObject.GetComponent<SlotItem>();
                if (slotItem != null)
                {
                    transform.SetParent(slotItem.transform.parent);
                    transform.position = slotItem.transform.position;
                    
                    slotItem.transform.SetParent(originalParent);
                    slotItem.transform.position = originalPosition;
                }
                else
                {
                    transform.SetParent(originalParent);
                    transform.position = originalPosition;
                }
            }
            else
            {
                transform.SetParent(originalParent);
                transform.position = originalPosition;
            }
            
            canvasGroup.blocksRaycasts = true;
        }
    }
}