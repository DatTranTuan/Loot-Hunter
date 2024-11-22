using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleWitchAnimationTriggers : MonoBehaviour
{
    private PurpleWitch  purpleWitch => GetComponentInParent<PurpleWitch>();

    private void AnimationTrigger()
    {
        purpleWitch.AnimationFinishTrigger();
    }

    private IEnumerator CreateAndDestroyPurpleWitch()
    {
        // Tạo ra bản sao của đối tượng PurpleWitch
        GameObject clone = Instantiate(purpleWitch.ClonePW, purpleWitch.PlayerPos.position, Quaternion.identity);

        // Đợi 2 giây
        yield return new WaitForSeconds(2f);

        // Hủy bản sao sau 2 giây
        Destroy(clone);

        // Gọi hàm kích hoạt trigger của animation
        purpleWitch.AnimationFinishTrigger();
    }

    private void CreatePurpleWitch()
    {
        // Khởi động coroutine để tạo và hủy bản sao
        StartCoroutine(CreateAndDestroyPurpleWitch());
    }
}
 