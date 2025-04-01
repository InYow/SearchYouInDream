using UnityEngine;
using UnityEngine.UI;

public class CustomProcessBar : MonoBehaviour
{
    // UI图片
    [SerializeField]private Image backgroundImage;  // 背景图片
    [SerializeField]private Image midImage;         // 中景图片（表示当前血量）
    [SerializeField]private Image foregroundImage;  // 前景图片（表示将要扣除的血量）

    // 当前血量与最大血量
    private float currentHealth;
    private float maxHealth;

    // 将要扣除的血量
    private float pendingDamage;

    // 预扣除血量的UI响应事件
    public void OnPendingDamageChange(float newValue)
    {
        Debug.Log("Pending damage: " + newValue);
        pendingDamage = newValue;
        UpdateHealthUI();
    }
    
    // 现有血量变化的UI响应事件
    public void OnRealHealthChange(float newValue)
    {
        currentHealth = Mathf.Clamp(newValue,0f,maxHealth);
        pendingDamage = 0;
        UpdateHealthUI();
    }
    
    // 最大血量变化的UI响应事件
    public void OnMaxHealthChange(float newValue)
    {
        maxHealth = newValue;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        // 更新背景图片（总血量）
        backgroundImage.fillAmount = 1f;

        // 更新中景图片（真实血量）
        midImage.fillAmount = currentHealth / maxHealth;

        // 更新前景图片（将要扣除/增加的血量）
        foregroundImage.fillAmount = (currentHealth + pendingDamage) / maxHealth;
    }
    
    // // 撤回将要扣除的血量显示
    // public void CancelPendingDamage()
    // {
    //     pendingDamage = 0f;
    //     UpdateHealthUI();
    // }
    //
    // // 设置真实扣除的血量
    // public void SetRealDamage(float damage)
    // {
    //     currentHealth -= damage;
    //     if (currentHealth < 0f) currentHealth = 0f;
    //     UpdateHealthUI();
    // }

    // 更新血量UI显示
}