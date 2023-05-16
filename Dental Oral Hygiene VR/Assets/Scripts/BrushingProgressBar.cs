using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushingProgressBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] private int stages;

    private float value;
    private float progress;
    
    // Start is called before the first frame update
    void Start()
    {
        value = 100/stages;
        progress = 0;
        slider.minValue = 0;
        slider.maxValue = 100;
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
}
