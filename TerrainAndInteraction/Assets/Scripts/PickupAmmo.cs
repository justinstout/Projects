using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupAmmo : MonoBehaviour
{
    public float Distance;
    public GameObject CommandKey;
    private Text CommandKeyText;
    public GameObject Command;
    private Text CommandText;
    public GameObject AmmoCrate;
    public GameObject P;
    private Player play;
    
    public void Initialize(){

    }

    void Awake()
    {
        CommandKeyText = CommandKey.GetComponent<Text>();
        CommandText = Command.GetComponent<Text>();
        play = P.GetComponent<Player>();
    }
    void Update()
    {
        Distance = Player.TargetDistance;
    }
    void OnMouseOver()
    {

        //The OnMouseOver function is used to activiate the HUD (canvas) in order to show we can pickup the pistol
        //when we hover over it.
        //and to not show it otherwise.
        if (Distance < 20)
        {
            CommandKeyText.text = "[e]";
            CommandText.text = "Pick Up Ammo";
            CommandKey.SetActive(true);
            Command.SetActive(true);
            if (Input.GetButtonDown("Action"))
            {
               
                CommandKey.SetActive(false);
               
                
                Command.SetActive(false);
                AmmoCrate.SetActive(false);
                
                P.GetComponent<Player>().increaseAmmo(1);

                
            }
        }
        else
        {
            CommandKey.SetActive(false);
            Command.SetActive(false);
        }
    }       
    void OnMouseExit()
    {
        CommandKey.SetActive(false);
        Command.SetActive(false);
    }
}
