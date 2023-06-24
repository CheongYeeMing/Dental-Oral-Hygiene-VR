using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public XrRigAnimation xrRigAnimation;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public Animator labelAnimator;
    [SerializeField] public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("Play", 5);
        transition.Play("FadeOut");
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
        transition.Play("FadeIn");
        Invoke("ChangeScene", 1);
    }

    private void ChangeScene() {
        SceneChanger.Instance.LoadJawModel();
    }
}
