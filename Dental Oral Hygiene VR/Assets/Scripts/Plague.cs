using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plague : MonoBehaviour
{
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = object.GetComponent<MeshRenderer>();
        debug.log(material);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
