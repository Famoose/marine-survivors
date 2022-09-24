using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private Vector2 currentMoveDirection = Vector2.zero;

    private void FixedUpdate()
    {
        transform.position += new Vector3(currentMoveDirection.x, currentMoveDirection.y, 0);
    }

    void OnMove(InputValue action)
    {
        currentMoveDirection = action.Get<Vector2>();
    }
}
