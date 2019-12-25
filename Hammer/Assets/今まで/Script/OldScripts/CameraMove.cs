using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform pivot = null;
    void Start()
    {
        if (pivot == null)
            pivot = transform.parent;
    }
    void Update()
    {
        pivot.eulerAngles = new Vector3(pivot.eulerAngles.x, pivot.eulerAngles.y, 0f);
    }
}