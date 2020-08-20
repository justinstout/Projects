using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Start is called before the first frame update
    private static float PaddleSpeed = 300.0f;
    private Rigidbody2D Rigidbody;
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        
    }
    void FixedUpdate(){
        float direction = Input.GetAxisRaw("Horizontal");
        Rigidbody.velocity = Vector2.right * direction * PaddleSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
