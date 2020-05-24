using UnityEngine;

[RequireComponent(typeof(IAIBehaviour))] [RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    IAIBehaviour behaviour;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        behaviour = GetComponent<IAIBehaviour>();
        health = GetComponent<Health>();
        health.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        behaviour.Move();
        behaviour.Act();
    }
}
