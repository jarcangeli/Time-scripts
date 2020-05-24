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

        GetComponent<Health>().OnDeath += OnDeath;
    }

    void OnDeath()
    {
        Debug.Log("Stopped zuk");
        if (sucking)
        {
            TimeController.current.ChangeTimeScale(TimeController.current.timeScale / timeMult);
        }
    }

    public void Move()
    {
        if (!sucking)
        {
            Vector2 direction = targetPos - (Vector2)transform.position;
            rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime * timeFlow.timeScale);
        }
    }
    public void Act()
    {
        if (!sucking && Vector2.Distance(transform.position, targetPos) < suckRange)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            sucking = true;
            Debug.Log("ZZuukkkk");
            TimeController.current.ChangeTimeScale(TimeController.current.timeScale * timeMult);
        }
    }
}
