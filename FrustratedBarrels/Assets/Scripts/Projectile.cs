using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private float ProjectileSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.position + Camera.main.transform.forward*2.0f;
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = Camera.main.transform.forward*ProjectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
