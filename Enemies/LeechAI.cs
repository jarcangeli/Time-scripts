using UnityEngine;

public class LeechAI : MonoBehaviour, IAIBehaviour
{
    Rigidbody2D rb;

    Vector2 targetPos;
    [SerializeField]
    float speed = 1f;

    bool sucking = false;
    float suckRange = 0.5f;

    TimeFlow timeFlow;
    [SerializeField]
    float timeMult = 1/4f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timeFlow = GetComponent<TimeFlow>();
        timeFlow.gravityScale = 0f; // flying
    }
    void Start()
    {
        targetPos = FindObjectOfType<ClockCenter>().transform.position;
        if (targetPos.x > transform.position.x)
        {
            transform.right *= -1;
        }
        GetComponent<Health>().OnDeath += OnDeath;

        GetComponentInChildren<Animator>().SetTrigger("FlyTrigger");
    }

    void OnDeath()
    {
        if (sucking)
        {
            TimeController.current.ChangeTimeScale(TimeController.current.timeScale / timeMult);
        }
    }

    public void Move()
    {
        if (!sucking)
        {
            Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
            Vector2 move = direction * speed * Time.fixedDeltaTime * timeFlow.timeScale;
            rb.MovePosition((Vector2)transform.position + move);
        }
    }
    public void Act()
    {
        if (!sucking && Vector2.Distance(transform.position, targetPos) < suckRange)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            sucking = true;
            TimeController.current.ChangeTimeScale(TimeController.current.timeScale * timeMult);
            GetComponentInChildren<Animator>().SetTrigger("SuckTrigger");
        }
    }

    public void Superify()
    {
        speed *= 2;
    }
}
