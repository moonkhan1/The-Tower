using System.Collections;
using System.Collections.Generic;
using PuzzlePlatformer.Abstract.States;
using UnityEngine;
using UnityEngine.AI;
using Dreamteck.Splines;
using DG.Tweening;

namespace PuzzlePlatformer.States.EnemyState
{


    public class BackToPatrolState : IState
    {
        NavMeshAgent _navMeshAgent;
        PlayerController _target;
        EnemyController _enemyController;
        SplineFollower _splineFollower;
        public BackToPatrolState(EnemyController enemyController)
        {
            _navMeshAgent = enemyController.GetComponent<NavMeshAgent>();
            _splineFollower = enemyController.GetComponent<SplineFollower>();
            _target = enemyController.Target.GetComponent<PlayerController>();
            _enemyController = enemyController;
        }
        public void OnExit()
        {
            Debug.Log($"{nameof(BackToPatrolState)}{nameof(OnExit)}");
        }

        public void OnStart()
        {
            Debug.Log($"{nameof(BackToPatrolState)}{nameof(OnStart)}");
        }

        public void StateControl()
        {
            _navMeshAgent.SetDestination(_splineFollower.spline.position);
            _enemyController.transform.LookAt(_splineFollower.spline.GetPointPosition(0));
            _enemyController.transform.DOMove(new Vector3(_splineFollower.spline.GetPointPosition(0).x, _splineFollower.spline.GetPointPosition(0).y, _splineFollower.spline.GetPointPosition(0).z), 2f)
            .OnComplete(() =>
            {
                _enemyController.isInPatrol = true;
                _enemyController.transform.GetComponentInChildren<ParticleSystem>().Stop();
                _splineFollower.follow = true;
                _navMeshAgent.speed = 8;
                _splineFollower.Restart(0);
            });
        }
    }
}