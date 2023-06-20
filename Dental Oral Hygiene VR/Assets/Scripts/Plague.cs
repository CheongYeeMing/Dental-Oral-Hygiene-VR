using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plague : MonoBehaviour
{
    private const float BRUSH_DELAY = 1;

    [SerializeField] BrushingProgressBar brushingProgressBar;
    
    private Material material;
    private int health;
    private float brushTimer;

    private void Awake() {
        health = 1;
    }

    // Start is called before the first frame update
    public void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        print(material);   
        
    }

    public void Update() 
    {
        brushTimer += Time.deltaTime;
    }

    public void ReduceHealth() {
        if (brushTimer < BRUSH_DELAY) 
        {
            return;
        }
        brushTimer = 0;
        health -= 1;
        if (health <= 0) {
            RemovePlague();
        }
    }

    public void RemovePlague() {
        // Deactivate Box Collider and Sprite Renderer
        gameObject.SetActive(false);
    }
}
