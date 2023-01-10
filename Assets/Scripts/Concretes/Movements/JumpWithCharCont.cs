using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpWithCharCont : IJump
{
    CharacterController _characterController;
    float fallMultipl = 2.5f;
    float lowJumpMultipl = 2f;
    Rigidbody rb;
    

    private void Update() {
        Debug.Log(rb.velocity.y);
    }

    public JumpWithCharCont(PlayerController playerController)
    {
        _characterController = playerController.GetComponent<CharacterController>();
        rb = playerController.GetComponent<Rigidbody>();
    }
    public void Jump(bool isJumpPressed, float jumpForce)
    {

    if(isJumpPressed)
    {
        rb.velocity = Vector3.up * jumpForce;
    }

    if(rb.velocity.y < 0.0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultipl - 1) * Time.deltaTime;

        }
    else if(rb.velocity.y > 0.0f && !isJumpPressed)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultipl - 1) * Time.deltaTime;
           
        }
    }

    
}

