using System;
using UnityEngine;

[RequireComponent(typeof(IAIBehaviour))] [RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    IAIBehaviour behaviour;
    Health health;
    [SerializeField] GameObject deathEffectPrefab;
    [SerializeField] GameObject spawnEffectPrefab;

    public event Action EnemyDied;

    // Start is called before the first frame update
    void Start()
    {
        behaviour = GetComponent<IAIBehaviour>();
        health = GetComponent<Health>();
        health.OnDeath += OnDeath;

        GameObject spawnEffect = Instantiate(spawnEffectPrefab);
        spawnEffect.transform.position = transform.position;

        EnemyDied += GameManager.game.IncrementDead;
    }

    void OnDeath()
    {
        AudioManager.instance.Play("EnemyDeath");
        GameObject deathEffect = Instantiate(deathEffectPrefab);
        deathEffect.transform.position = transform.position;
        Destroy(gameObject);
        if (EnemyDied != null) EnemyDied();
    }

    void FixedUpdate()
    {
        behaviour.Move();
        behaviour.Act();
    }
}
