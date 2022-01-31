using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationManager : MonoBehaviour
{

    //public static AnimationManager instance;
    private Animator animator;
    private string currentState;

    private void Awake()
    {
        //instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    //functions
    public void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        //play the animation
        animator.Play(newState);

        //reassign the current state
        currentState = newState;
    }
}
