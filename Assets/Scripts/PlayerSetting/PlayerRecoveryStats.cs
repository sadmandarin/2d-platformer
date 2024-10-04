using UnityEngine;

public class PlayerRecoveryStats : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player.CurrentRecoveryItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _player.RecoverStats();
            }
        }
    }
}
