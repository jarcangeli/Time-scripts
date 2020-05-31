using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySprites : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInputs player;
    [SerializeField] SpriteRenderer bodySprite = null;
    [SerializeField] Sprite standSprite = null;
    [SerializeField] Sprite sideSprite = null;
    [SerializeField] Sprite jumpSprite = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isGrounded)
        {
            bodySprite.sprite = jumpSprite;
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.5)
        {
            bodySprite.sprite = sideSprite; 
        }
        else
        {
            bodySprite.sprite = standSprite;
        }
    }
}
