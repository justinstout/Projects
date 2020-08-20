using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    //public AudioSource GameMusic;
    //public AudioSource GunSound1;
    //public AudioSource EnemySound1;

    //public static SoundController SC;
    private Scene CurrentScene;

    public bool sfxON = true;

    private static SoundController instance = null;
    public static SoundController Instance
    {
        get { return instance;}
    }


    public void funcSfx(bool isOn)
    {
        if(isOn == true)
            sfxON = true;
        else
            sfxON = false;
    }


    void Awake(){
        
        CurrentScene = SceneManager.GetActiveScene();

        if(instance != null && instance != this)
        {
            if (CurrentScene.name == "MainMenu")
            {
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
                //instance.gameObject.SetActive(false);

            }
            //Destroy(this.gameObject);
            return;
        } 
        

        else
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }




        DontDestroyOnLoad(this.gameObject);
    }


        //SoundController.Instance.gameObject.GetComponent<AudioSource>().Pause();



}
