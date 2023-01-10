using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.AI;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    [SerializeField] public LayerMask _layer;
    // [SerializeField] public GameObject _cakePrfb;
    [SerializeField] public Transform _fireTransform;
    public SplineFollower _splineFollower;
    public NavMeshAgent _navMesh;
    public PlayerController _playerController;
    private Rigidbody _rb;
    private Transform _transform;
    private float _enemySpeed = 11f;
    private Sequence _seq;
    private bool isFollowingPlayer = false;

    // IEnemyBaseState currentState;
    // public EnemyFollow EnemyFollow = new EnemyFollow();
    // public EnemyGoPatrol EnemyGoPatrol = new EnemyGoPatrol();
    // public EnemyPatrol EnemyPatrol = new EnemyPatrol();

    private void Start()
    {
        // currentState = EnemyPatrol;
        // currentState.EnterState(this);
    }

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _splineFollower = GetComponent<SplineFollower>();
        _playerController = FindObjectOfType<PlayerController>();
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (_playerController.GetComponent<PlayerController>().IsPlayerDead)
            _splineFollower.follow = false;
    }


 void OnDrawGizmos() {
    // public void CheckForPlayer()
    // {
        RaycastHit hit;
        if (Physics.SphereCast(_transform.position,_transform.lossyScale.x*8, _transform.forward, out hit, 25f,_layer))
        {
            // if (Physics.CapsuleCast(_fireTransform.position,_fireTransforms2.position,16f,_fireTransform.TransformDirection(Vector3.forward), out hit, 25f, _layer))
            // {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_fireTransform.position, _fireTransform.forward * hit.distance);
            Gizmos.DrawWireSphere(_fireTransform.position + _fireTransform.forward * hit.distance, _transform.lossyScale.x*8);
            if (hit.transform.gameObject.layer == 6)
            {
                _navMesh.destination = _playerController.transform.position;
                _splineFollower.follow = false;
                _transform.GetComponentInChildren<ParticleSystem>().Play();
                
            }

        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_fireTransform.position, _fireTransform.forward * 25f);
            Debug.Log("Did not Hit");
            _splineFollower.follow = true;
            _transform.GetComponentInChildren<ParticleSystem>().Stop();
            if (!_splineFollower.follow)
            {
                _navMesh.destination = _splineFollower.spline.position;
                _transform.LookAt(_splineFollower.spline.GetPointPosition(0));
                _transform.DOMove(new Vector3(_splineFollower.spline.GetPointPosition(0).x, _splineFollower.spline.GetPointPosition(0).y, _splineFollower.spline.GetPointPosition(0).z), 2.5f)
                .OnComplete(() =>
                {

                    _splineFollower.follow = true;
                    _splineFollower.Restart(0);
                });

            }

        }
    }
}





