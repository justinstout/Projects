using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class Particle : NetworkBehaviour
{
    private Rigidbody2D Rigidbody;
    private float MoveSpeed = 100.0f;
    private System.Random Random;

    [SyncVar]
    public GameObject Liquid;
    [SyncVar]
    public Color LiquidColor;
    [SyncVar]
    public Boolean Battling = false;


    void Awake()
    {
        Random = new System.Random();
        Rigidbody = this.GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        GetComponent<Renderer>().material.color = LiquidColor;
        StartCoroutine(CannotBattle());
    }


    void FixedUpdate()
    {
        var position = Liquid.transform.position;
        var direction = new Vector2(position.x - transform.position.x, position.y - transform.position.y).normalized;
        Rigidbody.velocity = direction * MoveSpeed;
    }


    [ServerCallback]
    void OnCollisionEnter2D(Collision2D collision) 
    {
        var otherGameObject = collision.collider.gameObject;
        if (otherGameObject.tag == "Particle")
        {
            var particle = gameObject.GetComponent<Particle>();
            var otherParticle = otherGameObject.GetComponent<Particle>();

            if (particle.Liquid != otherParticle.Liquid)
            {
                if (!Battling && !otherParticle.Battling)
                {
                    if (ThisParticleWins())
                    {
                        StartCoroutine(Battle(otherParticle));
                    }
                    else
                    {
                        StartCoroutine(otherParticle.Battle(particle));
                    }
                }
            }
        }
    }


    private bool ThisParticleWins()
    {
        return Random.NextDouble() >= 0.5;
    }


    IEnumerator Battle(Particle otherParticle)
    {
        Battling = true;
        otherParticle.Battling = true;

        otherParticle.gameObject.SetActive(false);
        Cmd_DestroyParticle(otherParticle.gameObject);
        var newParticleArgs = new object[] {otherParticle.gameObject.transform, Liquid, LiquidColor};
        Liquid.SendMessage("AddParticleReceiver", newParticleArgs, SendMessageOptions.DontRequireReceiver);

        yield return StartCoroutine(CannotBattle());
    }


    [Command]
    private void Cmd_DestroyParticle(GameObject particle)
    {
        NetworkServer.Destroy(particle);
    }


    IEnumerator CannotBattle()
    {
        Battling = true;
        yield return new WaitForSeconds(0.5f);
        Battling = false;
    }


}
