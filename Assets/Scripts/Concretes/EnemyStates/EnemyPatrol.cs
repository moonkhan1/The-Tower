// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public class EnemyPatrol : IEnemyBaseState
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
      

//             RaycastHit hit;
//             if (Physics.Raycast(_enemy._fireTransform.position, _enemy._fireTransform.TransformDirection(Vector3.forward), out hit, 25f, _enemy._layer))
//             {
//                 Debug.DrawRay(_enemy._fireTransform.position, _enemy._fireTransform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
//                 while (hit.transform.gameObject.layer == 6)
//                 {
//                     Debug.Log("IFdeyik");
//                     // _enemy.SwitchState(_enemy.EnemyFollow);
//                     break;

//                 }

//             }
//             else
//             {
//                 Debug.Log("Elsedeyik");
//                 // _enemy.SwitchState(_enemy.EnemyGoPatrol);
//             }
//         }
//     }

