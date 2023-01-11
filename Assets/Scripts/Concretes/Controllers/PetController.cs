using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PetController : MonoBehaviour
{
    private PlayerController _playerController;
    Transform _transform;
    private bool startFollow = false;
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _transform = GetComponent<Transform>();
        _playerController.GetComponent<PlayerController>().isAngelTriggered += CanPetFollow;
    }

    private void OnEnable() {
        if (_playerController == null) return;
        _playerController.GetComponent<PlayerController>().isAngelTriggered += CanPetFollow;
    }

    

    private void OnDisable() {
        if (_playerController == null) return;
        _playerController.GetComponent<PlayerController>().isAngelTriggered -= CanPetFollow;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(startFollow)
            PetFollow();
    }

    private void CanPetFollow()
    {
       startFollow = true;
    }

    public void PetFollow()
    {

        Vector3 PrevPos = _playerController.transform.position + Vector3.forward * 4f;
        Vector3 CurrentPos = _transform.position;
        _transform.position = Vector3.Lerp(CurrentPos, PrevPos, 15 * Time.deltaTime);

        Vector3 LookAt =_playerController.transform.position;
        Quaternion newRot = Quaternion.LookRotation(LookAt, _playerController.transform.forward);
        _transform.rotation = Quaternion.Slerp(_playerController.transform.rotation, newRot, Time.deltaTime);
    }
}
