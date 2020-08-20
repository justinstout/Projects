using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject PlayerGameObject;
    private Player PlayerObject;
    private bool Attacking = false;
    public float Health;
    public int PointValue;
    private float Damage = 10.0f;
    private int Type;
    private NavMeshAgent agent;
    private bool stop;

    private CreateEnemy createEnemy;
    public GameObject createEnemyGameObject;
    public GameObject AmmoCrate;
    public AudioSource AudioSource;
    public AudioClip HitSFX;
    //public Transform PlayerTransform;

    
    void Awake(){
       // Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        createEnemyGameObject = GameObject.FindGameObjectWithTag("EmptyEnemy");
        createEnemy = createEnemyGameObject.GetComponent<CreateEnemy>();
        AudioSource.clip = HitSFX;
    }
    
    public void Initialize(GameObject player, int type){
        PlayerGameObject = player;
        Type = type;
        //if(type == x) {Damage = y}
        PlayerObject = PlayerGameObject.GetComponent<Player>();
    }

    private float Distance(){
        var x = gameObject.transform.position.x - PlayerGameObject.transform.position.x;
        var y = gameObject.transform.position.y - PlayerGameObject.transform.position.y;
        var dist = Mathf.Sqrt((x*x) + (y*y));
        return dist;
    }

    void Update(){
        agent.SetDestination(PlayerGameObject.transform.position);
        Navigate.DebugDrawPath(agent.path.corners);
        if (Distance() <= 1.1f && Attacking == false){
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack(){
        Attacking = true;
        PlayerGameObject.GetComponent<Player>().DecreaseHealth(Damage);
        yield return new WaitForSeconds(1.0f);
        Attacking = false;
    }

    public void DamageEnemy(float damage) {
        if( SoundController.Instance.sfxON == true )
            AudioSource.Play();
        Health -= damage;
        Debug.Log("Enemy Damage Taken: " + damage + ". Total health for enemy " + GetComponent<Enemy>() + ": " + Health);
        if (Health <= 0.0f) {

            if( createEnemy.EnemyCount == 1 )
            {
                Vector2 positionOfEnemy = new Vector2( GetComponent<Enemy>().gameObject.transform.position.x, GetComponent<Enemy>().gameObject.transform.position.y );
                DropAmmoCrate(positionOfEnemy);
            }

            Destroy(GetComponent<Enemy>().gameObject);
            createEnemy.EnemyCount -= 1;
            Points.Instance.EnemyKilled(PointValue);
        }
    }


    void DropAmmoCrate(Vector2 EnemyPosition)
    {
        //Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Vector2 playerPos = new Vector2(Camera.main.ScreenToWorldPoint(PlayerTransform.position).x  + 2 ,Camera.main.ScreenToWorldPoint(PlayerTransform.position).y  + 2 );
        Instantiate(AmmoCrate, EnemyPosition, Quaternion.identity);
    }



    // IEnumerator EnemyDeath() {
    //     yield return new WaitForSeconds(2.0f);
    //     Destroy(GetComponent<Enemy>().gameObject);
    //     // createEnemy.EnemyCount -= 1;
    // }
}
