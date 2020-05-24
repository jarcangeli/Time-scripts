using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Bullet hit " + collider.name);
        if (collider.GetComponent<Health>() is Health hp)
        {
            hp.OnDamage(damage);
        }
        DestroySelf();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
