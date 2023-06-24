using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrRigAnimation : MonoBehaviour, Animation
{
    [SerializeField] public Animator animator;

    protected string currentState;

    protected const string XR_SETTINGS = "XRSettings";
    protected const string XR_SETTINGS_BACK = "XRSettingsBack";

    public void Start() {}
    
    public void ToggleSettings() 
    {
        if (currentState == XR_SETTINGS) {
            ChangeAnimationState(XR_SETTINGS_BACK);
        } else if (currentState == XR_SETTINGS_BACK) {
            ChangeAnimationState(XR_SETTINGS);
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
