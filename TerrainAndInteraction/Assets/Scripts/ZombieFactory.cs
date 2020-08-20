using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFactory : MonoBehaviour
{
    private static ZombieFactory _instance;
    public GameObject Zombie;
    public GameObject Player;
    public GameObject Terrain;
    private Collider c;
    
    void Awake(){
        _instance = this;
        c = Terrain.GetComponent<Collider>();
        }
        
    public static ZombieFactory Instance{
        get{
            return _instance;
            }
        }
    public Zombie CreateZombie()
    {
        // Statically define a starting position for play testing
        return CreateZombie(new Vector3(500, 100, 500));
    }
    public Zombie CreateZombieRandom(){
        // Dynamically calculate the bounds for the ground object
        var terrainCollider = c;
        var scaleX = terrainCollider.bounds.size.x;
        var scaleZ = terrainCollider.bounds.size.z;
        // Initialize a system random number generator
        var random = new System.Random();
        // A set starting Y-position such that the zombie does not fall through
        // the terrain. We can (and should) determine the height of the terrain at the
        // random X,Z location from the terrain and base the Y value from that
        var startPositionY = 100.0f;
        var startPositionX = (float) (random.NextDouble() - 0.5f)*scaleX;
        var startPositionZ = (float) (random.NextDouble() - 0.5f)*scaleZ;
        var startPosition = new Vector3(startPositionX, startPositionY, startPositionZ);
        return CreateZombie(startPosition);
    }
    private Zombie CreateZombie(Vector3 startPosition)
    {
        var zombieGameObject = Object.Instantiate(_instance.Zombie, startPosition, Quaternion.identity);
        var zombieObject =  zombieGameObject.GetComponent<Zombie>();
        zombieObject.Initialize(Player);
        return zombieObject;
    }
}