using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Door : MonoBehaviour
{

    public GameObject PromptBox;
    public Text MessagePrompt;
    private EnemyFactory EF; 
    public GameObject objectwithfactory;
    public List<int> a;
    public bool alreadyin;
    public int PointValue;

    
    void Start(){
        EF = objectwithfactory.GetComponent<EnemyFactory>();


    }


    void OnCollisionStay2D(Collision2D collision)
    //void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("--- collision.gameObject.name: " + collision.gameObject.name);
        //Debug.Log("--- this.transform.name: " + this.transform.name);
        if(collision.gameObject.tag == "Player"){
            PromptBox.SetActive(true);
            MessagePrompt.text = String.Format("Door: Cost:50 Press o to open");


            if(this.transform.name == "Door"  && Input.GetKeyDown("o") && InteractPlayer.points >= 50 ){  
                //spawnpoint15
                Debug.Log("--- pressed o  Opened Door ");
                
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 15){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(15);
                }
                
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);   
                
            }
            if(this.transform.name == "Door2" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint15
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 15){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(15);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door3" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint11
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 11){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(11);
                }
                //spawnpoint14
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 14){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(14);
                }
                //spawnpoint15
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 15){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(15);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door4"  && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint11
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 11){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(11);
                }
                //spawnpoint14
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 14){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(14);
                }
                //spawnpoint15
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 15){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(15);
                }
                //spawnpoint16
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 16){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(16);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door5"  && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 13
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 13){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(13);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door6"  && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 4
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 4){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(4);
                }
                //spawnpoint 15
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 15){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(15);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door7" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 4
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 4){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(4);
                }
                //spawnpoint 5
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 5){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(5);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door8" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 5
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 5){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(5);
                }
                //spawnpoint 6
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 6){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(6);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door9" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 6
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 6){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(6);
                }
                //spawnpoint 15
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 15){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(15);
                }
                //spawnpoint 16
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 16){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(16);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door10" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 6
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 6){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(6);
                }
                //spawnpoint 7
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 7){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(7);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door11" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 7
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 7){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(7);
                }
                //spawnpoint 9
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 9){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(9);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door12" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 8
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 8){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(8);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door13" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 9
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 9){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(9);
                }
                //spawnpoint 10
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 10){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(10);
                }
                //spawnpoint 16
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 16){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(16);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door14" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 10
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 10){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(10);
                }
                //spawnpoint 16
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 16){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(16);
                }
                //spawnpoint 11
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 11){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(11);
                }
                //spawnpoint 14
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 14){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(14);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door15" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 12
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 12){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(12);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            if(this.transform.name == "Door16" && Input.GetKeyDown("o") && InteractPlayer.points >= 50){ 
                //spawnpoint 12
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 12){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(12);
                }
                //spawnpoint 13
                alreadyin = false;
                a = EF.OpenSpawnPoints;
                for(int i = 0; i < a.Count; i++){
                    if(a[i] == 13){
                        alreadyin = true;
                    }

                }
                if(alreadyin == false){
                    a.Add(13);
                }
                this.gameObject.SetActive(false);
                Points.Instance.DoorOpened(50);
            }
            
            
        }
       // Debug.Log("HEY 2");
    }



    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.tag == "Player")
        {

        //if(collision.CompareTag("Weapon") || other.CompareTag("ObjectToPurchase") ){
            //if( currentObject == other.gameObject )
            //    currentObject = null;
                PromptBox.SetActive(false);
        }

    }
    

    
}
