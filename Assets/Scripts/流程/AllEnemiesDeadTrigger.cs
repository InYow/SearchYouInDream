using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllEnemiesDeadTrigger : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    public bool actived = false;

    public UnityEvent onAllEnemiesDead;

    [ContextMenu("GetEnemiesInChildren")]
    public void GetEnemiesInChildren()
    {
        enemies.Clear();
        // 获取所有子物体的Enemy组件并添加到列表中
        foreach (Transform child in transform)
        {
            Enemy enemy = child.GetComponent<Enemy>();
            if (enemy != null && enemy.gameObject.activeSelf)
            {
                enemies.Add(enemy);
            }
        }
    }

    //XXX 性能优化
    private void Update()
    {
        if (actived)
        {
            return;
        }

        bool b = true;
        // 遍历敌人列表，检查每个敌人的状态
        foreach (Enemy enemy in enemies)
        {
            if (enemy.health > 0)
            {
                b = false;
            }
        }

        if (b)
        {
            //触发事件
            onAllEnemiesDead?.Invoke();
            actived = true;
            Debug.Log("敌人全部死亡");
        }
    }
}
