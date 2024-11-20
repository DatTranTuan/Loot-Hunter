using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity entity;
    private CharacterStats myStats;

    // RectTransform dieu chinh vi tri cua Canvars
    private RectTransform myTransfrom;

    // Slider thanh mau
    private Slider slider;

    private void Start()
    {
        // khoi tao
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        myStats = GetComponentInParent<CharacterStats>();
        myTransfrom = GetComponent<RectTransform>();

        entity.OnFlipped += FlipUI;

        myStats.onHealthChanged += UpdateHealthUI;
        UpdateHealthUI();
     //   Debug.Log("character stats called");
    }
    private void Update()
    {
        // c?p nh?t tr?ng thái máu
        UpdateHealthUI();
    }
    // t?o hàm c?p nh?t máu thông qua  class CharacterStats
    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.maxHealth;
        slider.value = myStats.currentHealth;
    }
    // lat, ??i h??ng  canvar
    private void FlipUI()
    {
        myTransfrom.Rotate(0, 180, 0);
    }

    // loai bo khi b? l?t ho?c thay ??i máu
    private void OnDisable()
    {
        entity.OnFlipped -= FlipUI;
        myStats.onHealthChanged -= UpdateHealthUI;
    }
}
