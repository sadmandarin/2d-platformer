using UnityEngine;

public class StelsMode : MonoBehaviour
{
    private bool _canEnterStelsMode;
    private int _stelsObjectSortingOrder;
    private int _playerSortingOrder;

    private Player _player;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _player = GetComponent<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerSortingOrder = _spriteRenderer.sortingOrder;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_canEnterStelsMode)
            {
                if (!_player.IsInStelsMode)
                {
                    EnterStelsMode();
                }
                else
                {
                    ExitStelsMode();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StelsZone>())
        {
            _canEnterStelsMode = true;
            Debug.Log("Can enter StelsMode");
            _stelsObjectSortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StelsZone>())
        {
            _canEnterStelsMode = false;
            ExitStelsMode();
            Debug.Log("Exit StelsMode zone");
        }
    }

    void EnterStelsMode()
    {
        _player.ChangeStelsMode(true);
        _spriteRenderer.sortingOrder = _stelsObjectSortingOrder - 1;
    }

    void ExitStelsMode()
    {
        _player.ChangeStelsMode(false);
        _spriteRenderer.sortingOrder = _playerSortingOrder;
    }
}
