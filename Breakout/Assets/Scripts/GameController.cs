using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject AllBricks;
    private static GameController _instance;
    public static int BrickCount;
    public static string[] Levels = {"Level1", "Level2"};
    private static int LevelIndex = 0;
    private int Score = 0;
    private HUD HUD;
    private int ExtraLifeCount = 2;
    private int ExtraLifeCountMax = 5;
    

    public static GameController Instance{

        get{
        return _instance;

        }
        
    }
    void Awake(){
        HUD = HUD.Instance;
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        } else {
            Destroy(this);
        }
    }
    void OnEnable(){
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Start is called before the first frame update
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        AllBricks = GameObject.FindGameObjectWithTag("AllBricks");
    
        BrickCount = 0;
        foreach (Transform brickRow in AllBricks.transform){
            foreach(Transform brick in brickRow.transform){
                brick.GetComponent<Brick>().Initialize(brick.gameObject, LevelIndex + 1);
                BrickCount += 1;
            }
        }  
    }


    public void LifeLost(){
        
        if(ExtraLifeCount <= 0){
            Application.Quit();
        } else {
            ExtraLifeCount -= 1;
        }
    }

    public void BrickCollision(GameObject brick){
        var scoreChange = brick.GetComponent<Brick>().OnHit();
        if(scoreChange > 0){
            BrickCount -= 1;
            Score += scoreChange;
        }
    }
    // Update is called once per frame
    void Update()
    {
        HUD.Render(Score, LevelIndex + 1, ExtraLifeCount);
        if(BrickCount == 0){
            LevelIndex += 1;
            SceneManager.LoadScene(Levels[LevelIndex % Levels.Length]);
            if(ExtraLifeCountMax > ExtraLifeCount){
                ExtraLifeCount += 1;
            }
        
            
        }

    }
}
