using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float BallSpeed = 300.0f;
    private static Vector2 BallStartPosition;
    public GameObject Paddle;
    private Rigidbody2D Rigidbody;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        BallStartPosition = transform.position;
        
    }
    void Awake(){
        
        Rigidbody = GetComponent<Rigidbody2D>();
        tr = Paddle.GetComponent<Transform>();
        transform.position = BallStartPosition;
        Rigidbody.velocity = Vector2.up * BallSpeed;
    }
    private float CalculateHorizontalUpdate(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth){
        return (ballPosition.x - paddlePosition.x) / paddleWidth;
    }
    private float CalculateVerticalUpdate(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth){
        return (ballPosition.y - paddlePosition.y) / paddleWidth;
    }
    private void OnCollisionEnter2D(Collision2D hit){
        var gameObject = hit.collider.gameObject;
        var label = gameObject.tag;
        if(label == "Paddle"){
            float horizontalUpdate = CalculateHorizontalUpdate(transform.position, hit.transform.position, hit.collider.bounds.size.x);
            float VerticalUpdate = CalculateVerticalUpdate(transform.position, hit.transform.position, hit.collider.bounds.size.y);
            Vector2 direction = new Vector2(horizontalUpdate, VerticalUpdate).normalized;
            Rigidbody.velocity = direction * BallSpeed;
            
        }
        
        if(label == "Brick"){

            GameController.Instance.BrickCollision(gameObject);
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 ballPosition = transform.position;
        Vector2 paddlePosition = tr.position;

        if(ballPosition.y < (paddlePosition.y - 10)){
            GameController.Instance.LifeLost();
            Awake();

        }

    }
}
