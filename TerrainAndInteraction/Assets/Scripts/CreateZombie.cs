using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateZombie : MonoBehaviour
{
    private ZombieFactory Factory;
    public GameObject Player;
    private static float SpawnTime = 2.0f;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Factory = ZombieFactory.Instance;
        Factory.CreateZombie();
        Timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > SpawnTime){
            Factory.CreateZombieRandom();
            Timer = 0;
        }
    }
}
