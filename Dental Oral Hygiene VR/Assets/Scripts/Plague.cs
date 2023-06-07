using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plague : MonoBehaviour
{
    [SerializeField] BrushingProgressBar brushingProgressBar;
    private Material material;
    private int health;

    private void Awake() {
        health = 100;
    }

    // Start is called before the first frame update
    public void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        print(material);   
        
    }

    public void ReduceHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            RemovePlague();
        }
    }

    public void RemovePlague() {
        // Deactivate Box Collider and Sprite Renderer
        gameObject.SetActive(false);
    }
}
