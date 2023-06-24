using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public XrRigAnimation xrRigAnimation;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public Animator labelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("Play", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Settings() {
        xrRigAnimation.ToggleSettings();
    }

    public void Play() {
        dialogueBox.SetActive(true);
        labelAnimator.Play("Labels");
    }

    public void Brushing() {
        SceneChanger.Instance.LoadJawModel();
    }
}
