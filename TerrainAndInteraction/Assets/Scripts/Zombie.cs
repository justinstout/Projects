using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public Animator Animator;
    private Player PlayerObject;
    private bool Attacking = false;
    private float Health = 100.0f;
    

    void Awake(){
        Animator = GetComponent<Animator>();
    }
    IEnumerator Attack(){
        Attacking = true;
        Animator.SetTrigger("Attack");
        
        PlayerObject.DecreaseHealth(10.0f);
        yield return new WaitForSeconds(1.2f);
        Attacking = false;
    }

    IEnumerator ZombieDeath(){
        
        
        PlayerObject.DecreaseHealth(10.0f);
        yield return new WaitForSeconds(25.0f);
        Destroy(GetComponent<Zombie>());
    }
    public void DamageZombie(float damage){
        Health -= damage;

        if (Health <= 0.0f)
        {
            Animator.SetTrigger("Death");
            StartCoroutine(ZombieDeath());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out objectHit))
        {
            var distance = objectHit.distance;
            if (objectHit.transform.tag == "Player" && distance < 1.0 && Attacking == false)
            {

               StartCoroutine(Attack());
            }
        }
        var target = PlayerGameObject.transform.position;
        target.y = transform.position.y + (transform.lossyScale.y / 2);
        transform.LookAt(target);
        
    }
    public void Initialize(GameObject player){
       
        PlayerGameObject = player;
        PlayerObject = PlayerGameObject.GetComponent<Player>();
    }
}
