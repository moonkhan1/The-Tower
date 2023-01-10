// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public class EnemyGoPatrol : IEnemyBaseState
// {
//     public override void EnterState(EnemyController _enemy)
//     {
        
//     }

//     public override void OnCollisionEnter(EnemyController _enemy)
//     {
//         throw new System.NotImplementedException();
//     }

//     public override void UpdateState(EnemyController _enemy)
//     {
         
//         Debug.DrawRay(_enemy._fireTransform.position, _enemy._fireTransform.TransformDirection(Vector3.forward) * 25f, Color.blue);
//         Debug.Log("Did not Hit");
//         if (!_enemy._splineFollower.follow && _enemy.transform.position != _enemy._splineFollower.spline.position)
//         {
//             _enemy._navMesh.destination = _enemy._splineFollower.spline.position;
//             _enemy.transform.LookAt(_enemy._splineFollower.spline.GetPointPosition(0));
//             _enemy.transform.DOMove(new Vector3(_enemy._splineFollower.spline.GetPointPosition(0).x, 4.1f, _enemy._splineFollower.spline.GetPointPosition(0).z), 2.5f)
//             .OnComplete(() =>
//             {

//                 // _enemy._splineFollower.follow = true;
//                 // _enemy._splineFollower.Restart(0);
//                 // _enemy.SwitchState(_enemy.EnemyPatrol);
//             });
//         }
//         }
//     }

