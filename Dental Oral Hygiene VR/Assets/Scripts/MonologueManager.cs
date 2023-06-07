using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueManager : MonoBehaviour
{
    public Monologue monologue;

    // Start is called before the first frame update
    void Start()
    {
        monologue = GetComponent<Monologue>();
        Invoke("InitializeMonologue", 5);    
    }

    void InitializeMonologue() {
        monologue.StartMonologue();
    }
}
