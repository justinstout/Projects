using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{

    public GameObject Ball;
    public GameObject Projectile;
    private float FireRate = 2.0f;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0.0f;
        Ball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // Rotate launcher relative to the FPS camera
        print(Camera.main.transform.forward);
        transform.position = Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward,Camera.main.transform.up);
        // Rotate the Ball relative to the FPS camera and extended from the launcher
        Ball.transform.position = transform.position + transform.forward*2.0f;

        if (Input.GetButton("Fire1") && Timer >= FireRate){
            var projectileGameObject = Object.Instantiate(Projectile, Ball.transform.position,Quaternion.identity);
            
            Ball.SetActive(false);
            Timer = 0.0f;
        }
            
        if (Timer >= FireRate){
            Ball.SetActive(true);
        }

        Timer += Time.deltaTime;
    }
}
