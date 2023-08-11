using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothbrushSelect : MonoBehaviour
{
    [SerializeField] Monologue monologue;
    [SerializeField] int sequenceNumber;

    public void TriggerMonologue() {
        monologue.sequenceNumber = sequenceNumber;
        monologue.StartMonologue();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
