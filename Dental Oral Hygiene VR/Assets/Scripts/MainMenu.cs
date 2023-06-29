using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public XrRigAnimation xrRigAnimation;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public Animator labelAnimator;
    [SerializeField] public Transition transition;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("Play", 5);
        // transition.Play("FadeOut");
        if (MonologueData.sequenceNumber == 4) {
            ContinueSetup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Settings() {
        xrRigAnimation.ToggleSettings();
    }

    public void Play() {
        // dialogueBox.SetActive(true);
        labelAnimator.Play("Labels");
        dialogueBox.GetComponent<Monologue>().Play();
    }

    public void ContinueSetup() {
        labelAnimator.Play("Labels");
        dialogueBox.GetComponent<Monologue>().ContinueSetup();
    }

    public void Brushing() {
        transition.FadeOut();
        StartCoroutine(FindObjectOfType<AudioManager>().StopMusic("bathroom"));
        Invoke("ChangeScene", 1);
    }

    private void ChangeScene() {
        SceneChanger.Instance.LoadJawModel();
    }
}
