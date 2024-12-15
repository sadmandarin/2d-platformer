using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MoveCameraToQuest : MonoBehaviour
{
    private Inputs _inputs;
    private Vector2 _startPos;


    private void Awake()
    {
        _inputs = new Inputs();
    }

    public IEnumerator MoveCameraTowardsQuest(Transform position)
    {
        _startPos = transform.position;

        while (Vector2.Distance(transform.position, position.position) > 0.1f)
        {
            Vector2.MoveTowards(transform.position, position.position, 1f * Time.deltaTime); 

            yield return null;
        }

        OnCameraInPosition();
    } 

    private IEnumerator MoveCameraBack()
    {
        while (Vector2.Distance(transform.position, _startPos) > 0.1f)
        {
            Vector2.MoveTowards(transform.position, _startPos, 1f * Time.deltaTime);

            yield return null;
        }

        _inputs.GamePlay.Interaction.performed -= CloseCamera;
    }

    private void OnCameraInPosition()
    {
        _inputs.GamePlay.Interaction.performed += CloseCamera;
    }

    private void CloseCamera(InputAction.CallbackContext context)
    {
        StartCoroutine(MoveCameraBack());
    }
}
