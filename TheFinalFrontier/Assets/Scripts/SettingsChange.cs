using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class SettingsChange : MonoBehaviour
{

    //public Slider slider1;
    public Toggle toggle1;
    public void ToggleDifficulty(bool value)
    {
        Debug.Log("clicked toggle");
    }


        void OnMouseEnter() {
        //Debug.Log("Entered on Toggle");
        //if(ToggleDifficulty(true))

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if(toggle1.isOn)
        //    Debug.Log("toggle1.isOn");
        //    else Debug.Log("off");
        
        //Debug.Log("toggle1.onValueChanged " + toggle1.onValueChanged.ToString());


    }
}
