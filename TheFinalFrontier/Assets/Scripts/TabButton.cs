using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TabButton : MonoBehaviour
{
    public TabGroup tabGroup;
    public GameObject idleTab;
    public GameObject hoverTab;
    public GameObject chosenTab;

    private Scene CurrentScene;

    public InteractPlayer interactPlayer;
    public Inventory inventory;
    //private GameObject instanceInventory;
    public bool clickOnUI;

    public GameObject ThisButton;

    private CreateEnemy createEnemy;
    private GameObject createEnemyGameObject;

    public static int Difficulty = 2;

    
    public GameObject EasyButtonClicked;
    public GameObject MediumButtonClicked;
    public GameObject HardButtonClicked;
    public GameObject MusicButtonClicked;
    
    public GameObject SFXButtonClicked;

    private TabButton TheClickedTab;

    public AudioSource music; 
    
    /*
    private static TabButton _instance;

    public static TabButton Instance {
        get {
            return _instance;
        }
    }
    */

/*
    void Awake()
    {
        
        if (_instance == null) {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
        

        //createEnemy = CreateEnemy.Instance;
        //createEnemyGameObject = GameObject.FindGameObjectWithTag("EmptyEnemy");
        //createEnemy = createEnemyGameObject.GetComponent<CreateEnemy>();
    }
    */

    void OnMouseDown() {
        clickOnUI = true;
        tabGroup.SelectedTab = this;
        tabGroup.ResetTabs();
        idleTab.SetActive(false);
        chosenTab.SetActive(true);
        int index = this.transform.GetSiblingIndex();
        if (CurrentScene.name == "MainMenu" || CurrentScene.name == "GameOver") {
            
            if( this.transform.parent.name != "OptionsBar" )    // (tabGroup.PagesToSwap[i].tag != "OptionsBar") 
            {

                for (int i = 0; i < tabGroup.PagesToSwap.Count; i++) 
                {
                    if (i == index) 
                    {
                        tabGroup.PagesToSwap[i].SetActive(true);
                        if (tabGroup.PagesToSwap[i].tag == "GoToGame") 
                        {
                            SceneManager.LoadScene("Gameplay");
                        }
                        if (tabGroup.PagesToSwap[i].tag == "GoToMainMenu") 
                        {
                            SceneManager.LoadScene("MainMenu");
                        }
                    } 
                    else
                    { 
                        tabGroup.PagesToSwap[i].SetActive(false);
                    }
                }


            }


        //int v = this.transform.GetSiblingIndex();
        if ( this.transform.parent.name == "OptionsBar" ) {
            tabGroup.SettingsButtons[0].SetActive(true);
            tabGroup.SettingsButtons[1].SetActive(true);
            tabGroup.SettingsButtons[2].SetActive(true);
            tabGroup.SettingsButtons[3].SetActive(true);
            tabGroup.SettingsButtons[4].SetActive(true);
        }

        //if(TheClickedTab.tabGroup.tag == "OptionsBar")
        if(TheClickedTab != null)
        {    
            Debug.Log("--- OnMouseDown() TheClickedTab!=null:  TheClickedTab : " + TheClickedTab); 
            TheClickedTab.ThisButton.SetActive(true);
        }
        

    

        }


        if (CurrentScene.name == "MainMenu" )
        {
            Debug.Log("---OnMouseDown tabGroup.SelectedTab.name  : " + tabGroup.SelectedTab.name); 

            if( this.transform.name.Equals("Easy") &&  !EasyButtonClicked.activeSelf) 
            {
                TheClickedTab = tabGroup.SelectedTab;
                EasyButtonClicked.SetActive(true);
                Debug.Log("---Pressed Button Easy was Not active");    
                // call a function to set the appropriate amount of enemies
                Difficulty = 1;
                //createEnemy.DifficultySetByUser(1);
                MediumButtonClicked.SetActive(false);
                HardButtonClicked.SetActive(false);
            }
            else if( this.transform.name.Equals("Easy") &&  EasyButtonClicked.activeSelf)            
            {
                EasyButtonClicked.SetActive(false);
                Debug.Log("---Pressed Easy Button was Active");    
            }


            if( this.transform.name.Equals("Medium") && !MediumButtonClicked.activeSelf)
            {
                TheClickedTab = tabGroup.SelectedTab;
                MediumButtonClicked.SetActive(true);
                Debug.Log("---Pressed Button Medium was Not active");    
                // call a function to set the appropriate amount of enemies
                Difficulty = 2;
                //createEnemy.DifficultySetByUser(2);
                EasyButtonClicked.SetActive(false);
                HardButtonClicked.SetActive(false);
            }
            else if( this.transform.name.Equals("Medium") &&  MediumButtonClicked.activeSelf)            
            {
                MediumButtonClicked.SetActive(false);
                Debug.Log("---Pressed Medium Button was Active");    
            }


            if( this.transform.name.Equals("Hard") && !HardButtonClicked.activeSelf)
            {
                TheClickedTab = tabGroup.SelectedTab;
                HardButtonClicked.SetActive(true);
                Debug.Log("---Pressed Button Hard was Not active"); 
                // call a function to set the appropriate amount of enemies
                Difficulty = 3;
                //createEnemy.DifficultySetByUser(3);
                MediumButtonClicked.SetActive(false);
                EasyButtonClicked.SetActive(false);
            }
             else if( this.transform.name.Equals("Hard") &&  HardButtonClicked.activeSelf)            
            {
                HardButtonClicked.SetActive(false);
                Debug.Log("---Pressed Hard Button was Active");    
            }




            if( this.transform.name.Equals("Music") &&  !MusicButtonClicked.activeSelf)
            {
                TheClickedTab = tabGroup.SelectedTab;
                MusicButtonClicked.SetActive(true);
                music.mute = true;
                Debug.Log("---Pressed Music Button was Not active");     
                // call a function to set mute music
            }
            else if( this.transform.name.Equals("Music") &&  MusicButtonClicked.activeSelf)            
            {
                MusicButtonClicked.SetActive(false);
                music.mute = false;
                Debug.Log("---Pressed Music Button was Active");    
                // call a function to set mute SFX
            }



            if( this.transform.name.Equals("SFX") &&  !SFXButtonClicked.activeSelf)
            //if( tabGroup.SelectedTab.name.Equals("SFX") &&  !SFXButtonClicked.activeSelf)
            {
                SFXButtonClicked.SetActive(true);
                TheClickedTab = tabGroup.SelectedTab;
                Debug.Log("---Pressed Button SFX was Not active");    
                // call a function to set mute SFX
            }
            else if( this.transform.name.Equals("SFX") &&  SFXButtonClicked.activeSelf)            
            //else if( tabGroup.SelectedTab.name.Equals("SFX") &&  SFXButtonClicked.activeSelf)
            {
                SFXButtonClicked.SetActive(false);
                Debug.Log("---Pressed Button SFX was Active");    
                // call a function to set mute SFX
            }
            

        }




        if( tabGroup.SelectedTab.name.Equals("Object1Button") )
        {
            clickOnUI = true;
            Debug.Log("--Pressed Button 1");    
            interactPlayer.GetComponent<InteractPlayer>().UseItemPurchased(1);   
        }

        if( tabGroup.SelectedTab.name.Equals("Object2Button") )
        {
            clickOnUI = true;
            Debug.Log("--Pressed Button 2"); 
            interactPlayer.GetComponent<InteractPlayer>().UseItemPurchased(2);  
        }

        if( tabGroup.SelectedTab.name.Equals("Object3Button") )
        {
            clickOnUI = true;
            Debug.Log("--Pressed Button 3"); 
            interactPlayer.GetComponent<InteractPlayer>().UseItemPurchased(3);  
        }





        if( tabGroup.SelectedTab.name.Equals("WeaponTab1") )
        {
            Debug.Log("--Pressed WeaponTab1");    
            //interactPlayer.GetComponent<InteractPlayer>().DisplayInventoryUI(1); 
            inventory.WeaponSwitch(1);  
        }
        if( tabGroup.SelectedTab.name.Equals("WeaponTab2") )
        {
            Debug.Log("--Pressed WeaponTab2"); 
            //interactPlayer.GetComponent<InteractPlayer>().DisplayInventoryUI(2);  
            inventory.WeaponSwitch(2);
        }
        
        if( tabGroup.SelectedTab.name.Equals("WeaponTab3") )
        {
            Debug.Log("--Pressed WeaponTab3");    
            inventory.WeaponSwitch(3);  
        }
        if( tabGroup.SelectedTab.name.Equals("WeaponTab4") )
        {
            Debug.Log("--Pressed WeaponTab4"); 
            inventory.WeaponSwitch(4);
            //instanceInventory = inventory.GetComponent<Inventory>().inventory[3];
            //instanceInventory.GetComponent<Inventory>().WeaponSwitch(4);
            //inventory.GetComponent<Inventory>().WeaponSwitch(4);
        }
        if( tabGroup.SelectedTab.name.Equals("WeaponTab5") )
        {
            Debug.Log("--Pressed WeaponTab5");    
            inventory.WeaponSwitch(5);  
        }
        if( tabGroup.SelectedTab.name.Equals("WeaponTab6") )
        {
            Debug.Log("--Pressed WeaponTab6"); 
            inventory.WeaponSwitch(6);
        }
        if( tabGroup.SelectedTab.name.Equals("WeaponTab7") )
        {
            Debug.Log("--Pressed WeaponTab7");    
            inventory.WeaponSwitch(7);  
        }
        if( tabGroup.SelectedTab.name.Equals("WeaponTab8") )
        {
            Debug.Log("--Pressed WeaponTab8"); 
            inventory.WeaponSwitch(8);
        }
        if( tabGroup.SelectedTab.name.Equals("WeaponTab9") )
        {
            Debug.Log("--Pressed WeaponTab9");    
            inventory.WeaponSwitch(9);  
        }
        




    }

    void OnMouseEnter() {
        Debug.Log("TheClickedTab : " + TheClickedTab);
        Debug.Log("Entered: this : " + this);
        if( this.ThisButton != TheClickedTab   ) //SFXButtonClicked.activeSelf
        {
            tabGroup.ResetTabs();
            if (tabGroup.SelectedTab == null || this != tabGroup.SelectedTab) {
                idleTab.SetActive(false);
                hoverTab.SetActive(true);
            }
        }   
    }

    void OnMouseExit() {
        Debug.Log("TheClickedTab : " + TheClickedTab);
        Debug.Log("exited: this : " + this);
        if( this.ThisButton != TheClickedTab   ) //SFXButtonClicked.activeSelf
        {
            tabGroup.ResetTabs();
            clickOnUI = false;

            if(ThisButton != null)
            {
                ThisButton.SetActive(true);
                hoverTab.SetActive(true);
                tabGroup.ResetTabs2();
                Debug.Log("OnMouseExit : ThisButton != null");

            }
        }
    }

    void Start()
    {
        clickOnUI = false;
        tabGroup.Subscribe(this);
        tabGroup.ResetTabs();
        CurrentScene = SceneManager.GetActiveScene();


        if(ThisButton != null)
        {
            ThisButton.SetActive(true);
            hoverTab.SetActive(true);
            tabGroup.ResetTabs2();
            //Debug.Log("Start : ThisButton != null");

        }
    }
}
