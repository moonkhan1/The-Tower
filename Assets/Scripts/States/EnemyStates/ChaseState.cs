using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzlePlatformer.Abstract.States;
using UnityEngine.AI;
using CASP.SoundManager;
using Dreamteck.Splines;

namespace PuzzlePlatformer.States.EnemyState
{
    public class ChaseState : IState
    {
        NavMeshAgent _navMeshAgent;
        PlayerController _target;
        EnemyController _enemyController;
        SplineFollower _splineFollower;
        public ChaseState(EnemyController enemyController)
        {
            _navMeshAgent = enemyController.GetComponent<NavMeshAgent>();
             _splineFollower = enemyController.GetComponent<SplineFollower>();
            _target = enemyController.Target.GetComponent<PlayerController>();
            _enemyController = enemyController;
        }
        public void OnExit()
        {
            Debug.Log($"{nameof(ChaseState)}{nameof(OnExit)}");
        }

        public void OnStart()
        {
            Debug.Log($"{nameof(ChaseState)}{nameof(OnStart)}");
        }

        public void StateControl()
        {
            SoundManager.Instance._enemySound.Play();
            _navMeshAgent.SetDestination(_target.transform.position);
            _navMeshAgent.speed = 11;
            _splineFollower.follow = false;
            _enemyController.GetComponentInChildren<ParticleSystem>().Play();
            _enemyController.isInPatrol = false;
        }
    }
}
