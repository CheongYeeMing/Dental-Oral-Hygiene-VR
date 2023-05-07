using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushingProgressBar : MonoBehaviour
{
    private float value;
    [SerializeField] private int stages;
    
    // Start is called before the first frame update
    void Start()
    {
        value = 100/stages;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncrementProgress() {
        // Increase by some set amount
    }

    private void UpdateProgressBar() {
        // Update Progress Bar UI
    }

    private void CompleteBrushing() {
        // Progress to Scene 3b
    }
}
