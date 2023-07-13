using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private float currentSpeed;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private Vector2 movement;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float movementSpeed; // Value for the player's movement speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set Player Movement Speed
        currentSpeed = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().statSO.moveSpeed;
    }

    void Update()
    {
        // Movement speed acts as a multiplier for Movement Vector values
        movement.x = Input.GetAxisRaw("Horizontal") * movementSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * movementSpeed;
    }

    // Updated the player movement implementation for a tighter feel
    private void FixedUpdate()
    {
        // Player Movement is now through force; experimenting...
        // No longer normalizes the Vector2
        Vector2 movement = new Vector2(this.movement.x, this.movement.y);
        rb.velocity = movement; // Used velocity instead of add force for more consistency

        if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
    }
    
    #region Functions
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
    
    public void UpdateMovementSpeed(float speed)
    {
        // Updates speed when Player hits an upgrade
        currentSpeed = speed;
    }
    #endregion
}
