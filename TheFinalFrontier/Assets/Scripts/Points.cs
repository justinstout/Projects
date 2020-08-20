using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private static Points _instance;
    public static int Score;
    private HUD HUD;
    //private CreateEnemy createEnemy;
    //public GameObject createEnemyGameObject;


    public static Points Instance {
        get {
            return _instance;
        }
    }

    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
        HUD = HUD.Instance;
        //createEnemyGameObject = GameObject.FindGameObjectWithTag("EmptyEnemy");
        //createEnemy = createEnemyGameObject.GetComponent<CreateEnemy>();
        Score = 0;
    }
    
    public void EnemyKilled(int scoreChange) {
        Score += scoreChange;
        InteractPlayer.points += scoreChange;
    }

    public void DoorOpened(int scoreChange) {
        InteractPlayer.points -= scoreChange;
    }

    // Update is called once per frame
    void Update()
    {
        if(HUD == null){
            HUD = HUD.Instance;
        }

        HUD.Render(Score, InteractPlayer.points, CreateEnemy.Wave);
    }
}
