using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : IEnemyBaseState
{
    public override void EnterState(EnemyController _enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(EnemyController _enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyController _enemy)
    {
        _enemy._navMesh.destination = _enemy._playerController.transform.position;
        _enemy._splineFollower.follow = false;





    }



}
