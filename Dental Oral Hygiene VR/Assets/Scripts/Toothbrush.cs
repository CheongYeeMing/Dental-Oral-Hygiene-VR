using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toothbrush : MonoBehaviour
{
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = GetComponent<Transform>().position;
        originalRotation = GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Plague") {
            other.gameObject.GetComponent<Plague>().ReduceHealth();
        }  
    }

    public void ResetPosition() {
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
    }
}
