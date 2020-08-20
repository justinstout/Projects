using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barricade : MonoBehaviour
{
    bool Attacking = false;
    public int Health = 100;
    int maxHealth = 100;
    public GameObject b;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake(){
        Attacking = false;
    }

    public void Repair(){

        b.SetActive(true);
        Health = maxHealth;
    }



    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator Attack(){
        Attacking = true;
        Health -= 10;
        Debug.Log(Health);
        if(Health <= 0){
           Attacking = false;
           b.SetActive(false);
       }
        yield return new WaitForSeconds(.5f);
        Attacking = false;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            //Repair();
            //Debug.Log("**** DId repair here ");

        }
        if (collision.gameObject.tag == "Enemy")
        {
            if(Attacking == false){
                StartCoroutine(Attack());
            }

            //If the GameObject has the same tag as specified, output this message in the console
            //Debug.Log(Health);
        }
        Debug.Log("*** OnCollisionEnter2D");
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(Attacking == false){
                StartCoroutine(Attack());
            }

            //If the GameObject has the same tag as specified, output this message in the console
            //Debug.Log(Health);
        }
        //Debug.Log("*** OnCollisionStay2D");
    }


    
}
