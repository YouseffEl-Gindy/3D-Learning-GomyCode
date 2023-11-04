using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    #region References
    Transform player;
    Rigidbody rb;
    Animator animator;
    #endregion

    #region Variables
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float coolDownTime;
    HealthManager playerHealth;
    Vector3 direction = Vector3.up;
    bool dealDamage = false;
    float hitCoolDown;

    #endregion

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerHealth = player.transform.GetComponent<HealthManager>();
    }

    private void Update()
    {
        if(player)
        {
            direction = (player.position - transform.position).normalized;
            direction.y = 0;
            rb.rotation = Quaternion.LookRotation(direction);
            rb.angularVelocity = Vector3.zero;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            animator.SetFloat("MoveSpeed", 1);
        }
        else direction = Vector3.zero;

       

        if(dealDamage && playerHealth && hitCoolDown >= coolDownTime)
        {
            playerHealth.TakeDamage(damage);
            hitCoolDown = 0f;
        }
        hitCoolDown += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            dealDamage = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            dealDamage = false;
        }

    }

}
