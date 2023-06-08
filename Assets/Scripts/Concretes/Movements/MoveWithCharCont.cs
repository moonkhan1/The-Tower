using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CASP.SoundManager;
using DG.Tweening;

public class MoveWithCharCont : IMover
{
    private float rotationFactor = 8.0f;
    private readonly Transform _transform;
    private readonly Rigidbody _rb;
    float _turnSmoothVelocity;
    [SerializeField] float SmoothTurnTime = 0.1f;

    public MoveWithCharCont(PlayerController playerController)
    {
        _transform = playerController.GetComponent<Transform>();
        _rb = playerController.GetComponent<Rigidbody>();
    }
    public void MoveAction(Vector3 direction, float speed, Transform rayPoint)
    {
        StopWhenHitWall(rayPoint, direction);
        _rb.MovePosition(_transform.position + (direction * Time.fixedDeltaTime * speed));
    }
    public void RunAction(Vector3 direction, float runSpeed, Transform rayPoint)
    {
        StopWhenHitWall(rayPoint, direction);
        _rb.MovePosition(_transform.position + (direction * Time.fixedDeltaTime * runSpeed));
        
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
    
    private void StopWhenHitWall(Transform rayPoint, Vector3 direction)
    {
        LayerMask wallLayer = 8;
        const float maxHitDistance = 2f;
        const float jumpBackForce = 1f;
        if (Physics.Raycast(rayPoint.position, _transform.forward, out var hit, maxHitDistance))
        {
            if (hit.transform.gameObject.layer == wallLayer)
            {
                Debug.DrawRay(rayPoint.position, _transform.forward * hit.distance, Color.blue);
                Vector3 jumpBackDirection = -direction.normalized;
                _rb.AddForce(jumpBackDirection * jumpBackForce, ForceMode.Impulse);
            }
        }


    }

}
