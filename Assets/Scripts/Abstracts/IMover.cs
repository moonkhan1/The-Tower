using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover 
{
    void MoveAction(Vector3 direction, float speed);
     void RunAction(Vector3 direction, float speed);
    void HandleRotation(Vector3 direction, bool isMoving);
}
