﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() is Health hp)
        {
            hp.OnDamage(damage);
        }
        DestroySelf();
    }

    void DestroySelf()
    {
        GetComponentInChildren<Animator>().SetTrigger("ImpactTrigger");
        Destroy(gameObject, 0.1f);
    }
}
