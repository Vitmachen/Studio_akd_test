using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;

    void LateUpdate()
    {
        transform.position = _target.transform.position + _offset;
        //transform.rotation = _target.transform.rotation;
    }
}
