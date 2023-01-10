using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputReader
{
    Vector3 Direction {get;}
    bool isJumpPressed {get;}
    bool isMovingPressed{get;}
    bool isRunPressed{get;}
}
