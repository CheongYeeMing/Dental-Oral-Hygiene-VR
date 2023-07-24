using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform parent;
    private void Start()
    {
        parent.localPosition = new Vector3(25.028f, 10.702f, -25.074f);
        parent.localRotation = Quaternion.Euler(new Vector3(0f, 135f, 0f));
    }
}
