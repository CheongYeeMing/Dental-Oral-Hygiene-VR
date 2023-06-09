using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Toothbrush : MonoBehaviour
{
    public XRGrabInteractable XRGrabInteractable;
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = GetComponent<Transform>().position;
        originalRotation = GetComponent<Transform>().rotation;
        XRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Plague") {
            other.gameObject.GetComponent<Plague>().ReduceHealth();
        }  
    }

    public void ResetPosition() {
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
    }

    public void ChangeTransform(Transform newTransform)
    {
        XRGrabInteractable.attachTransform.position = newTransform.position;
        XRGrabInteractable.attachTransform.rotation = newTransform.rotation;
    }
}
