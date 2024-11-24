using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar_Hieu : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform camera;
    public Transform enemy;

    void Start()
    {
        camera = Camera.main.transform;
    }

    void Update()
    {
        if (enemy != null)
        {
            // Cập nhật vị trí thanh máu theo vị trí của quái vật
            transform.position = enemy.position + Vector3.up * 1; // Điều chỉnh vị trí cao hơn đầu quái vật
        }
    }

    void LateUpdate()
    {
        // Thanh máu luôn hướng về camera
        transform.LookAt(transform.position + camera.forward);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
