using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class barricadeDecreaser : MonoBehaviour
{
    
    public GameObject b;

    public GameObject PromptBox;
    public Text MessagePrompt;
    private Transform theChild;

    public void Repair(){

        b.GetComponent<barricade>().Repair();
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player"  ){
            theChild = this.transform.Find("barricade1"); 
            //Debug.Log("**** theChild: " + theChild );
            //Debug.Log("**** theChild.gameObject.activeSelf : " + theChild.gameObject.activeSelf );

            if( !theChild.gameObject.activeSelf ){ 
                PromptBox.SetActive(true);
                MessagePrompt.text = String.Format("Repair Barricade: Press R to Repair");
        
                if( Input.GetKeyDown("r") ){  //Input.GetKeyDown(KeyCode.O) ){ 
                    Debug.Log("**** Pressed R. Repaired Barricade  ");
                    Repair();
                }
            }
        }        
    }



        void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.tag == "Player")
        {
                PromptBox.SetActive(false);
        }

    }

    

    
}

