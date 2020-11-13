using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D objBody2D;
    public float minimalVelocity = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        objBody2D = GetComponent<Rigidbody2D>();
        // Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!objBody2D.isKinematic && objBody2D.velocity.magnitude <= minimalVelocity)
            Destroy(gameObject);
    }

    void OnApplicationQuit()
    {

    }

    void OnDisable()
    {
        if(EventManager.instance)
            EventManager.TriggerEvent("BirdKilled");
    }
}
