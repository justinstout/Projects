using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{
    public GameObject Box;
    public GameObject HoldBox;
    private BoxCollider b;
    private Rigidbody rb;

    void Awake(){
        b = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }
    
    public void OnMouseDown(){
        b.enabled = false;
        rb.useGravity = false;
        transform.position = HoldBox.transform.position;
        transform.parent = HoldBox.transform;
    }
    public void onMouseUp(){
        transform.parent = null;
        b.enabled = true;
        rb.useGravity = true;
    }
}
