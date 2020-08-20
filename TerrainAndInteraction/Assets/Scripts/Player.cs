using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public static float TargetDistance;
    public float Distance;
    private float Health = 100.0f;
    public AudioSource aud;
    public static float Clips = 1;
    public static float Ammo = 0;
    public Text totalClips;
    public GameObject clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Render(){
        

        

        totalClips.text = "" + Clips;
        int i;
        for (i = 0; i < Ammo; i++){
            clip.transform.GetChild(i).gameObject.SetActive(true);
        
        }
        for (int j = i; j < 5; j++){
            clip.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void increaseAmmo(float a){
        Clips += a;
    }

    public void DecreaseHealth(float HealthLoss){
        Health -= HealthLoss;
        aud.Play();
        if(Health <= 0){
            SceneManager.LoadScene("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Render();
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out objectHit))
        {
            Distance = objectHit.distance;
            TargetDistance = Distance;
        }
    }
}
