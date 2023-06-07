using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.AI;
using DG.Tweening;
using CASP.SoundManager;
using PuzzlePlatformer.States;
using PuzzlePlatformer.States.EnemyState;

public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] public LayerMask _layer;
    [SerializeField] public Transform _fireTransform;
    [SerializeField] SplineFollower _splineFollower;
    public NavMeshAgent _navMesh;
    PlayerController _playerController;
    private Rigidbody _rb;
    [SerializeField] Transform _enemyTransform;
    private float _enemySpeed = 11f;
    private Sequence _seq;
    private bool isFollowingPlayer = false;
    public bool isInPatrol{get;set;}
    StateMachine _stateMachine;

    public Transform Target{get;set;}

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _splineFollower = GetComponent<SplineFollower>();
        Target = FindObjectOfType<PlayerController>().transform;
        _rb = GetComponent<Rigidbody>();
        _stateMachine = new StateMachine();
    }
    private void Start()
    {

        _enemyTransform = GetComponent<Transform>();
        PatrolState patrolState = new PatrolState(this);
        ChaseState chaseState = new ChaseState(this);
        BackToPatrolState backToPatrolState = new BackToPatrolState(this);

        _stateMachine.AddState(patrolState, chaseState, ()=> isFollowingPlayer);
        _stateMachine.AddState(chaseState, backToPatrolState, ()=> !isFollowingPlayer);
        _stateMachine.AddState(backToPatrolState, patrolState, ()=> isInPatrol);
        _stateMachine.SetState(patrolState);
    }

    void Update()
    {
        _stateMachine.StateControl();
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {

        RaycastHit hit;
        float interactionDistance = 25f;
        if (Physics.SphereCast(_enemyTransform.position, _enemyTransform.lossyScale.x * 8, _enemyTransform.forward, out hit, interactionDistance, _layer))
        {
            if (hit.transform.TryGetComponent(out PlayerController player))
            {
                isFollowingPlayer = true;
            }

        }
        else
        {
            if (isFollowingPlayer)
            {

                isFollowingPlayer = false;
            }

        }
    }

}





