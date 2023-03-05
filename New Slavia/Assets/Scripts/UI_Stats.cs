using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Stats : MonoBehaviour
{
    private Inventory inventory;
    private Transform statsContainer;
    private TextMeshProUGUI moveSpeedText, damageText, attackSpeedText, bulletSpeedText, rangeText, bulletSizeText, maxHealthText;

    public void Start() {
        statsContainer = transform.Find("StatsContainer");
        moveSpeedText = statsContainer.Find("MoveSpeedText").GetComponent<TextMeshProUGUI>();
        damageText = statsContainer.Find("DamageText").GetComponent<TextMeshProUGUI>();
        attackSpeedText = statsContainer.Find("AttackSpeedText").GetComponent<TextMeshProUGUI>();
        bulletSpeedText = statsContainer.Find("BulletSpeedText").GetComponent<TextMeshProUGUI>();
        rangeText = statsContainer.Find("RangeText").GetComponent<TextMeshProUGUI>();
        bulletSizeText = statsContainer.Find("BulletSizeText").GetComponent<TextMeshProUGUI>();
        maxHealthText = statsContainer.Find("MaxHealthText").GetComponent<TextMeshProUGUI>();
    }

    public void SetStats(float moveSpeed, float damage, float attackSpeed, float bulletSpeed, float range, float bulletSize, float maxHealth) {
        moveSpeedText.SetText("Скорость: " + moveSpeed.ToString());
        damageText.SetText("Урон: " + damage.ToString());
        attackSpeedText.SetText("Скорость атаки: " + attackSpeed.ToString());
        bulletSpeedText.SetText("Скорость снарядов: " + bulletSpeed.ToString());
        rangeText.SetText("Дальность: " + range.ToString());
        bulletSizeText.SetText("Размер снарядов: " + bulletSize.ToString());
        maxHealthText.SetText("Максимальное ОЗ: " + maxHealth.ToString());
    }
}
