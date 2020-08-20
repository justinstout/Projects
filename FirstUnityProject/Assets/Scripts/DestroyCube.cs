using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnControllerColliderHit(ControllerColliderHit hit){
        var gameObject = hit.collider.gameObject;
        var label = gameObject.tag;

        if(label == "SmallCube"){
            print("Collision with " + label);
            Destroy(gameObject);
        }
    }
}
