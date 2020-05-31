using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    bool hit = false;
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() is Health hp)
        {
            hp.OnDamage(damage);
        }
        if (!hit) DestroySelf();
    }

    void DestroySelf()
    {
        hit = true;
        if (animator != null) animator.SetTrigger("ImpactTrigger");
        else Destroy(gameObject);
    }
}
