using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 6.0f;

    [Header("Jump State")]
    public float jumpVelocity = 17;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float minJumpTime = 0.05f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    public float wallCheckDistance = 0.5f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.2f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float dashDuration = 0.2f;
    public float dashVelocity = 30f;

    [Header("Knockback State")]
    public float knockbackVelocityX = 8;
    public float knockbackVelocityY = 5;
    public float knockbackDuration = 0.2f;

}
