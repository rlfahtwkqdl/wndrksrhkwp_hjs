using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public Transform target; // 여기에 Main Camera를 드래그해서 넣으세요.

    void LateUpdate()
    {
        if (target != null)
        {
            // 카메라의 X, Y만 따라가고 Z는 내 원래 위치를 지킴
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
