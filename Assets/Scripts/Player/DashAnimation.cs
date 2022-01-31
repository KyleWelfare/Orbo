using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAnimation : MonoBehaviour
{
    private Player player;
    private Animator anim;
    private SpriteRenderer SR;
    void Awake()
    {
        //player = GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        SR.enabled = false;
    }

    
    void Update()
    {
        if (player.StateMachine.CurrentState == player.DashState)
        {          
            SR.enabled = true;
            anim.SetBool("isDashing", true);   
        }

        if (player.CheckIfTouchingWall())
        {
            anim.SetBool("isDashing", false);
            SR.enabled = false;
        }

        //if (player.StateMachine.CurrentState == player.DashState && player.DashState.oldXPos == player.transform.position.x)
        //{
        //    anim.SetBool("isDashing", false);
        //    SR.enabled = false;
        //}
    }

    private void AnimationStartPos()
    {
        if (player.FacingDirection < 0)
        {
            SR.flipX = true;
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }
        else if (player.FacingDirection > 0)
        {
            SR.flipX = false;
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }
    }
    private void AnimationFinished()
    {
        SR.enabled = false;
        anim.SetBool("isDashing", false);
        //transform.parent = player.transform;

    }

    //private IEnumerator DashAnim()
    //{
    //    transform.parent = null;
    //    SR.enabled = true;
    //    anim.Play("dashAnim");
    //    yield return new WaitForSeconds(0.3f);
    //    SR.enabled = false;
    //    transform.parent = player.gameObject.transform;
    //    transform.position = player.transform.position;
    //    shouldDashAnim = true;
    //}
}
