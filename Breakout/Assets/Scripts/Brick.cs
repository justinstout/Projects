﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameObject ThisBrick;
    private int HitsToDestroy;
    private int HitCount = 0;
    
    public void Initialize(GameObject thisBrick, int hitsToDestroy){
        ThisBrick = thisBrick;
        HitsToDestroy = hitsToDestroy;
        }
    public int OnHit(){
        HitCount += 1;
        if (HitsToDestroy <= HitCount){
            Destroy(ThisBrick);
            return HitsToDestroy;
        }
        return HitCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
