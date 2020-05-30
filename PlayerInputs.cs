using UnityEngine;

[RequireComponent(typeof(TimeFlow))]
public class PlayerInputs : MonoBehaviour
{
    public static PlayerInputs player;

    Rigidbody2D rb;
    Health health;
    TimeFlow timeFlow;

    // Movement properties
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    float jumpForce = 10f;
    [SerializeField]
    Transform groundCheck = null;
    [SerializeField]
    float groundRadius = 0.1f;
    [SerializeField]
    bool isGrounded = false;
    int groundLayer;
    bool doubleJumped = false;

    Camera mainCamera;
    Gun gun;

    [SerializeField] Transform characterSprite = null;
    [SerializeField] Transform gunSprite = null;

    private void Awake()
    {
        player = this;
    }

    private void Start()
    {
        // Cache own components
        timeFlow = GetComponent<TimeFlow>();
        rb = GetComponent<Rigidbody2D>();
        gun = GetComponent<Gun>();

        health = GetComponent<Health>();
        health.OnDeath += OnDeath;

        mainCamera = FindObjectOfType<Camera>();
        groundLayer =~ LayerMask.GetMask("Player"); // jump off everything but self
    }

    void OnDeath()
    {
        Debug.Log("Game Over");
        this.enabled = false;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
    }
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal") * timeFlow.timeScale * speed * Time.fixedDeltaTime;
        float y = rb.velocity.y;

        rb.velocity = new Vector2(x, y);

        // Check grounded
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        isGrounded = collider != null;
        doubleJumped = isGrounded ? false : doubleJumped; // reset double jump on grounding
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) && (isGrounded || !doubleJumped))
        {
            float jumpVel = timeFlow.GetVelocity(jumpForce);
            if (doubleJumped) jumpVel /= 2f;

            rb.velocity = new Vector2(rb.velocity.x, jumpVel);

            if (!isGrounded) { doubleJumped = true; }
        }

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        FaceMouse(mousePosition);
        gun.PointGun(mousePosition);
        if (Input.GetMouseButton(0) && gun.CanFire())
        {
            gun.FireGun(mousePosition);
        }
    }

    void FaceMouse(Vector2 mousePosition)
    {
        if (mousePosition.x > transform.position.x)
        {
            characterSprite.localRotation = Quaternion.Euler(0f, 0f, 0f);
            gunSprite.localRotation = Quaternion.Euler(gunSprite.localRotation.eulerAngles.x, 0f, gunSprite.localRotation.eulerAngles.z);
        }
        else if (mousePosition.x < transform.position.x)
        {
            characterSprite.localRotation = Quaternion.Euler(0f, 180f, 0f);
            gunSprite.localRotation = Quaternion.Euler(gunSprite.localRotation.eulerAngles.x, 180f, gunSprite.localRotation.eulerAngles.z);
        }
    }
    public void OnDrawGizmosSelected()
    {
        // Draw the ground check radius
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
