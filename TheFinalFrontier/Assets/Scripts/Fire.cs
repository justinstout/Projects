using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

  

    
    private RaycastHit2D objectShot;
    // Start is called before the first frame update
    void Start()
    {
        objectShot = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.zero));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RaycastHit2D GetObjectShot() {
        return objectShot;
    }

    public bool HitEnemy() {
        if (objectShot.collider != null) {
            var distance = objectShot.distance;
            if (objectShot.transform.tag == "Enemy" && distance < 1.0) {
                return true;
            }
        }
        return false;
    }
    

}
