using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{
    public bool actived = false;

    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (actived)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            //触发事件
            onTriggerEnter?.Invoke();

            actived = true;
            Debug.Log("触发器被激活");
        }
    }
}
