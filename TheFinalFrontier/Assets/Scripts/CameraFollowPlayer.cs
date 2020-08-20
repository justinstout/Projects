using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - PlayerTransform.position;
        //SoundController.Instance.gameObject.GetComponent<AudioSource>().Pause();

    }

    // LateUpdate is called after Update methods
    // include this method so that other objects can be dragged that 
    // might have been moved inside the update() method 
    void LateUpdate() 
    {
        Vector3 newPosition = PlayerTransform.position + cameraOffset;

        // Here the camera's new position is calculated, but not directly
        // in order to have a smooth camera movement, to achieve this, use the Slerp 
        // method to interpolate between the current camera position with the new position
        transform.position = Vector3.Slerp(transform.position, newPosition, SmoothFactor);
   
        if(LookAtPlayer){
            transform.LookAt(PlayerTransform);
        }
  
    }

    // Update is called once per frame
     void Update()
    {

    }
}
