using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] Behaviour[] switchOnDeath;
    bool dead = false;

    public void TakeDamage(float amount)
    {
        if (dead) return;
        health -= amount;
        if(health <=0)
        {
            health = 0;
            Die();
        }
    }
    void Die()
    {
        dead = true;
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 4f);
        foreach (Behaviour b in switchOnDeath)
        {
            b.enabled = !b.enabled;
        }

        try
        {
            GetComponent<Animator>().SetTrigger("Dead");
        }
        catch 
        {
            Debug.Log("There is no animator in this Game Object");
        }

    }
}
