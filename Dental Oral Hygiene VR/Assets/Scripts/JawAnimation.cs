using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawAnimation : MonoBehaviour
{
    [SerializeField] public Animator animator;

    protected string currentState

    protected const string JAW_OPEN = "Open"
    protected const string JAW_CLOSE = "Close"

    // Start is called before the first frame update
    public virtial void Start()
    {
        ChangeAnimationState(JAW_CLOSE);
    }

    public void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself
        if (currentState == newState) return;

        // Play the Animation
        animator.Play(newState)

        // Reassign current state with new state
        currentState = newState
    }
}
