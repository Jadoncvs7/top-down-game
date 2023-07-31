using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 1f;
    Vector2 movementInput;
    Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisons = new List<RaycastHit2D>();
    public float collisonOffset = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            int count = rb.Cast(
            movementInput,
            movementFilter,
            castCollisons,
            movespeed * Time.fixedDeltaTime + collisonOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * movespeed * Time.fixedDeltaTime);
            }
        }
    }
    void OnMove(InputValue movmentValue)
    {
        movementInput = movmentValue.Get<Vector2>();
    }
}