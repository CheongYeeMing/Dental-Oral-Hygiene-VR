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
        public bool nextStage;
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

    [SerializeField] Sprite clara;
    [SerializeField] Text examineText;

    public bool isTalking = false;
    private bool isTyping;
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
        monologueAnimation.Toggle();
        Debug.Log("Monologue Triggered");
        isTalking = true;
        StartCoroutine(TypeSentence(Sequences[sequenceNumber].monologue[0]))
    }

    // Method to be triggered by VR Grabbable UI
    public void VRDialogueBoxHandClick()
    {
        if (isTyping) 
        {
            CompleteSentence();
        }
    }

    // Text Typing Animation
    public IEnumerator TypeSentence(string sentence) 
    {
        examineText.text = "";
        isTyping = true;
        currentSentence = sentence;
        foreach (char letter in sentence.ToCharArray())
        {
            examineText.text += letter;
            yield return new WaitForSeconds(TEXT_TYPING_SPEED);
        }
        isTyping = false;
    }

    // Depending on whether allow to skip the monologue
    private void CompleteSentence() 
    {
        StopAllCoroutines();
        isTyping = false;
    }
}
