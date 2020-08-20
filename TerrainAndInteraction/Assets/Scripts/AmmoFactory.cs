using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoFactory : MonoBehaviour
{
   private static AmmoFactory _instance;
    public GameObject Ammo;
    public GameObject Terrain;
    private Collider c;
    
    void Awake(){
        _instance = this;
        c = Terrain.GetComponent<Collider>();
        }
        
    public static AmmoFactory Instance{
        get{
            return _instance;
            }
        }
    public PickupAmmo CreateAmmo()
    {
        // Statically define a starting position for play testing
        return CreateAmmo(new Vector3(500, 100, 500));
    }
    public PickupAmmo CreateAmmoRandom(){
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
        return CreateAmmo(startPosition);
    }
    private PickupAmmo CreateAmmo(Vector3 startPosition)
    {
        var ammoGameObject = Object.Instantiate(_instance.Ammo, startPosition, Quaternion.identity);
        var ammoObject =  ammoGameObject.GetComponent<PickupAmmo>();
        ammoObject.Initialize();
        return ammoObject;
    }
}
