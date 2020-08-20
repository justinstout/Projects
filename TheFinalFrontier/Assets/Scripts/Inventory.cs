using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[9];
    public bool[] isFull;
    public GameObject Weapon1Collected;
    public GameObject Weapon2Collected;
    public GameObject Weapon3Collected;
    public GameObject Weapon4Collected;
    public GameObject Weapon5Collected;
    public GameObject Weapon6Collected;
    public GameObject Weapon7Collected;
    public GameObject Weapon8Collected;
    public GameObject Weapon9Collected;
    public GameObject WeaponInventory;
    //public GameObject chosenTab;
    private Transform[] listOfComponentsInChildren = new Transform[10];

    public TabGroup theTabGroup;



    public void AddItem(GameObject item)
    {
 
        //bool itemAdded = false;
        bool itemAlreadyInInventory = false;


        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] != null)
            if(inventory[i].name == item.name)
            {
                itemAlreadyInInventory = true;
                 break;
            }
        }
                if(itemAlreadyInInventory == false)
                {
                    //Debug.Log("--- weaponNumber: " + item.name.Substring(6,1));
                    var weaponNumber = System.Int32.Parse( item.name.Substring(6,1) );
                    //Debug.Log("--- weaponNumber: " + weaponNumber);
                    
                    inventory[weaponNumber - 1] = inventory[0].GetComponent<Gun>().inventoryHere[weaponNumber - 1];

                    //inventory[weaponNumber - 1] = item;
                    
                    Debug.Log("--- " + item.name + " was added to inventory");
                    //item.SendMessage("DoInteraction");
                    item.SetActive(false);
                    //itemAdded = true;

                    Instantiate(item, inventory[weaponNumber - 1].transform, false);
                    //Instantiate( inventory[0].GetComponent<Gun>().inventoryHere[weaponNumber - 1] , inventory[weaponNumber - 1].transform, false);


                    if( weaponNumber == 2 )
                        Weapon2Collected.SetActive(true);
                    if( weaponNumber == 3 )
                        Weapon3Collected.SetActive(true);
                    if( weaponNumber == 4 )
                        Weapon4Collected.SetActive(true);
                    if( weaponNumber == 5 )
                        Weapon5Collected.SetActive(true);
                    if( weaponNumber == 6 )
                        Weapon6Collected.SetActive(true);
                    if( weaponNumber == 7 )
                        Weapon7Collected.SetActive(true);
                    if( weaponNumber == 8 )
                        Weapon8Collected.SetActive(true);
                    if( weaponNumber == 9 )
                        Weapon9Collected.SetActive(true);
                }
            

    }



    //public GameObject AmmoCrate;
    //public GameObject AmmoCrateText;
