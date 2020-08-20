using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAmmo : MonoBehaviour
{
    private AmmoFactory Factory;
    
    private static float SpawnTime = 20.0f;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Factory = AmmoFactory.Instance;
        Factory.CreateAmmo();
        Timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > SpawnTime){
            Timer = 0;
            Factory.CreateAmmoRandom();
            
        }
    }
}
