using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushingProgressBarAnimation : MonoBehaviour, Animation
{
    [SerializeField] public Animator animator;

    protected string currentState;

    protected const string PROGRESS_BAR_OPEN = "BrushingProgressBarOpen";
    protected const string PROGRESS_BAR_CLOSE = "BrushingProgressBarClose";

    // Start is called before the first frame update
    public virtual void Start()
    {
        ChangeAnimationState(PROGRESS_BAR_CLOSE);
    }

    public void Toggle() 
    {
        if (currentState == PROGRESS_BAR_OPEN) {
            ChangeAnimationState(PROGRESS_BAR_CLOSE);
        } else if (currentState == PROGRESS_BAR_CLOSE) {
            ChangeAnimationState(PROGRESS_BAR_OPEN);
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

    public void DelayToggle() {
        Invoke("Toggle", 2);
    }
}