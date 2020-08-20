using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class FollowMouse : NetworkBehaviour
{
    private float MoveSpeed = 100.0f;
    
    [SerializeField]
    private Rigidbody2D Rigidbody;


    void Awake()
    {
        Rigidbody = this.GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
        Rigidbody.velocity = direction * MoveSpeed;
    }
    
}
