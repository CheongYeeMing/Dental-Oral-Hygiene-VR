using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushingProgressBar : MonoBehaviour, Animation
{
    [SerializeField] public Animator animator;
    [SerializeField] public Slider slider;
    [SerializeField] private int stages;

    protected string currentState;

    protected const string PROGRESS_BAR_OPEN = "BrushingProgressBarOpen";
    protected const string PROGRESS_BAR_CLOSE = "BrushingProgressBarClose";

    private float value;
    private float progress;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        value = 100/stages;
        progress = 0;
        slider.minValue = 0;
        slider.maxValue = 100;
        ChangeAnimationState(PROGRESS_BAR_CLOSE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartBrushing() {
        // Set up Scene 3a Progress Bar 
        progress = 0;
        UpdateProgressBar();
    }

    public void IncrementProgress() {
        // Increase by some set amount
        progress += value;
        UpdateProgressBar();
    }

    private void UpdateProgressBar() {
        // Update Progress Bar UI
        slider.value = progress;
    }

    private void CompleteBrushing() {
        // Progress to Scene 3b
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
}
