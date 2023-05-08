using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushingProgressBar : MonoBehaviour
{
    [SerializeField] private int stages;

    private float value;
    private float progress;
    
    // Start is called before the first frame update
    void Start()
    {
        value = 100/stages;
        progress = 0;
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

    private void IncrementProgress() {
        // Increase by some set amount
        progress += value;
        UpdateProgressBar();
    }

    private void UpdateProgressBar() {
        // Update Progress Bar UI
    }

    private void CompleteBrushing() {
        // Progress to Scene 3b
    }
}
