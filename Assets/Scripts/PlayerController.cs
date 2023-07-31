using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 1f;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisons = new List<RaycastHit2D>();
    public float collisonOffset = 0.05f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //if movment input is not zero try to move 
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success && movementInput.x > 0)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success && movementInput.y > 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);

        }
        // set direction of sprite to movement direction
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = false;

        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    private bool TryMove(Vector2 direction)
    { // check for potinial collisons
        int count = rb.Cast(
           direction, // x and y values bewteen -1 and 1 that repersent the direction from the body to look for collisons
           movementFilter,// the settings that determine where a collison can occur on such as layers to collide with
           castCollisons, // list of collisons to store the found collisons into after the cast is finished
           movespeed * Time.fixedDeltaTime + collisonOffset); // the amount of cast equal to the movement plus an offset

        if (count == 0)
        {
            rb.MovePosition(rb.position + movementInput * movespeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnMove(InputValue movmentValue)
    {
        movementInput = movmentValue.Get<Vector2>();
    }
}
