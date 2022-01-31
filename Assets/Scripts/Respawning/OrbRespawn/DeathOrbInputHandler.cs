using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathOrbInputHandler : MonoBehaviour
{
    public Vector2 movementInput { get; private set; }
    public bool DashInput { get; private set; }
    public float dashInputStartTime { get; private set; }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().normalized;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            dashInputStartTime = Time.time;
            Debug.Log("Dash");
        }
    }
    public void UseDashInput() => DashInput = false;
}
