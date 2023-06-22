using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuAnimation : MonoBehaviour, Animation
{
    [SerializeField] public Animator animator;

    protected string currentState;

    protected const string SETTINGS_MENU_OPEN = "SettingsOpen";
    protected const string SETTINGS_MENU_CLOSE = "SettingsClose";

    // Start is called before the first frame update
    public virtual void Start()
    {
        ChangeAnimationState(SETTINGS_MENU_CLOSE);
    }

    public void Toggle() 
    {
        if (currentState == SETTINGS_MENU_OPEN) {
            ChangeAnimationState(SETTINGS_MENU_CLOSE);
        } else if (currentState == SETTINGS_MENU_CLOSE) {
            ChangeAnimationState(SETTINGS_MENU_OPEN);
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
