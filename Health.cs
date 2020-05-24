using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    public int currHealth = 1;

    public void OnDamage(int dmg)
    {
        currHealth -= dmg;
        if (currHealth < 0) { currHealth = 0; }
        else if (currHealth > maxHealth) { currHealth = maxHealth; }

        if (currHealth == 0) Die();
    }

    public event Action OnDeath;
    public void Die()
    {
        if (OnDeath != null) OnDeath();
        Debug.Log(transform.name + " died");
    }

}