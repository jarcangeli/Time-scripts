using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPad : MonoBehaviour
{
    float lastPickupTime = 0f;
    [SerializeField] float spawnDelay = 6f;
    Collider2D ownCollider;
    Renderer ownRenderer;

    // set to collision layer with only player collisions
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(transform.name + " collided with " + collider.transform.name);
        Health playerHealth = collider.GetComponent<Health>();
        if (playerHealth != null && playerHealth.currHealth < playerHealth.maxHealth && playerHealth.currHealth > 0)
        {
            playerHealth.OnDamage(-1); // heals
            lastPickupTime = Time.time;
            Disable();
            AudioManager.instance.Play("PlayerPickup");
        }
    }

    private void Awake()
    {
        lastPickupTime = Time.time + 5f;
        ownCollider = GetComponent<Collider2D>();
        ownRenderer = GetComponentInChildren<Renderer>();
        Disable();
    }
    private void Update()
    {
        if (Time.time >= lastPickupTime + spawnDelay)
        {
            Enable();
        }
    }

    void Disable()
    {
        ownRenderer.enabled = false;
        ownCollider.enabled = false;
    }
    void Enable()
    {
        ownRenderer.enabled = true;
        ownCollider.enabled = true;
    }
}
