using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monologue : MonoBehaviour
{   
    public List<Sequence> Sequences;
    public int sequenceNumber;

    [System.Serializable]
    public class Sequence
    {
        public bool nextAngle;
        public string jawAngle;
        public bool isEnd;

        [TextArea(3, 15)]
        public string[] monologue;
    }

    private MonologueAnimation monologueAnimation;
    private const float TEXT_TYPING_SPEED = 0.008f;
    private const string ANIMATOR_IS_OPEN = "IsOpen";
    private const string AUDIO_CLICK = "Click";
    private const string AUDIO_DIALOGUE_MONOLOGUE = "DialogueMonologue";
    private const string AUDIO_OPEN = "Open";
    private const string AUDIO_RETRO_CLICK = "RetroClick";

    [SerializeField] private Text monologueBox;
    [SerializeField] private JawSceneAnimation jawSceneAnimation;
    [SerializeField] private MonologueFocus monologueFocus;

    public bool isTalking = false;
    private bool isTyping;
    public int currElement;
    private string currentSentence;

    // Start is called before the first frame update
    void Start()
    {
        monologueAnimation = GetComponent<MonologueAnimation>();
        sequenceNumber = 0;
        Debug.Log("Monologue Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMonologue()
    {
        monologueFocus.Toggle();
        monologueAnimation.Toggle();
        Debug.Log("Monologue Triggered");
        isTalking = true;
        currElement = 0;
        StartCoroutine(TypeSentence(Sequences[sequenceNumber].monologue[currElement]));
    }

    public void EndMonologue()
    {
        monologueFocus.Toggle();
        monologueAnimation.Toggle();
        Debug.Log("Monologue Closed");
        isTalking = false;
        if (Sequences[sequenceNumber].nextAngle) 
        {
            jawSceneAnimation.ChangeAnimationState(Sequences[sequenceNumber].jawAngle);
        }
        sequenceNumber++;
    }

    // Method to be triggered by VR Grabbable UI
    public void VRDialogueBoxHandClick()
    {
        Debug.Log("Clicked!!!");
        if (isTyping) 
        {
            CompleteSentence();
            return;
        }
        if (currElement < Sequences[sequenceNumber].monologue.Length -1)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(Sequences[sequenceNumber].monologue[currElement + 1]));
            currElement++;
        } 
        else if (Sequences[sequenceNumber].isEnd)
        {
            EndMonologue();
        } 
        else
        {
            EndMonologue();
            Invoke("StartMonologue", 2);
        }
    }

    // Text Typing Animation
    public IEnumerator TypeSentence(string sentence) 
    {
        monologueBox.text = "";
        isTyping = true;
        currentSentence = sentence;
        foreach (char letter in sentence.ToCharArray())
        {
            monologueBox.text += letter;
            yield return new WaitForSeconds(TEXT_TYPING_SPEED);
        }
        isTyping = false;
    }

    // Depending on whether allow to skip the monologue
    private void CompleteSentence() 
    {
        StopAllCoroutines();
        monologueBox.text = currentSentence;
        isTyping = false;
    }
}
