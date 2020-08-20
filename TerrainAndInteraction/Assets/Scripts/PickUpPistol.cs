using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUpPistol : MonoBehaviour
{
    public float Distance;
    public GameObject CommandKey;
    private Text CommandKeyText;
    public GameObject Command;
    private Text CommandText;
    public GameObject WorldGun;
    public GameObject PlayerGun;
    public GameObject Crosshair;
    void Awake()
    {
        CommandKeyText = CommandKey.GetComponent<Text>();
        CommandText = Command.GetComponent<Text>();
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
        if (Distance < 2)
        {
            CommandKeyText.text = "[e]";
            CommandText.text = "Pick Up Pistol";
            CommandKey.SetActive(true);
            Command.SetActive(true);
            if (Input.GetButtonDown("Action"))
            {
                WorldGun.SetActive(false);
                PlayerGun.SetActive(true);
                CommandKey.SetActive(false);
                Crosshair.SetActive(true);
                
                Command.SetActive(false);
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

