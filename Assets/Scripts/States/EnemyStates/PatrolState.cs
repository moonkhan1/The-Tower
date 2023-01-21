using System.Collections;
using System.Collections.Generic;
using PuzzlePlatformer.Abstract.States;
using UnityEngine;
using Dreamteck.Splines;

namespace PuzzlePlatformer.States.EnemyState
{
    public class PatrolState : IState
    {

        SplineFollower _splineFollower;
        PlayerController _target;
        EnemyController _enemyController;
        public PatrolState(EnemyController enemyController)
        {
            _splineFollower = enemyController.GetComponent<SplineFollower>();
            _target = enemyController.Target.GetComponent<PlayerController>();
            _enemyController = enemyController;
        }
        public void OnExit()
        {
            Debug.Log($"{nameof(PatrolState)}{nameof(OnExit)}");
        }

        public void OnStart()
        {
            Debug.Log($"{nameof(PatrolState)}{nameof(OnStart)}");
        }

        public void StateControl()
        {
            if (!_target.IsPlayerStop)
            {
                _splineFollower.follow = true;
                Debug.Log(_target.IsPlayerStop);
            }
            else
                _splineFollower.follow = false;
        }
    }
}
