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

    public void UpdateTimeScale()
    {
        if (feelFlow)
        {
            Debug.Log(rb);
            Debug.Log(TimeController.current);
            rb.velocity = rb.velocity / TimeController.current.lastTimeScale * TimeController.current.timeScale;
            rb.gravityScale = gravityScale * TimeController.current.timeScale * TimeController.current.timeScale;
            if (GetComponent<PlayerInputs>() != null)
            {
                Debug.Log("Set gravity scale to " + rb.gravityScale);
            }
        }
        else
        {
            rb.gravityScale = gravityScale * defaultTimeScale * defaultTimeScale;
        }

    }

    public Vector2 GetVelocity(Vector2 velocity)
    {
        if (feelFlow) velocity *= TimeController.current.timeScale;
        else velocity *= defaultTimeScale;

        return velocity;
    }
    public float GetVelocity(float velocity)
    {
        if (feelFlow) velocity *= TimeController.current.timeScale;
        else velocity *= defaultTimeScale;

        return velocity;
    }

    public Vector2 GetAcceleration(Vector2 acc)
    {
        if (feelFlow) acc *= TimeController.current.timeScale * TimeController.current.timeScale;
        else acc *= defaultTimeScale * defaultTimeScale;

        return acc;
    }
    public float GetAcceleration(float acc)
    {
        if (feelFlow) acc *= TimeController.current.timeScale * TimeController.current.timeScale;
        else acc *= defaultTimeScale * defaultTimeScale;

        return acc;
    }
}

