using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Toothbrush : MonoBehaviour
{
    [SerializeField] private ParticleSystem foam;
    [SerializeField] private GameObject toothbrushHead;
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
            FindObjectOfType<AudioManager>().StopEffect("brush");
            FindObjectOfType<AudioManager>().PlayEffect("brush");
            other.gameObject.GetComponent<Plague>().ReduceHealth();
            Instantiate(foam, toothbrushHead.transform.position, transform.rotation);
        }  
    }

    public void ResetPosition() {
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
    }

    public void ChangeTransform(Transform newTransform)
    {
        XRGrabInteractable.attachTransform.position = newTransform.position;
        XRGrabInteractable.attachTransform.rotation = newTransform.rotation;
    }
}
