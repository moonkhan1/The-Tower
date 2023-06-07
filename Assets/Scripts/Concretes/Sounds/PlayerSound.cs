using System;
using System.Collections;
using System.Collections.Generic;
using CASP.SoundManager;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerController _playerController;
    private float _footStepsTimer;
    private readonly float _footStepsTimerMax = 0.8f;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _playerController.isRunning += PlayerControllerOnRunning;
    }

    private void OnDisable()
    {
        _playerController.isRunning -= PlayerControllerOnRunning;
    }

    private void PlayerControllerOnRunning(bool obj)
    {
        if(!obj) return;
        _footStepsTimer += Time.deltaTime;
        if (_footStepsTimer >= _footStepsTimerMax)
        {
            float volume = 1f;
            if (_playerController.Direction.magnitude > 1f)
            {
                SoundManager.Instance.PlaySound("CatFoot", _playerController.transform.position, volume);
                _footStepsTimer = 0f;
            }

        }
    }
    
}
