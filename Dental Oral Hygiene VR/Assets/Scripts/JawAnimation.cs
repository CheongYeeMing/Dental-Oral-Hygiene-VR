using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawAnimation : MonoBehaviour
{
    [SerializeField] public Animator animator;

    protected string currentState;

    protected const string JAW_OPEN = "JawOpen";
    protected const string JAW_CLOSE = "JawClose";

    // Start is called before the first frame update
    public virtual void Start()
    {
        ChangeAnimationState(JAW_CLOSE);
    }

    public void Toggle() 
    {
        if (currentState == JAW_OPEN) {
            ChangeAnimationState(JAW_CLOSE);
        } else if (currentState == JAW_CLOSE) {
            ChangeAnimationState(JAW_OPEN);
        }
    }

    public void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself
        if (currentState == newState) return;

        // Play the Animation
        animator.Play(newState);

        // Reassign current state with new state
        currentState = newState;
    }
}
