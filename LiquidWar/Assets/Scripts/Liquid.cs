using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class Liquid : NetworkBehaviour
{
    public GameObject Particle;

    private int ParticleCount = 50;

    [SyncVar]
    private Color LiquidColor;

    private System.Random Random;


    void Awake()
    {
        Random = new System.Random();
    }
    

    public override void OnStartClient()
    {
        if (isServer)
        {
            LiquidColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
            
        var particlePosition = transform.position;
        for (int i = 0; i < ParticleCount; i++)
        {
            var mean = 0.0f;
            var stddev = 10.0f;
            var positionX = particlePosition.x + (float) RandomNormalSample(mean, stddev);
            var positionY = particlePosition.y + (float) RandomNormalSample(mean, stddev);

            AddParticle(new Vector2(positionX, positionY), gameObject, LiquidColor);
        }
    }


    private double RandomNormalSample(float mean, float stddev)
    {
        double u1 = 1.0f - Random.NextDouble();
        double u2 = 1.0f - Random.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        return (mean + stddev * randStdNormal);
    }
    

    [Server]
    void AddParticle(Vector2 position, GameObject liquid, Color liquidColor)
    {
        var newParticle = Instantiate(Particle, new Vector2(position.x, position.y), Quaternion.identity);
        newParticle.GetComponent<Particle>().Liquid = gameObject;
        newParticle.GetComponent<Particle>().LiquidColor = LiquidColor;
        NetworkServer.SpawnWithClientAuthority(newParticle, connectionToClient);
    }


    void AddParticleReceiver(object[] args)
    {
        Transform transform = (Transform) args[0];
        var position = new Vector2(transform.position.x, transform.position.y);
        GameObject liquid = (GameObject) args[1];
        Color liquidColor = (Color) args[2];
        AddParticle(position, liquid, liquidColor);
    }

}
