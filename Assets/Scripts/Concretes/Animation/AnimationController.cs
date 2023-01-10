using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : IAnimation
{
    public Animator _animator{get;set;}


    public AnimationController(PlayerController playerController)
    {
        _animator = playerController.GetComponent<Animator>();
    }

    public  void MoveAnimation(bool isWalking)
    {
        // if(_animator.GetFloat("isWalking") == moveSpeed) return;
        // else
        //     _animator.SetFloat("isWalking",moveSpeed, 0.1f, Time.deltaTime);
        if(isWalking)
        {
            _animator.SetBool("Walking", true);
        }
        else{
            _animator.SetBool("Walking", false);
        }

        
    }

    public  void RunAnimation( bool isRunning)
    {
        if(isRunning)
        {
            _animator.SetBool("isRunning", true);
        }
        else{
            _animator.SetBool("isRunning", false);
        }
    }


    public  void JumpAnimation(bool isJumping)
    {
        if(isJumping)
        {
            _animator.SetBool("Jump", true);
            Debug.Log("True Jump");
        }
        else
        {
            _animator.SetBool("Jump", false);
            Debug.Log("False Jump");
        }
    }

}

