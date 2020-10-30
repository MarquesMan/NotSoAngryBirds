using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ( GetImpactForce(collision) >= 100.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private static float GetImpactForce(Collision2D collision)
    {
        float impulse = 0f;
        foreach (ContactPoint2D point in collision.contacts)
        {
            impulse += point.normalImpulse;
        }

        return impulse / Time.fixedDeltaTime;
    } 
}
