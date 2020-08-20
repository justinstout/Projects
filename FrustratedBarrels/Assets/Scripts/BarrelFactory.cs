using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelFactory : MonoBehaviour
{
    private static BarrelFactory _instance;
    public GameObject Barrel;
    public GameObject Terrain;
    private System.Random Random;
    
    void Awake(){
        _instance = this;
        Random = new System.Random();
    }
    public static BarrelFactory Instance{
        // TODO: Add code to return the private instance of BarrelFactory
        get{
            return _instance;
            }
    }
        
        public Barrel CreateBarrelRandom(){
            // TODO: create a variable named startPosition and populate it with the position in the
            // scene where a new barrel will be placed
            var terrainCollider = Terrain.GetComponent<Collider>();
            var scaleX = 25;
            var scaleZ = 25;
            // Initialize a system random number generator
            // A set starting Y-position such that the zombie does not fall through
            // the terrain. We can (and should) determine the height of the terrain at the
            // random X,Z location from the terrain and base the Y value from that
            var startPositionY = 5.0f;
            var startPositionX = (float) (Random.NextDouble() - 0.5f)*scaleX;
            var startPositionZ = (float) (Random.NextDouble() - 0.5f)*scaleZ;
            var startPosition = new Vector3(startPositionX, startPositionY, startPositionZ);
            return CreateBarrel(startPosition);
        }
        private Barrel CreateBarrel(Vector3 startPosition){
            var startRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
            var barrelGameObject = Object.Instantiate(_instance.Barrel, startPosition, startRotation);
            var barrelObject =  barrelGameObject.GetComponent<Barrel>();
            barrelObject.Initialize();
            // TODO: Instantiate a new Barrel GameObject and return the Barrel object
            // Note: The asset comes pre-defined rotated sideways -- you will need to use the
            // startRotation defined above in your object instantiation call
        
            return barrelObject;
        }
}
