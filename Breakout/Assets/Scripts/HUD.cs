using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class HUD : MonoBehaviour
{
    private static HUD _instance;
    public Text ScoreText;
    public Text LevelText;

    public static HUD Instance{

        get{
        return _instance;

        }
    }
    void Awake(){
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        } else {
            Destroy(this);
        }
    }
    public void Render(int score, int level, int extraLifeCount){
        ScoreText.text = String.Format("Score: {0}", score);
        LevelText.text = String.Format("Level: {0}", level);

        Transform lives = transform.Find("Lives");
        int i;
        for (i = 0; i < extraLifeCount; i++){
            lives.GetChild(i).gameObject.SetActive(true);
        }
        for (int j = i; j < lives.childCount; j++){
            lives.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
