using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Zombie;

    // Update is called once per frame
    void Update()
    {
        var lookX = Player.transform.position.x;
        var lookY = Zombie.transform.position.y;
        var lookZ = Player.transform.position.z;
        transform.LookAt(new Vector3(lookX, lookY, lookZ));
    }
}
