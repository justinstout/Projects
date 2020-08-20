using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    private Animator Animator;
    private bool Firing = false;
    public GameObject Barrel;
    public GameObject MuzzleFlash;
    public AudioSource PistolShot;
    public AudioSource PistolEmpty;
    public AudioSource Reload;

    private float Damage = 25.0f;

    void Awake(){
        Animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator FirePistol(){
    Firing = true;
    RaycastHit objectShot;
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out objectShot))
    {
        objectShot.transform.SendMessage("DamageZombie", Damage, SendMessageOptions.DontRequireReceiver);
    }
    
    Animator.SetTrigger("Fire");
    PistolShot.Play();
    var flash = Instantiate(MuzzleFlash, Barrel.transform.position, Barrel.transform.rotation);
    yield return new WaitForSeconds(1.0f);
    Destroy(flash);
    Firing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            if(Player.Ammo > 0){
                Player.Ammo -= 1;
                StartCoroutine(FirePistol());
            } else {
            }
        }
        if(Input.GetButtonDown("Reload")){
            if(Player.Clips > 0){
            Reload.Play();
            Player.Ammo = 5;
            Player.Clips -= 1;
            }
            
        }
    }
}
