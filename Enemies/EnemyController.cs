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
        GetComponentInChildren<Animator>().SetTrigger("DeathTrigger");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, .5f);
    }

    void FixedUpdate()
    {
        behaviour.Move();
        behaviour.Act();
    }
}
