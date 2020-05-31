using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidAI : MonoBehaviour, IAIBehaviour
{
    Rigidbody2D rb;
    public float speed = .2f;
    Transform player;
    TimeFlow timeFlow;

    Gun gun;

    void Awake()
    {
        timeFlow = GetComponent<TimeFlow>();
        gun = GetComponentInChildren<Gun>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // flying
    }
    void Start() 
    {
        player = FindObjectOfType<PlayerInputs>().transform;
    }

    public void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime * timeFlow.timeScale);
    }
    public void Act()
    {
        if (gun.CanFire())
        {
            gun.FireGun(player.position);
            GetComponentInChildren<Animator>().SetTrigger("ShootTrigger");
        }
    }

    public void Superify()
    {
        gun.shotDelay /= 2f;
        speed *= 2;
    }
}
