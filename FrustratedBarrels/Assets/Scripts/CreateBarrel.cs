using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBarrel : MonoBehaviour
{
    private BarrelFactory Factory;
    private static int SpawnCount = 30;
    // Start is called before the first frame update
    void Start(){
        Factory = BarrelFactory.Instance;
        for (int i = 0; i < SpawnCount; i++){
            // TODO: Add code to tell the factory to create a new barrel
            Factory = BarrelFactory.Instance;
            Factory.CreateBarrelRandom();
        }
    }
}
