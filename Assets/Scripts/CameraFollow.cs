using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Ссылка на Transform персонажа
    public Vector3 offset;    // Смещение камеры относительно персонажа

    void LateUpdate()
    {
      
        if (target != null)
        {
           Debug.Log("Camera Follow is running");
           transform.position = target.position + offset;
        }
    }
}