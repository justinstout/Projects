using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class InteractPlayer : MonoBehaviour
{
 
    public GameObject currentObject = null;
    public InteractionObject currentObjectScript = null;
    public Inventory inventory;
    public Gun gun;
    Player thePlayer;
    public GameObject AmmoCrate;
    

    //public GameObject AmmoCrateText;

    public GameObject ObjectToPurchase1;
    public GameObject ObjectToPurchase2;
    public GameObject ObjectToPurchase3;

    private GameObject[] itemsPurchased = new GameObject[3];

    //public GameObject ObjectsPurchased;
    public Text ObjectsPurchasedText;


    public GameObject PromptBox;
    public Text MessagePrompt;

    public GameObject ObjectToPurchase1Collected;
    public GameObject ObjectToPurchase2Collected;
    public GameObject ObjectToPurchase3Collected;

    public GameObject Object1Button;
    public GameObject Object2Button;
    public GameObject Object3Button;


    public static int points;

    void Awake() {
        points = 400;
    }

    void Update()
    {
        if( itemsPurchased[0] != null ){            
            //ObjectsPurchased.SetActive(true);
            //ObjectsPurchasedText.SetActive(true);
            ObjectsPurchasedText.text = String.Format(" {0} ", itemsPurchased[0].name );
        }
        if( itemsPurchased[1] != null )        
            ObjectsPurchasedText.text = String.Format(" {0} ", itemsPurchased[1].name );
        if( itemsPurchased[2] != null )        
            ObjectsPurchasedText.text = String.Format(" {0} ", itemsPurchased[2].name ); 

        if( itemsPurchased[0] != null && itemsPurchased[1] != null )        
            ObjectsPurchasedText.text = String.Format(" {0} ", itemsPurchased[0].name + " & " + itemsPurchased[1].name );
        if(itemsPurchased[0] != null && itemsPurchased[2] != null )        
            ObjectsPurchasedText.text = String.Format(" {0} ", itemsPurchased[0].name + " & " + itemsPurchased[2].name );       
        if(itemsPurchased[1] != null && itemsPurchased[2] != null )        
            ObjectsPurchasedText.text = String.Format(" {0} ", itemsPurchased[1].name + " & " + itemsPurchased[2].name );       
        
        if(itemsPurchased[0] != null && itemsPurchased[1] != null && itemsPurchased[2] != null  )        
            ObjectsPurchasedText.text = String.Format(" {0} ", itemsPurchased[0].name + " & " + itemsPurchased[1].name + " & " + itemsPurchased[2].name );       


        if(currentObject == null && Input.GetKeyDown(KeyCode.P) ){
            Debug.Log("---Items currently in your purchases: " + itemsPurchased[0] + " & " + itemsPurchased[1] + " & " + itemsPurchased[2] );
            Debug.Log("---Current amount of points = " + points );
        }




        if(currentObject) //player's position has met the object and has stored it in this variable
        {

            if(currentObjectScript.ammoCrate)
            {
                inventory.inventory[inventory.weaponSelected].GetComponent<Gun>().AmmoCrateAquired(currentObject);
                //AmmoCrate.SetActive(true);
                //currentObject = null;
                currentObjectScript.ammoCrate = false;

            }
            
    
             if( currentObject.Equals( ObjectToPurchase1 ) && Input.GetKeyDown(KeyCode.P) &&  currentObject.activeSelf && points >= 50 )
            {
                Debug.Log("---PURCHASED ITEM ObjectToPurchase1" );
                itemsPurchased[0] = ObjectToPurchase1;
                points = points - 50;
                Debug.Log("---Purchase -50 points. Current amount of points = " + points );
                Object1Button.SetActive(true);
                ObjectToPurchase1Collected.SetActive(true);
                this.gameObject.GetComponent<Player>().HealthBar.gameObject.GetComponent<CharacterHealth>().MaxHealth = 150;
                this.gameObject.GetComponent<Player>().HealthBar.gameObject.GetComponent<CharacterHealth>().CurrentHealth = 150;

                //ObjectToPurchase1.SetActive(false);
                //PurchasedItemText.SetActive(true);
            }

             if( currentObject.Equals( ObjectToPurchase2 ) && Input.GetKeyDown(KeyCode.P) &&  currentObject.activeSelf && points >= 50 )
            {
                Debug.Log("---PURCHASED ITEM ObjectToPurchase2");
                itemsPurchased[1] = ObjectToPurchase2;
                points = points - 50;
                Debug.Log("---Purchase -50 points. Current amount of points = " + points );
                Object2Button.SetActive(true);
                ObjectToPurchase2Collected.SetActive(true);

                this.gameObject.GetComponent<Player>().UpgradeSprintSpeed();
                this.gameObject.GetComponent<Player>().UpgradeSprintTime();
                this.gameObject.GetComponent<Player>().UpgradeSprintFrequency();

     
                //ObjectToPurchase2.SetActive(false);
                //PurchasedItemText.SetActive(true);
            }

             if( currentObject.Equals( ObjectToPurchase3 ) && Input.GetKeyDown(KeyCode.P) &&  currentObject.activeSelf && points >= 50 )
            {
                Debug.Log("---PURCHASED ITEM ObjectToPurchase3");
                itemsPurchased[2] = ObjectToPurchase3;
                points = points - 50;
                Debug.Log("---Purchase -50 points. Current amount of points = " + points );
                Object3Button.SetActive(true);
                ObjectToPurchase3Collected.SetActive(true);
                //ObjectToPurchase3.SetActive(false);
                //PurchasedItemText.SetActive(true);
            }

            if(currentObjectScript.inventory && Input.GetKeyDown(KeyCode.P) && points >= 50 )
            {
                Debug.Log("---PURCHASED GUN");
                points = points - 50;
                Debug.Log("---Purchase -50 points. Current amount of points = " + points );
                inventory.AddItem(currentObject);
            }

        } // closes if(currentObject)




    }


    public void UseItemPurchased( int itemNumber )
    {
        Debug.Log("--itemNumber : " + itemNumber ); 

        if( itemNumber == 1 && itemsPurchased[0] != null )
        {
            Debug.Log("--Inside UseItemPurchased itemNumber == 1 ");  
            itemsPurchased[0] = null;
            ObjectsPurchasedText.text = String.Format("");
            ObjectToPurchase1Collected.SetActive(false);
            Object1Button.SetActive(false);
        }

         if( itemNumber == 2 && itemsPurchased[1] != null )
        {
            Debug.Log("--Inside UseItemPurchased itemNumber == 2 "); 
            itemsPurchased[1] = null;
            ObjectsPurchasedText.text = String.Format("");
            ObjectToPurchase2Collected.SetActive(false);
            Object2Button.SetActive(false);
            
        }

         if( itemNumber == 3 && itemsPurchased[2] != null )
        {
            Debug.Log("--Inside UseItemPurchased itemNumber == 3 "); 
            itemsPurchased[2] = null;
            ObjectsPurchasedText.text = String.Format("");
            ObjectToPurchase3Collected.SetActive(false);
            Object3Button.SetActive(false);

        }

    }




  void OnTriggerEnter2D(Collider2D other){

            Debug.Log(other.name);
            
            if(other.CompareTag("Ammocrate") ){
                Debug.Log("---GOT AMMO");
                currentObject = other.gameObject;
                currentObjectScript = currentObject.GetComponent<InteractionObject>();           
                //inventory.inventory[inventory.weaponSelected].GetComponent<Gun>().AmmoCrateAquired(currentObject);
            }

            if(other.CompareTag("Weapon") ){
                PromptBox.SetActive(true);
                if( other.name.Equals("Weapon2") )        
                    MessagePrompt.text = String.Format("Weapon 2: Cost:50 Press P to Purchase");
                if( other.name.Equals("Weapon3") )        
                    MessagePrompt.text = String.Format("Weapon 3: Cost:50 Press P to Purchase");
                if( other.name.Equals("Weapon4") )        
                    MessagePrompt.text = String.Format("Weapon 4: Cost:50 Press P to Purchase");
                if( other.name.Equals("Weapon5") )        
                    MessagePrompt.text = String.Format("Weapon 5: Cost:50 Press P to Purchase");
                if( other.name.Equals("Weapon6") )        
                    MessagePrompt.text = String.Format("Weapon 6: Cost:50 Press P to Purchase");
                if( other.name.Equals("Weapon7") )        
                    MessagePrompt.text = String.Format("Weapon 7: Cost:50 Press P to Purchase");
                if( other.name.Equals("Weapon8") )        
                    MessagePrompt.text = String.Format("Weapon 8: Cost:50 Press P to Purchase");

                Debug.Log("---NEAR GUN");
                currentObject = other.gameObject;
                currentObjectScript = currentObject.GetComponent<InteractionObject>();
            }

            if(other.CompareTag("ObjectToPurchase") ){
                PromptBox.SetActive(true);
                if( other.name.Equals("ObjectToPurchase1") )        
                    MessagePrompt.text = String.Format("Object1: Cost:50 Press P to Purchase");
                if( other.name.Equals("ObjectToPurchase2") )        
                    MessagePrompt.text = String.Format("Object2: Cost:50 Press P to Purchase");
                if( other.name.Equals("ObjectToPurchase3") )        
                    MessagePrompt.text = String.Format("Object2: Cost:50 Press P to Purchase");

                Debug.Log("---NEAR Object to Purchase");
                currentObject = other.gameObject;
                currentObjectScript = currentObject.GetComponent<InteractionObject>();
            }
        
  }



    void OnTriggerExit2D(Collider2D other){
         if(other.CompareTag("Weapon") || other.CompareTag("ObjectToPurchase") ){
             if( currentObject == other.gameObject )
                currentObject = null;
                PromptBox.SetActive(false);
         }

    }



}
