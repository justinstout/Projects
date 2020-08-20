using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance;
    public GameObject Alien1;
    public GameObject Alien2;
    public GameObject Alien3;
    public GameObject Robot1;
    public GameObject Robot2;
    public GameObject Robot3;

    public GameObject Player;
    public GameObject Terrain;
    public List<int> OpenSpawnPoints;
    public List<GameObject> spawnPointObjects;
    
    void Awake(){
        _instance = this;
        }
        
    public static EnemyFactory Instance{
        get{
            return _instance;
            }
        }
    public Enemy CreateEnemy()
    {
        // Statically define a starting position for play testing
        return CreateEnemy(new Vector2(40, -40), 1);
    }
    public Enemy CreateEnemyRandom(int type){
        // Dynamically calculate the bounds for the ground object
       
        float x;
        float y;

        
        // Initialize a system random number generator
        var random = new System.Random();

        //Deciding Spawn Zones

        int a = Random.Range(0, OpenSpawnPoints.Count);
        Debug.Log(a);
        x = spawnPointObjects[OpenSpawnPoints[a]].transform.position.x - 5;
        Debug.Log(x);
        y = spawnPointObjects[OpenSpawnPoints[a]].transform.position.y - 5;
        Debug.Log(y);
        


        // A set starting Y-position such that the zombie does not fall through
        // the terrain. We can (and should) determine the height of the terrain at the
        // random X,Z location from the terrain and base the Y value from that
        var startPositionX = x + ((float) random.NextDouble()) * 10;
        var startPositionY = y + ((float) random.NextDouble()) * 10;
        var startPosition = new Vector2(x, y);
        return CreateEnemy(startPosition, type);
    }


    private Enemy CreateEnemy(Vector2 startPosition, int type)
    {
        if(type == 1){
            var EnemyGameObject =  Object.Instantiate(_instance.Alien1, startPosition, Quaternion.identity);
            var enemyObject =  EnemyGameObject.GetComponent<Enemy>();
            enemyObject.Initialize(Player, type);
            return enemyObject;
            
        } else if (type == 2){
            var EnemyGameObject = Object.Instantiate(_instance.Alien2, startPosition, Quaternion.identity);
            var enemyObject =  EnemyGameObject.GetComponent<Enemy>();
            enemyObject.Initialize(Player, type);
            return enemyObject;
            
        } else if (type == 3){
            var EnemyGameObject =  Object.Instantiate(_instance.Alien3, startPosition, Quaternion.identity);
            var enemyObject =  EnemyGameObject.GetComponent<Enemy>();
            enemyObject.Initialize(Player, type);
            return enemyObject;
        } else if (type == 4){
            var EnemyGameObject = Object.Instantiate(_instance.Robot1, startPosition, Quaternion.identity);
            var enemyObject =  EnemyGameObject.GetComponent<Enemy>();
            enemyObject.Initialize(Player, type);
            return enemyObject;
            /*
        } else if (type == 5){
            var EnemyGameObject =  Object.Instantiate(_instance.Robot2, startPosition, Quaternion.identity);
            var enemyObject =  EnemyGameObject.GetComponent<Enemy>();
            enemyObject.Initialize(Player, type);
            return enemyObject;
        } else if (type == 6){
            var EnemyGameObject = Object.Instantiate(_instance.Robot3, startPosition, Quaternion.identity);
            var enemyObject =  EnemyGameObject.GetComponent<Enemy>();
            enemyObject.Initialize(Player, type);
            return enemyObject;
            */
        } else {
            var EnemyGameObject =   Object.Instantiate(_instance.Alien1, startPosition, Quaternion.identity);
            var enemyObject =  EnemyGameObject.GetComponent<Enemy>();
            enemyObject.Initialize(Player, type);
            return enemyObject;
        }
        
        
        
        
    }
}