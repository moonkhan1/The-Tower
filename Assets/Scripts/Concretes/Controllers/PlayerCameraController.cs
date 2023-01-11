using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CASP.CameraManager;
public class PlayerCameraController : IPlayerCamera
{

    Collider _playerCollider;


    public PlayerCameraController(PlayerController playerController)
    {
        _playerCollider = playerController.GetComponent<Collider>();
    }

    public override void TriggerForCamera(Collider other)
    {
        foreach (KeyValuePair<GameObject, GameObject> items in CameraList.Instance.CameraListControl)
        {
        if(other.name == items.Key.name )
        {
            CameraManager.Instance.OpenCamera(items.Value.name,0.5f,CameraEaseStates.EaseInOut);
            CameraManager.Instance.SetFollow(items.Value.name,_playerCollider.transform);
            CameraManager.Instance.SetLookAt(items.Value.name,_playerCollider.transform);

            
            Debug.Log("Kameralar Haydi!");
        }
        }
    }
}
