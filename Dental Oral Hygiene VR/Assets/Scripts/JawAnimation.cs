using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawAnimation : MonoBehaviour, Animation
{
    [SerializeField] public Animator animator;

    protected string currentState;

    protected const string JAW_OPEN = "JawOpenNew";
    protected const string JAW_CLOSE = "JawCloseNew";
    protected const string UPPER_TEETH_A = "upper_teeth_a";
    protected const string UPPER_TEETH_B = "upper_teeth_b";
    protected const string UPPER_TEETH_C = "upper_teeth_c";
    protected const string UPPER_TEETH_D = "upper_teeth_d";
    protected const string UPPER_TEETH_E = "upper_teeth_e";
    protected const string UPPER_TEETH_F = "upper_teeth_f";
    protected const string LOWER_TEETH_A = "lower_teeth_a";
    protected const string LOWER_TEETH_B = "lower_teeth_b";
    protected const string LOWER_TEETH_C = "lower_teeth_c";
    protected const string LOWER_TEETH_D = "lower_teeth_d";
    protected const string LOWER_TEETH_E = "lower_teeth_e";
    protected const string LOWER_TEETH_F = "lower_teeth_f";
    protected const string BRUSHING_COMPLETED = "brushing_completed";

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
