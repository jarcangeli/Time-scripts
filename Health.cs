using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    public int currHealth = 1;
    [SerializeField] string hitSound = "";

    float hitDelay = .5f;
    float lastHit = 0f;
    private void Awake()
    {
        lastHit = Time.time;
    }

    public void OnDamage(int dmg)
    {
        if (Time.time > lastHit + hitDelay && currHealth > 0)
        {
            lastHit = Time.time;
            currHealth -= dmg;
            if (currHealth < 0) { currHealth = 0; }
            else if (currHealth > maxHealth) { currHealth = maxHealth; }
            else if (hitSound != "") AudioManager.instance.Play(hitSound);

            if (currHealth == 0) Die();
        }
    }

    public event Action OnDeath;
    public void Die()
    {
        if (OnDeath != null) OnDeath();
    }

}