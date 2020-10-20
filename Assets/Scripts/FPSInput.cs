using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSInput : MonoBehaviour
{
    [SerializeField] bool _invertVertical = false;

    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    public event Action JumpInput = delegate { };

    void Update()
    {
        DetectMoveInput();
        DetectRotateInput();
        DetectJumpInput();
    }


    void DetectMoveInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        if (xInput != 0 || yInput != 0)
        {
            Vector3 _horizontalMovement = transform.right * xInput;
            Vector3 _forwardMovement = transform.forward * yInput;
            Vector3 movement = (_horizontalMovement + _forwardMovement).normalized;
            // notify we have moved
            MoveInput?.Invoke(movement);
        }
    }

    void DetectRotateInput()
    {
        float xInput = Input.GetAxisRaw("Mouse X");
        float yInput = Input.GetAxisRaw("Mouse Y");

        if (xInput != 0 || yInput != 0)
        {
            if (_invertVertical == true) { yInput = -yInput; }
        }
        // LR should be y axis and updown is x axis
        Vector3 rotation = new Vector3(yInput, xInput, 0);
        // notify we have rotated
        RotateInput?.Invoke(rotation);
    }

    void DetectJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput?.Invoke();
        }
    }
}