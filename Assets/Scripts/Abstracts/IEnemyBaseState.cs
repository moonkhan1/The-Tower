using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemyBaseState 
{
    public abstract void EnterState(EnemyController _enemy);
    public abstract void UpdateState(EnemyController _enemy);
    public abstract void OnCollisionEnter(EnemyController _enemy);
}
