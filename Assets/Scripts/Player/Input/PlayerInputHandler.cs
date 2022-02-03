using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float MovementInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool FireInput { get; private set; }
    public bool DashInput { get; private set; }

    [SerializeField] private float inputBufferTime;
    private float jumpInputStartTime;
    private float dashInputStartTime;

    public Vector2 OrbDirection;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = Mathf.Abs(context.ReadValue<float>()) <= Mathf.Epsilon ? 0 : Mathf.Sign(context.ReadValue<float>());

        //NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        //NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
            JumpInputStop = false;
        }

        if(context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void OnFireInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FireInput = true;
        }

        if (context.canceled)
        {
            FireInput = false;
        }
    }
    public void UseFireInput() => FireInput = false;

    public void OnAimInput(InputAction.CallbackContext context)
    {
        OrbDirection = context.ReadValue<Vector2>().normalized;
    }

    

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputBufferTime)
        {
            JumpInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            dashInputStartTime = Time.time;
        }
    }
    public void UseDashInput() => DashInput = false;
}
