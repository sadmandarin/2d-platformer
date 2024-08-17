using UnityEngine;

public class MainCameraMoving : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private float _offset = 3.75f;

    private void LateUpdate()
    {
        transform.position = _playerTransform != null
            ? new Vector3(_playerTransform.position.x, _playerTransform.position.y + _offset, -1)
            : transform.position;
    }
}
