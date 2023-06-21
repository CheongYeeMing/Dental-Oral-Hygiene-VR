using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", 2);
    }

    // Update is called once per frame
    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
