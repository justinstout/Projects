using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float damage;
    public float range;

    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(){
        GameObject firedBullet = Instantiate(Bullet, transform.position, transform.rotation, transform.parent.parent.parent );
        firedBullet.GetComponent<bullet>().damage = damage;
        firedBullet.GetComponent<bullet>().range = range;
    }
}
