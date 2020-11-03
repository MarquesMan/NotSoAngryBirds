using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Vida do inimigo, ou o quanto ele pode suportar de impulso
    public float health = 100.0f;

    void Start()
    {
        // Dispara evento para o GameManager contar quantos inimigos existem
        EventManager.TriggerEvent("InsertEnemy");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o impacto for maior que ele pode suportar
        if ( GetImpactForce(collision) >= health)
        {
            // Dispara evento para o GameManager decrementar quantia de inimigos
            EventManager.TriggerEvent("RemoveEnemy");

            // Destroi esse objeto
            Destroy(this.gameObject);
        }
    }

    private static float GetImpactForce(Collision2D collision)
    {
        float impulse = 0f;
        // Para ponto de contato de contato entre colisores
        foreach (ContactPoint2D point in collision.contacts)
        {
            // soma o Impulso aplicado a normal desse ponto
            impulse += point.normalImpulse;
        }

        // Divide a soma dos impulsos pelo tempo em segundos em que a fisica eh atualizada
        // Pois esse eh o calculo da aceleracao: 
        //         ΔV/Δt(variacao de velocidade sobre variacao de tempo)
        return impulse / Time.fixedDeltaTime;
    } 
}
