using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDevice
{
   public abstract void WhenTriggerInteractable(Collider other);
   // public abstract void WhenTriggerInteractable(Collider other,bool isActive);
   public abstract void WhenTriggerInteractable(Collider other, Transform This);
   public abstract void WhenTriggerInteractable(Transform player,Vector3 playerDirection, Collider other,Quaternion isRotationCorrect);
   public abstract event System.Action isPicTriggered;
}
