using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.AI;
using DG.Tweening;
using CASP.SoundManager;
using PuzzlePlatformer.States;
using PuzzlePlatformer.States.EnemyState;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public LayerMask _layer;
    // [SerializeField] public GameObject _cakePrfb;
    [SerializeField] public Transform _fireTransform;
    [SerializeField] SplineFollower _splineFollower;
    public NavMeshAgent _navMesh;
    private PlayerController _playerController;
    private Rigidbody _rb;
    [SerializeField] Transform _enemyTransform;
    private float _enemySpeed = 11f;
    private Sequence _seq;
    private bool isFollowingPlayer = false;
    StateMachine _stateMachine;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _splineFollower = GetComponent<SplineFollower>();
        _playerController = FindObjectOfType<PlayerController>();
        _rb = GetComponent<Rigidbody>();
        _stateMachine = new StateMachine();
    }
    private void Start()
    {

        _enemyTransform = GetComponent<Transform>();
        PatrolState patrolState = new PatrolState();
        ChaseState chaseState = new ChaseState();
        BackToPatrolState backToPatrolState = new BackToPatrolState();

        _stateMachine.AddState(patrolState, chaseState, ()=> isFollowingPlayer);
        _stateMachine.AddState(chaseState, backToPatrolState, ()=> !isFollowingPlayer);
        _stateMachine.AddState(backToPatrolState, patrolState, ()=> _splineFollower.follow);

    }

    void Update()
    {
        if (_playerController.GetComponent<PlayerController>().IsPlayerStop)
            _splineFollower.follow = false;
        _stateMachine.StateControl();
    }


    void OnDrawGizmos()
    {
        // public void CheckForPlayer()
        // {
        RaycastHit hit;
        if (Physics.SphereCast(_enemyTransform.position, _enemyTransform.lossyScale.x * 8, _enemyTransform.forward, out hit, 25f, _layer))
        {
            // if (Physics.CapsuleCast(_fireTransform.position,_fireTransforms2.position,16f,_fireTransform.TransformDirection(Vector3.forward), out hit, 25f, _layer))
            // {
            Gizmos.color = new Color(32, 32, 32, 0);
            Gizmos.DrawRay(_fireTransform.position, _fireTransform.forward * hit.distance);
            Gizmos.DrawWireSphere(_fireTransform.position + _fireTransform.forward * hit.distance, _enemyTransform.lossyScale.x * 8);
            if (hit.transform.tag == "Player")
            {
                isFollowingPlayer = true;
                SoundManager.Instance._enemySound.Play();
                _navMesh.destination = _playerController.transform.position;
                _navMesh.speed = 11;
                _splineFollower.follow = false;
                _enemyTransform.GetComponentInChildren<ParticleSystem>().Play();

            }

        }
        else
        {
            isFollowingPlayer = false;
            Gizmos.color = new Color(32, 32, 32, 0);
            Gizmos.DrawRay(_fireTransform.position, _fireTransform.forward * 25f);
            _enemyTransform.GetComponentInChildren<ParticleSystem>().Stop();

            // _splineFollower.follow = true;
            // SoundManager.Instance._enemySound.Stop();
            if (!_splineFollower.follow)
            {

                _navMesh.destination = _splineFollower.spline.position;
                _enemyTransform.LookAt(_splineFollower.spline.GetPointPosition(0));
                _enemyTransform.DOMove(new Vector3(_splineFollower.spline.GetPointPosition(0).x, _splineFollower.spline.GetPointPosition(0).y, _splineFollower.spline.GetPointPosition(0).z), 2f)
                .OnComplete(() =>
                {
                    
                    _splineFollower.follow = true;
                    _navMesh.speed = 8;
                    _splineFollower.Restart(0);
                });
            }

        }
    }

    // IEnumerator WaitAndGoBack()
    // {
    //     yield return new WaitForSeconds(2f);


    // }
}





