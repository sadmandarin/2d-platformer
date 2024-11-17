using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCloseRangeGround : MonoBehaviour
{
    public LayerMask GroundLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            GroundLayerMask = collision.gameObject.layer;
        }
    }
}
