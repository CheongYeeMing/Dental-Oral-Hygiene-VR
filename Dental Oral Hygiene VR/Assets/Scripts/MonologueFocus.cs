using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueFocus : MonoBehaviour, Animation
{
    [SerializeField] public Animator animator;

    protected string currentState;

    protected const string FOCUS_IN = "FocusIn";
    protected const string FOCUS_OUT = "FocusOut";

    // Start is called before the first frame update
    public virtual void Start()
    {
        ChangeAnimationState(FOCUS_IN);
    }

    public void Toggle() 
    {
        if (currentState == FOCUS_IN) {
            ChangeAnimationState(FOCUS_OUT);
        } else if (currentState == FOCUS_OUT) {
            ChangeAnimationState(FOCUS_IN);
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
