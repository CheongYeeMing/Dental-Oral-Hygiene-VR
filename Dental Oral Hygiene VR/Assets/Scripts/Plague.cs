using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plague : MonoBehaviour
{
    private Material material;
    private int health;

    private Awake() {
        health = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        print(material);   
    }

    public void ReduceHealth(int amount) {
        health -= amount
        if (health <= 0) {
            RemovePlague()
        }
    }

    private void RemovePlague() {
        // Deactivate Box Collider and Sprite Renderer
    }
}
