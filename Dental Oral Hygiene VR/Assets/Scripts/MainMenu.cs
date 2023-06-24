using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public XrRigAnimation xrRigAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Settings() {
        xrRigAnimation.ToggleSettings();
    }

    public void Play() {
        
    }
}
