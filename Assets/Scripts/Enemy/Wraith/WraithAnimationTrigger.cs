using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithAnimationTrigger : MonoBehaviour
{
    private Wraith_Enemy wraith => GetComponentInParent<Wraith_Enemy>();

    private void AnimationTrigger()
    {
        wraith.AnimationFinishTrigger();
    }
    private void CreateWraithMini()
    {
        for(int i = 0;i<5;i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * 2f; // Tạo một vị trí ngẫu nhiên trong bán kính 2 đơn vị
            Vector3 spawnPos = wraith.MiniPos.position + randomOffset;
            Instantiate(wraith.WraithMini,spawnPos, Quaternion.identity);
        }
        wraith.AnimationFinishTrigger();
    }
}
