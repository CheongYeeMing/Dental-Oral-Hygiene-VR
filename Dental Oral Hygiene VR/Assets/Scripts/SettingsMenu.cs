using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] SettingsMenuAnimation settingsMenuAnimation;
    [SerializeField] public XrRigAnimation xrRigAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume() {
        settingsMenuAnimation.Toggle();
    }

    public void MainMenu() {
        // Reference to scene changer instance to MM
        SceneChanger.Instance.LoadBathroom();
    }

    public void Back() {
        // Revert back to Main Menu View
        settingsMenuAnimation.Toggle();
        xrRigAnimation.ToggleSettings();
    }

    public void Quit() {
        Application.Quit();
    }
}
