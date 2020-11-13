using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrog : MonoBehaviour
{

    private Collider2D componentCollider;
    private bool dragging;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        dragging = false;
        mainCamera = Camera.main;
        componentCollider = GetComponent<Collider2D>();   
    }

    public void OnMouseUp()
    {
        dragging = false;
    }

    public void OnMouseDown()
    {
        dragging = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.dragging)
        {
            Vector3 tempVec = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            tempVec.z = 0;
            componentCollider.transform.position = tempVec;
        }
    }
}
