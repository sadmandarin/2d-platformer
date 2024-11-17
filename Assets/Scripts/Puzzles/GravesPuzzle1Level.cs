using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravesPuzzle1Level : MonoBehaviour
{
    [SerializeField] private Grave[] _graves;
    [SerializeField] private List<int> _rightSequence;
    [SerializeField] private GameObject _doorToOpen;
    [SerializeField] private List<EnemyBase> _enemies;
    [SerializeField] private List<Vector2> _enemyPosition;

    private bool _rightOrder = false;
    private List<int> _currentSequence = new();

    public bool PuzzleComplete = false;

    public void PressGrave(int index)
    {
        _currentSequence.Add(index);

        if (_currentSequence.Count == 4)
        {
            for (int i = 0; i < _rightSequence.Count; i++)
            {
                _rightOrder = _rightSequence[i] == _currentSequence[i] ? true : false;
            }

            if (_rightOrder)
                OnCorrectSequence();
            else
                OnWrongSequence();
        }
    }

    private void OnCorrectSequence()
    {
        Destroy(_doorToOpen);
        for (int i = 0; i < _graves.Length; i++)
        {
            Destroy(_graves[i].gameObject.GetComponent<BoxCollider2D>());
        }
        PuzzleComplete = true;
    }

    private void OnWrongSequence()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            Instantiate(_enemies[i], _enemyPosition[i], Quaternion.Euler(0, 180,0));
        }

        foreach (var grave in _graves)
        {
            grave.IsPressed = false;
        }

        _currentSequence.Clear();
    }
}