/*
    public void haveAmmoCrate()
    {
        AmmoCrate.SetActive(true);
        AmmoCrateText.SetActive(true);
        Vector2 spawnPoint = new Vector2(-6.53f, -0.05f);
        Instantiate(AmmoCrate, spawnPoint, Quaternion.identity);
    }
    */

    public int weaponSelected = 0;


    // Start is called before the first frame update
    void Start()
    {
        weaponSelected = 0;
        SelectWeapon();
        Weapon1Collected.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {

        //WeaponSwitch( );

        
        int previousWeaponSelected = weaponSelected;

        if(Input.GetKeyDown(KeyCode.Alpha1) || weaponSelected == 0 )
        {
            weaponSelected = 0;
            GameObject tab = WeaponInventory.transform.Find("WeaponTab1").gameObject;
            GameObject chosenResult;
            GameObject idleTab;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen1").gameObject;
                chosenResult.SetActive(true);
                //idleTab = tab.transform.Find("Chosen1").gameObject;
                //idleTab.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && inventory[2 - 1] != null)
        {
            weaponSelected = 1;
            Weapon2Collected.SetActive(true);
            GameObject tab = WeaponInventory.transform.Find("WeaponTab2").gameObject;
            GameObject chosenResult;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen2").gameObject;
                chosenResult.SetActive(true);
            }
            //listOfComponentsInChildren = WeaponInventory.GetComponentsInChildren<Transform>(true);
            //WeaponInventory.GetComponentInChildren<GameObject>();
        }


        if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && inventory[3 - 1] != null)
        {
            weaponSelected = 2;
            Weapon3Collected.SetActive(true);
            GameObject tab = WeaponInventory.transform.Find("WeaponTab3").gameObject;
            GameObject chosenResult;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen3").gameObject;
                chosenResult.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4 && inventory[4 - 1] != null)
        {
            weaponSelected = 3;
            Weapon4Collected.SetActive(true);
            GameObject tab = WeaponInventory.transform.Find("WeaponTab4").gameObject;
            GameObject chosenResult;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen4").gameObject;
                chosenResult.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5 && inventory[5 - 1] != null)
        {
            weaponSelected = 4;
            Weapon5Collected.SetActive(true);
            GameObject tab = WeaponInventory.transform.Find("WeaponTab5").gameObject;
            GameObject chosenResult;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen5").gameObject;
                chosenResult.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6 && inventory[6 - 1] != null)
        {
            weaponSelected = 5;
            Weapon6Collected.SetActive(true);
            GameObject tab = WeaponInventory.transform.Find("WeaponTab6").gameObject;
            GameObject chosenResult;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen6").gameObject;
                chosenResult.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7 && inventory[7 - 1] != null)
        {
            weaponSelected = 6;
            Weapon7Collected.SetActive(true);
            GameObject tab = WeaponInventory.transform.Find("WeaponTab7").gameObject;
            GameObject chosenResult;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen7").gameObject;
                chosenResult.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha8) && transform.childCount >= 8 && inventory[8 - 1] != null)
        {
            weaponSelected = 7;
            Weapon8Collected.SetActive(true);
            GameObject tab = WeaponInventory.transform.Find("WeaponTab8").gameObject;
            GameObject chosenResult;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen8").gameObject;
                chosenResult.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha9) && transform.childCount >= 9 && inventory[9 - 1] != null)
        {
            weaponSelected = 8;
            Weapon9Collected.SetActive(true);
            GameObject tab = WeaponInventory.transform.Find("WeaponTab9").gameObject;
            GameObject chosenResult;
            theTabGroup.ResetTabs2();
            if(tab != null){
                chosenResult = tab.transform.Find("Chosen9").gameObject;
                chosenResult.SetActive(true);
            }
        }


        if(previousWeaponSelected != weaponSelected)
        {
            SelectWeapon();
        }
        

    }




    public void WeaponSwitch( int tabNumber )
    {
       int previousWeaponSelected = weaponSelected;
       Debug.Log("--tabNumber : " + tabNumber ); 


        if( tabNumber == 1 )
        {
            weaponSelected = 0;
            Weapon1Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 1 ");  
            //ObjectToPurchase1Collected.SetActive(false);
            //Object1Button.SetActive(false);
        }
        if( ( tabNumber == 2 || Input.GetKeyDown(KeyCode.Alpha2) )  && transform.childCount >= 2 && inventory[2 - 1] != null)
        {
            weaponSelected = 1;
            Weapon2Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 2 ");
        }
        if( tabNumber == 3 && transform.childCount >= 3 && inventory[3 - 1] != null)
        {
            weaponSelected = 2;
            Weapon3Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 3 ");
        }
        if( tabNumber == 4 && transform.childCount >= 4 && inventory[4 - 1] != null)
        {
            weaponSelected = 3;
            Weapon4Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 4 ");
        }
        if( tabNumber == 5 && transform.childCount >= 5 && inventory[5 - 1] != null)
        {
            weaponSelected = 4;
            Weapon5Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 5 ");
        }

        if( tabNumber == 6 && transform.childCount >= 6 && inventory[6 - 1] != null)
        {
            weaponSelected = 5;
            Weapon6Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 6 ");
        }
        if( tabNumber == 7 && transform.childCount >= 7 && inventory[7 - 1] != null)
        {
            weaponSelected = 6;
            Weapon7Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 7 ");
        }
        if( tabNumber == 8 && transform.childCount >= 8 && inventory[8 - 1] != null)
        {
            weaponSelected = 7;
            Weapon8Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 8 ");
        }
        if( tabNumber == 9 && transform.childCount >= 9 && inventory[9 - 1] != null)
        {
            weaponSelected = 8;
            Weapon9Collected.SetActive(true);
            Debug.Log("--Inside WeaponSwitch tabNumber == 9 ");
        }


        if(previousWeaponSelected != weaponSelected)
        {
            SelectWeapon();
        }

    }






    void SelectWeapon()
    {
        int index = 0;
        foreach (Transform weapon in transform)
        {
            if(index == weaponSelected)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            index++;
        }
    }

}




    
