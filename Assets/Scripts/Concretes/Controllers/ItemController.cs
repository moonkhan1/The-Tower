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
        // _rb = GetComponent<Rigidbody>();
    }

private void Update() {
    
}
    private void OnCollisionEnter(Collision other)
    {

    }
    private void OnTriggerEnter(Collider other) {
        _devices.WhenTriggerInteractable(other,_transform);
        Debug.Log("Item Make Come forward");
        
    }
    // private void OnCollisionExit(Collision other)
    // {
    //     _devices.WhenTriggerInteractable(other.collider,false);
    //     DeviceManager.Instance.isDeviceAvtivated = false;

    //     Debug.Log("Item Make Went back");

    // }

    private void OnTriggerExit(Collider other) {
        _devices.WhenTriggerInteractable(other,_transform);
        Debug.Log("Item Make Come forward");
    }

}


