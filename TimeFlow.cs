using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TimeFlow : MonoBehaviour
{
    /*
     Class to control the time flow on a rigidbody, as controlled by the TimeController
     Corrects velocities and accelerations by timeScale
    */
    public bool feelFlow = true;
    public float defaultTimeScale = 10f;
    public float gravityScale = 1f / 50;

    TimeController timeController;
    Rigidbody2D rb;

    public float timeScale 
    {
        get 
        {
            if (feelFlow) return TimeController.current.timeScale;
            else return defaultTimeScale;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timeController = TimeController.current;
    }

    public void UpdateTimeScale()
    {
        if (feelFlow)
        {
            Debug.Log(rb);
            Debug.Log(timeController);
            rb.velocity = rb.velocity / timeController.lastTimeScale * timeController.timeScale;
            rb.gravityScale = gravityScale * timeController.timeScale * timeController.timeScale;
        }
        else
        {
            rb.gravityScale = gravityScale * defaultTimeScale * defaultTimeScale;
        }

    }

    public Vector2 GetVelocity(Vector2 velocity)
    {
        if (feelFlow) velocity *= timeController.timeScale;
        else velocity *= defaultTimeScale;

        return velocity;
    }

    public Vector2 GetAcceleration(Vector2 acc)
    {
        if (feelFlow) acc *= timeController.timeScale * timeController.timeScale;
        else acc *= defaultTimeScale * defaultTimeScale;

        return acc;
    }
}

