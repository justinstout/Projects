using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Vector3 moveDirection;
    private float timer = 0.0f;
    public int speed = 6;
    public float damage;
    public float range;
    void Start(){
    moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    moveDirection.z = 0;       
    moveDirection.Normalize();
    
    }
    void Update(){
        timer += Time.deltaTime;
        if(timer > range){
            Destroy(this.gameObject);
        }
     transform.position = transform.position + moveDirection * speed* Time.deltaTime;
    }


    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().DamageEnemy(damage);
            Destroy(gameObject);

            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("HIT ENEMY");
        }
        //Debug.Log("*** OnCollisionEnter2D");
    }


    

}
