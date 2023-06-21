using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monologue : MonoBehaviour
{   
    public List<Sequence> Sequences;
    public int sequenceNumber;

    public List<GameObject> UPPER_TEETH_A;
    public List<GameObject> UPPER_TEETH_B;
    public List<GameObject> UPPER_TEETH_C;
    public List<GameObject> UPPER_TEETH_D;
    public List<GameObject> UPPER_TEETH_E;
    public List<GameObject> UPPER_TEETH_F;
    public List<GameObject> LOWER_TEETH_A;
    public List<GameObject> LOWER_TEETH_B;
    public List<GameObject> LOWER_TEETH_C;
    public List<GameObject> LOWER_TEETH_D;
    public List<GameObject> LOWER_TEETH_E;
    public List<GameObject> LOWER_TEETH_F;
    public List<GameObject> UPPER_BITING_A;
    public List<GameObject> UPPER_BITING_B;
    public List<GameObject> LOWER_BITING_A;
    public List<GameObject> LOWER_BITING_B;
    public List<GameObject> INTERDENTAL_A;
    public List<GameObject> INTERDENTAL_B;
    
    public List<GameObject> currentList;
    [SerializeField] Toothbrush toothbrush;
    [SerializeField] Toothbrush interDentalToothbrush;
    [SerializeField] JawAnimation jawAnimation;
    [SerializeField] GameObject upperJaw;
    [SerializeField] GameObject lowerJaw;

    [System.Serializable]
    public class Sequence
    {
        public bool nextAngle;
        public string jawAngle;
        public bool plagueBrush;
        public bool isEnd;
        public bool hasNewToothbrushTransform;
        public Transform newToothbrushTransform;
        public bool enableInterdentaldisableToothbrush;

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
    [SerializeField] private Material plagueInactive;
    [SerializeField] private Material plagueActive;
    [SerializeField] private BrushingProgressBar brushingProgressBar;

    public bool isTalking = false;
    public bool isBrushing = false;
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
        if (isBrushing) {
            CheckBrushingCompletion();
        }
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
            if (Sequences[sequenceNumber].plagueBrush) 
            {
                ActivatePlague(Sequences[sequenceNumber].jawAngle);
            }
            if (Sequences[sequenceNumber].hasNewToothbrushTransform) 
            {
                toothbrush.ChangeTransform(Sequences[sequenceNumber].newToothbrushTransform);
            }
        }
        if (Sequences[sequenceNumber].enableInterdentaldisableToothbrush) 
        {
            toothbrush.GetComponent<BoxCollider>().enabled = false;
            interDentalToothbrush.GetComponent<BoxCollider>().enabled = true;
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

    public void ActivatePlague(string angle)
    {
        if (angle == "upper_teeth_a") 
        {
            ActivateBoxCollider(UPPER_TEETH_A);
        } 
        else if (angle == "upper_teeth_b")
        {
            ActivateBoxCollider(UPPER_TEETH_B);
        } 
        else if (angle == "upper_teeth_c")
        {
            ActivateBoxCollider(UPPER_TEETH_C);
        } 
        else if (angle == "upper_teeth_d")
        {
            Debug.Log("D Activated");
            ActivateBoxCollider(UPPER_TEETH_D);
            jawAnimation.JawOpen();
        } 
        else if (angle == "upper_teeth_e")
        {
            ActivateBoxCollider(UPPER_TEETH_E);
        } 
        else if (angle == "upper_teeth_f")
        {
            ActivateBoxCollider(UPPER_TEETH_F);
        }
        else if (angle == "lower_teeth_a")
        {
            ActivateBoxCollider(LOWER_TEETH_A);
            jawAnimation.JawClose();
        }
        else if (angle == "lower_teeth_b")
        {
            ActivateBoxCollider(LOWER_TEETH_B);
        }
        else if (angle == "lower_teeth_c")
        {
            ActivateBoxCollider(LOWER_TEETH_C);
        }
        else if (angle == "lower_teeth_d")
        {
            ActivateBoxCollider(LOWER_TEETH_D);
            jawAnimation.JawOpen();
        }
        else if (angle == "lower_teeth_e")
        {
            ActivateBoxCollider(LOWER_TEETH_E);
        }
        else if (angle == "lower_teeth_f")
        {
            ActivateBoxCollider(LOWER_TEETH_F);
        }
        else if (angle == "upper_biting_a")
        {
            ActivateBoxCollider(UPPER_BITING_A);
        }
        else if (angle == "upper_biting_b")
        {
            ActivateBoxCollider(UPPER_BITING_B);
        }
        else if (angle == "lower_biting_a")
        {
            ActivateBoxCollider(LOWER_BITING_A);
        }
        else if (angle == "lower_biting_b")
        {
            ActivateBoxCollider(LOWER_BITING_B);
        }
        else if (angle == "interdental_a")
        {
            ActivateBoxCollider(INTERDENTAL_A);
            jawAnimation.JawClose();
        }
        else if (angle == "interdental_b")
        {
            ActivateBoxCollider(INTERDENTAL_B);
        }
    }

    public void ActivateBoxCollider(List<GameObject> plagueList)
    {
        isBrushing = true;
        currentList = plagueList;
        foreach(GameObject plague in plagueList)
        {
            plague.GetComponent<BoxCollider>().enabled = true;
            plague.GetComponent<MeshRenderer>().sharedMaterial = plagueActive;
        }
    }

    public void CheckBrushingCompletion()
    {
        if (currentList != null) {
            foreach(GameObject plague in currentList)
            {
                if (plague.activeSelf) 
                {
                    return;
                }
            }
        }
        if (!isBrushing) 
        {
            return;
        }
        isBrushing = false;
        toothbrush.ResetPosition();
        brushingProgressBar.IncrementProgress();
        Invoke("StartMonologue", 2);
    }
}
