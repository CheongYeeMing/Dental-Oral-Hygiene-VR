using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public List<GameObject> PS;
    public List<GameObject> AllPlague;

    public List<GameObject> currentList;
    [SerializeField] GameObject toothbrush;
    [SerializeField] GameObject interDentalToothbrush;
    [SerializeField] JawAnimation jawAnimation;
    [SerializeField] GameObject upperJaw;
    [SerializeField] GameObject lowerJaw;
    [SerializeField] GameObject jawModel;
    [SerializeField] GameObject image;
    [SerializeField] List<GameObject> toothbrushSelection;
    [SerializeField] List<GameObject> brushingPhaseObjects;
    [SerializeField] GameObject toothpaste;

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
        public bool isMainMenu;
        public bool activatePS;
        public bool activateToothpaste;
        public bool goBackMainMenu;
        public bool reset;
        public bool hasImage;
        public bool isToothbrushSelectionPhase;
        public bool isOngoingSelectionPhase;
        public bool isEndOfToothbrushSelectionPhase;
        public bool triggerRotateJawAtEnd;
        public bool triggerStopRotateAndChangePlagueMaterial;
        public bool skipMonologue;
        public Sprite image;

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

    [SerializeField] private TextMeshProUGUI monologueBox;
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
    public float doubleClick = 0.3f;
    public float clickTimer = 1;
    public bool isClickable = false;

    // Start is called before the first frame update
    void Start()
    {
        monologueAnimation = GetComponent<MonologueAnimation>();
        sequenceNumber = MonologueData.sequenceNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBrushing)
        {
            CheckBrushingCompletion();
        }
        clickTimer += Time.deltaTime;
    }

    public void Play()
    {
        Invoke("StartMonologue", 2);
        MonologueData.sequenceNumber = 0;
    }

    public void ContinueSetup()
    {
        Invoke("StartMonologue", 2);
    }

    public void StartMonologue()
    {
        isClickable = true;
        if (!Sequences[sequenceNumber].isMainMenu && !Sequences[sequenceNumber].skipMonologue)
        {
            monologueFocus.Toggle();
        }
        if (Sequences[sequenceNumber].skipMonologue)
        {
            EndMonologue();
            return;
        }
        monologueAnimation.Toggle();
        isTalking = true;
        currElement = 0;
        if (Sequences[sequenceNumber].hasImage)
        {
            image.SetActive(true);
            image.GetComponent<Image>().sprite = Sequences[sequenceNumber].image;
        }
        if (Sequences[sequenceNumber].isToothbrushSelectionPhase || Sequences[sequenceNumber].isOngoingSelectionPhase)
        {
            foreach (GameObject toothbrushOption in toothbrushSelection)
            {
                toothbrushOption.GetComponent<BoxCollider>().enabled = false;
            }
        }
        if (Sequences[sequenceNumber].isToothbrushSelectionPhase)
        {
            foreach (GameObject toothbrushOption in toothbrushSelection)
            {
                toothbrushOption.SetActive(true);
            }
            foreach (GameObject obj in brushingPhaseObjects)
            {
                obj.SetActive(false);
            }
            jawModel.SetActive(false);
        }
        StartCoroutine(TypeSentence(Sequences[sequenceNumber].monologue[currElement]));
    }

    public void EndMonologue()
    {
        isClickable = false;
        if (!Sequences[sequenceNumber].isMainMenu && !Sequences[sequenceNumber].skipMonologue)
        {
            monologueFocus.Toggle();
        }
        if (!Sequences[sequenceNumber].skipMonologue)
        {
            monologueAnimation.Toggle();
            isTalking = false;
        }

        if (Sequences[sequenceNumber].nextAngle)
        {
            jawSceneAnimation.ChangeAnimationState(Sequences[sequenceNumber].jawAngle);
            if (Sequences[sequenceNumber].plagueBrush)
            {
                ActivatePlague(Sequences[sequenceNumber].jawAngle);
            }
            if (Sequences[sequenceNumber].hasNewToothbrushTransform)
            {
                toothbrush.GetComponent<Toothbrush>().ChangeTransform(Sequences[sequenceNumber].newToothbrushTransform);
            }
        }
        if (Sequences[sequenceNumber].enableInterdentaldisableToothbrush)
        {
            toothbrush.SetActive(false);
            interDentalToothbrush.SetActive(true);
        }
        if (Sequences[sequenceNumber].activatePS)
        {
            foreach (GameObject ps in PS)
            {
                ps.SetActive(true);
            }
        }
        if (Sequences[sequenceNumber].activateToothpaste)
        {
            toothpaste.SetActive(true);
        }
        if (Sequences[sequenceNumber].goBackMainMenu)
        {
            Invoke("BackToMainMenu", 3);
        }
        if (Sequences[sequenceNumber].reset)
        {
            FindObjectOfType<MainMenu>().labelAnimator.Play("LabelsStay");
            MonologueData.sequenceNumber = 0;
            sequenceNumber = 0;
            return;
        }
        if (Sequences[sequenceNumber].hasImage)
        {
            image.SetActive(false);
        }
        if (Sequences[sequenceNumber].isToothbrushSelectionPhase || Sequences[sequenceNumber].isOngoingSelectionPhase)
        {
            foreach (GameObject toothbrushOption in toothbrushSelection)
            {
                toothbrushOption.GetComponent<BoxCollider>().enabled = true;
            }
        }
        if (Sequences[sequenceNumber].isEndOfToothbrushSelectionPhase)
        {
            foreach (GameObject toothbrushOption in toothbrushSelection)
            {
                toothbrushOption.SetActive(false);
            }
            foreach (GameObject obj in brushingPhaseObjects)
            {
                obj.SetActive(true);
            }
            jawModel.SetActive(true);
            jawModel.GetComponent<JawAnimation>().Toggle();
        }
        if (Sequences[sequenceNumber].triggerRotateJawAtEnd)
        {
            // Trigger roatating jaw
            jawModel.GetComponent<JawAnimation>().ChangeAnimationState("JawRotate");

            // Start monologue after rotate jaw
            Invoke("StartMonologue", 15);
        }
        else if (Sequences[sequenceNumber].triggerStopRotateAndChangePlagueMaterial)
        {
            // jawModel.GetComponent<JawAnimation>().ChangeAnimationState("JawCloseNew");
            DeactivatePlague();
        }
        if (sequenceNumber == 7) return; // When select hard toothbrush
        sequenceNumber++;
    }

    public void BackToMainMenu()
    {
        FindObjectOfType<SettingsMenu>().MainMenu();
        MonologueData.sequenceNumber = 4;
    }

    // Method to be triggered by VR Grabbable UI
    public void VRDialogueBoxHandClick()
    {
        if (clickTimer < doubleClick || !isClickable) return;
        clickTimer = 0;
        FindObjectOfType<AudioManager>().StopEffect("click");
        FindObjectOfType<AudioManager>().PlayEffect("click");
        if (isTyping)
        {
            CompleteSentence();
            return;
        }
        if (currElement < Sequences[sequenceNumber].monologue.Length - 1)
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
            jawModel.GetComponent<JawAnimation>().ChangeAnimationState("JawCloseNew");
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
            RandomlyActivatePlague(INTERDENTAL_A);
            jawAnimation.JawClose();
        }
        else if (angle == "interdental_b")
        {
            RandomlyActivatePlague(INTERDENTAL_B);
        }
    }

    public void ActivateBoxCollider(List<GameObject> plagueList)
    {
        isBrushing = true;
        currentList = plagueList;
        foreach (GameObject plague in plagueList)
        {
            plague.GetComponent<BoxCollider>().enabled = true;
            plague.GetComponent<MeshRenderer>().sharedMaterial = plagueActive;
        }
    }

    public void DeactivatePlague()
    {
        foreach (GameObject plague in AllPlague)
        {
            plague.GetComponent<MeshRenderer>().sharedMaterial = plagueInactive;
        }
    }

    public void RandomlyActivatePlague(List<GameObject> plagueList)
    {
        isBrushing = true;
        currentList = plagueList;
        // Random random = new Random();
        int top_1 = Random.Range(0, 4);
        int top_2 = top_1;
        while (top_2 == top_1)
        {
            top_2 = Random.Range(0, 4);
        }
        int bot_1 = Random.Range(4, 8);
        int bot_2 = bot_1;
        while (bot_2 == bot_1)
        {
            bot_2 = Random.Range(4, 8);
        }
        plagueList[top_1].SetActive(true);
        plagueList[top_2].SetActive(true);
        plagueList[bot_1].SetActive(true);
        plagueList[bot_2].SetActive(true);
    }

    public void CheckBrushingCompletion()
    {
        if (currentList != null)
        {
            foreach (GameObject plague in currentList)
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
        FindObjectOfType<AudioManager>().PlayEffect("success");
        isBrushing = false;
        if (toothbrush.activeSelf)
        {
            toothbrush.gameObject.GetComponent<Toothbrush>().ResetPosition();
        }
        if (interDentalToothbrush.activeSelf)
        {
            interDentalToothbrush.gameObject.GetComponent<Toothbrush>().ResetPosition();
        }
        brushingProgressBar.IncrementProgress();
        Invoke("StartMonologue", 2);
    }
}
