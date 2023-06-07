using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CASP.SoundManager;
public class MoveWithCharCont : IMover
{
    private float rotationFactor = 8.0f;
    Transform _transform;
    Rigidbody rb;
    float turnSmoothVelocity;
    [SerializeField] float SmoothTurnTime = 0.1f;
   
    public MoveWithCharCont(PlayerController playerController)
    {
        _transform = playerController.GetComponent<Transform>();
        rb = playerController.GetComponent<Rigidbody>();
    }
    public void MoveAction(Vector3 direction, float speed)
    {
        // _characterController.Move(direction * Time.fixedDeltaTime * speed);
        rb.MovePosition(_transform.position + (direction * Time.fixedDeltaTime * speed));

    }
    public void RunAction(Vector3 direction, float runSpeed)
    {
        rb.MovePosition(_transform.position + (direction * Time.fixedDeltaTime * runSpeed));
    }
    public void HandleRotation(Vector3 direction, bool isMoving)
    {
        Vector3 positionToLook;
        positionToLook.x = direction.x;
        positionToLook.y = 0.0f;
        positionToLook.z = direction.z;
        Quaternion currentRot = _transform.rotation;

        if(isMoving)
        {    
         Quaternion targetRotation = Quaternion.LookRotation(positionToLook);
        _transform.rotation = Quaternion.Slerp(currentRot, targetRotation, rotationFactor * Time.fixedDeltaTime);
        }
    }

    
}
