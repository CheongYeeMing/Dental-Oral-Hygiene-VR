using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monologue : MonoBehaviour
{
    private const float TYPING_DELAY = 0.008f;

    [SerializeField] Sprite clara;
    [SerializeField] Text examineText;


    private bool isTyping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Text Typing Animation
    public IEnumerator TypeSentence(string sentence) 
    {
        examineText.text = "";
        isTyping = true;
        previousSentence = sentence;
        foreach (char letter in sentence.ToCharArray())
        {
            examineText.text += letter;
            yield return new WaitForSeconds(TYPING_DELAY);
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
