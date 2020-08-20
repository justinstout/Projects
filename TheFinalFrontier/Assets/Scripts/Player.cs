using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static float PlayerWalkSpeed = 10.0f;
    //private float Health = 100.0f;
    private Rigidbody2D Rigidbody;
    private bool Attacked = false;
    private bool Healing = false;
    public GameObject HealthBar;
    private bool Sprinting = false;
    private float SprintDelay = 10;
    public float Timer = 0;
    private float SprintTime = 1;
    private float SprintMultiplication = 2;
    public Animator animator;

    void Awake(){
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
        float directionHorizontal = Input.GetAxisRaw("Horizontal");
        float directionVertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Move", Mathf.Abs(directionHorizontal) + Mathf.Abs(directionVertical));
        Rigidbody.velocity = ((Vector2.right * directionHorizontal) + (Vector2.up * directionVertical)) * PlayerWalkSpeed;
        
    }

    public void DecreaseHealth(float healthLoss) {
        HealthBar.GetComponent<CharacterHealth>().DealDamage(healthLoss);
        // if (Health == 0) {
        //     SceneManager.LoadScene("GameOver");
        // }
    }

    void Update(){
        Timer += Time.deltaTime;
        if (Sprinting == false && Timer > SprintDelay && Input.GetKeyDown("left shift")){
            StartCoroutine(Sprint());
            Timer = 0;

        }
    }

    public void UpgradeSprintSpeed(){
        SprintMultiplication += .5f;
    }
    public void UpgradeSprintTime(){
        SprintTime += .5f;
    }
    public void UpgradeSprintFrequency(){
        SprintTime -= 2;
    }

    IEnumerator Sprint(){
        Sprinting = true;
        PlayerWalkSpeed *= SprintMultiplication;
        yield return new WaitForSeconds(SprintTime);
        PlayerWalkSpeed *= 1/SprintMultiplication;
        Sprinting = false;
    }


    


}
