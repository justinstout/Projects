using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    private EnemyFactory Factory;
    public GameObject Player;
    private float SpawnTime = 1.5f;
    public float Timer;
    public int EnemyPoints = 10;
    public static int Wave;
    public int EnemyCount;
    public int type;
    
    public GameObject AmmoCrate;
    public Transform PlayerTransform;

    public TabButton TabButton;

    public int intDifficultySetByUser;

    //private static CreateEnemy _instance;

/*    public static CreateEnemy Instance {
        get {
            return _instance;
        }
    }
    */


    void Awake()
    {
        //TabButton = TabButton.Instance;
        //intDifficultySetByUser = TabButton.GetComponent<TabButton>().;
        Wave = 1;
        intDifficultySetByUser = TabButton.Difficulty;
        Debug.Log("In Awake CreateEnemy.  intDifficultySetByUser = " + intDifficultySetByUser );

    /*
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
    */


    }

    // Start is called before the first frame update
    void Start()
    {
        Factory = EnemyFactory.Instance;
        //Factory.CreateEnemyRandom(1);
        Timer = 0;

        if(intDifficultySetByUser == 1)
                SpawnTime = 4;
        if(intDifficultySetByUser == 2)
            SpawnTime = 1.5f;
        if(intDifficultySetByUser == 3)
            SpawnTime = 0.75f;
        Debug.Log("In Start. CreateEnemy. After setting SpawnTime.  SpawnTime = " + SpawnTime );


    }


    public void DifficultySetByUser(int difficulty)
        {
            if(difficulty == 1)
                SpawnTime = 4;
            if(difficulty == 2)
                SpawnTime = 1.5f;
            if(difficulty == 3)
                SpawnTime = 0.75f;

        }

    // Update is called once per frame
    void Update()
    {
        //if(TabButton == null){
          //  TabButton = TabButton.Instance; }


        var random = new System.Random();
        Timer += Time.deltaTime;
        if (EnemyCount == 0 && EnemyPoints == 0) {
            Wave += 1;
            EnemyPoints = Wave * 10;
            EnemyCount = 0;


        } else if (EnemyPoints > 0 || EnemyCount == 0) {
            if(Timer > SpawnTime){
                double R = random.NextDouble();
                if(R < .4){
                    Factory.CreateEnemyRandom(1);
                    EnemyPoints -= 1;
                } else if (R < .6 && EnemyPoints > 2 && Wave > 3){
                    Factory.CreateEnemyRandom(2);
                    EnemyPoints -= 2;
                } else if (R < .75 && EnemyPoints > 3 && Wave > 5){
                    Factory.CreateEnemyRandom(3);
                    EnemyPoints -= 3;
                } else if (R < .9 && EnemyPoints > 5 && Wave > 8){
                    Factory.CreateEnemyRandom(4);
                    EnemyPoints -= 5;
                /*
                } else if (R < .95 && EnemyPoints > 7 && Wave > 10){
                    Factory.CreateEnemyRandom(5);
                    EnemyPoints -= 7;
                } else if (EnemyPoints > 10 && Wave > 15){
                    Factory.CreateEnemyRandom(6);
                    EnemyPoints -= 10;
                    */
                } else {
                    Factory.CreateEnemyRandom(1);
                    EnemyPoints -= 1;
                }
                EnemyCount += 1;
                Timer = 0;
            }
        }   
    }
}
