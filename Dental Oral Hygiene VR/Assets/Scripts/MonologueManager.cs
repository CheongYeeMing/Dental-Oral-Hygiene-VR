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
        Debug.Log(SceneChanger.Instance.GetScene());
        if (SceneChanger.Instance.GetScene() == "JawModel")
        {
            Invoke("InitializeMonologue", 5);  
        }
    }

    void InitializeMonologue() {
        monologue.StartMonologue();
    }
}
