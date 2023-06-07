using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    IDevice _devices;
    Collider _collider;
    Rigidbody _rb;
    Transform _transform;

    private void Awake()
    {
        _devices = new DeviceController(this);
        _collider = GetComponent<Collider>();
        _transform = GetComponent<Transform>();
    }
    
    private void OnTriggerEnter(Collider other) {
        _devices.WhenTriggerInteractable(other,_transform);
        
    }

    private void OnTriggerExit(Collider other) {
        _devices.WhenTriggerInteractable(other,_transform);
    }

}


