using UnityEngine;

public interface IAnimation
{
    Animator _animator { get; }
    void MoveAnimation(bool isWalking);
    void JumpAnimation(bool isJumping);
    void RunAnimation(bool isRunning);
}
