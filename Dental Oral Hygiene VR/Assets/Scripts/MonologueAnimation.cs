using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueAnimation : MonoBehaviour, Animation
{
    [SerializeField] public Animator animator;

    protected string currentState;

    protected const string DIALOGUE_BOX_OPEN = "DialogueBoxOpen";
    protected const string DIALOGUE_BOX_CLOSE = "DialogueBoxClose";

    // Start is called before the first frame update
    public virtual void Start()
    {
        ChangeAnimationState(DIALOGUE_BOX_OPEN);
    }

    public void Toggle() 
    {
        if (currentState == DIALOGUE_BOX_OPEN) {
            ChangeAnimationState(DIALOGUE_BOX_CLOSE);
        } else if (currentState == DIALOGUE_BOX_CLOSE) {
            ChangeAnimationState(DIALOGUE_BOX_OPEN);
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

