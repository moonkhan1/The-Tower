using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;
public class DeviceController : IDevice
{
    [SerializeField] Transform _movePosition;
    private PlayerController _playerController;
    private ItemController _itemController;
    public override event System.Action isPicTriggered;


    public DeviceController(PlayerController playerController)
    {
        _playerController = playerController.GetComponent<PlayerController>();

    }
    public DeviceController(ItemController itemController)
    {
        _itemController = itemController.GetComponent<ItemController>();
    }

    public override void WhenTriggerInteractable(Collider other)
    {
        foreach (KeyValuePair<GameObject, GameObject> items in DeviceManager.Instance.itemsDevices)
        {
            if (other.name == items.Key.name && items.Key.name.Contains("Gate"))
            {
                items.Value.transform.DOMove(items.Value.transform.GetChild(0).position, 2f)
                .SetEase(Ease.Linear);
                Debug.Log("Touched Gate");
            }

            if (other.name == items.Key.name && items.Key.name.Contains("Rotate"))
            {
                items.Value.transform.GetComponentInChildren<ParticleSystem>().Play();
                items.Value.transform.DOMoveY(items.Value.transform.GetChild(0).position.y, 2f).OnComplete(() =>
                {
                    items.Value.GetComponentInChildren<ParticleSystem>().Stop();
                });
                Debug.Log("Touched GateKeyIron");
            }
        }
    }
    // public override void WhenTriggerInteractable(Collider other, bool isActive)
    // {
    //     foreach (KeyValuePair<GameObject, GameObject> items in DeviceManager.Instance.itemsDevices)
    //     {
    //         if (other.name == items.Key.name && items.Key.name.Contains("Duyme") && isActive)
    //         {
    //             items.Value.transform.DOMoveX(items.Value.transform.GetChild(0).position.x, 2f);

    //         }
    //         if (other.name == items.Key.name && items.Key.name.Contains("Duyme") && !isActive)
    //         {
    //             items.Value.transform.DOMoveX(items.Value.transform.GetChild(1).position.x, 2f);
    //         }

    //     }
    // }

    public override void WhenTriggerInteractable(Collider other, Transform This)
    {
        foreach (KeyValuePair<GameObject, GameObject> items in DeviceManager.Instance.itemsDevices)
        {
            if (other.name == items.Key.name && items.Key.name.Contains("Amulet"))
            {
                if (This.transform.parent == null)
                {
                    if(other.tag == This.tag)
                        DeviceManager.Instance.Level1AmuletCount++;
                    This.DOJump(other.transform.GetChild(0).position,0.7f,1, 0.6f).OnComplete(() =>
                    {
                        This.rotation = other.transform.GetChild(0).rotation;
                        // other.GetComponent<Collider>().isTrigger = false;
                    });

                }
                if (other.tag == This.tag && This.transform.parent != null)
                    // other.GetComponent<Collider>().isTrigger = true;
                    DeviceManager.Instance.Level1AmuletCount--;
            }
            if (DeviceManager.Instance.Level1AmuletCount == 3)
            {
                foreach (var item in DeviceManager.Instance.itemsDevices.Where(u => u.Key.name.Contains("Amulet")))
                {
                    item.Value.transform.DOMoveX(item.Value.transform.GetChild(0).position.x, 2f);
                    item.Key.transform.GetComponentInChildren<ParticleSystem>().Play();
                    item.Value.transform.GetComponentInChildren<ParticleSystem>().Play();
                }

            }



            //  if (other.tag != This.tag)
            //  {
            //     This.DOJump(new Vector3(This.position.x + Random.Range(2, 4),0, This.position.z + Random.Range(3, 5)),0.7f,1,0.4f);
            //  }
        }
    }


    public override void WhenTriggerInteractable(Transform player, Vector3 playerDirection, Collider other, Quaternion isRotationCorrect)
    {
        foreach (KeyValuePair<GameObject, GameObject> items in DeviceManager.Instance.itemsDevices)
        {
            if (other.name == items.Key.name && items.Key.name.Contains("Roll"))
            {

                bool isPictureCorrect = false;
                player.DOJump(items.Key.transform.GetChild(0).position, 0.7f, 1, 0.2f);
                if (playerDirection.z > 0 && !isPictureCorrect)
                {
                    items.Key.transform.DORotate(new Vector3(0, 0, items.Key.transform.rotation.z - 5), 0.1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental)
                    .SetRelative();
                    items.Value.transform.DORotate(new Vector3(0, items.Value.transform.rotation.y - 1, 0), 0.1f).SetLoops(-1, LoopType.Incremental)
                    .SetRelative();
                }
                if (playerDirection.z < 0 && !isPictureCorrect)
                {
                    items.Key.transform.DORotate(new Vector3(0, 0, items.Key.transform.rotation.z + 5), 0.1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental)
                    .SetRelative();
                    items.Value.transform.DORotate(new Vector3(0, items.Value.transform.rotation.y + 1, 0), 0.1f).SetLoops(-1, LoopType.Incremental)
                    .SetRelative();
                }
                if (playerDirection.z == 0)
                {
                    items.Key.transform.DOKill();
                    items.Value.transform.DOKill();
                }
                // DeviceManager.Instance.Level2Mosaic.All(u => u.transform.localEulerAngles.y >= isRotationCorrect.eulerAngles.y - 2);

                if ((DeviceManager.Instance.Level2Mosaic.All(u =>
                System.Math.Truncate(Mathf.Abs(u.transform.localEulerAngles.y)) >= isRotationCorrect.eulerAngles.y - 4
                && System.Math.Truncate(Mathf.Abs(u.transform.localEulerAngles.y)) <= isRotationCorrect.eulerAngles.y + 4)))
                {
                    Debug.Log("Correct");
                    isPictureCorrect = true;
                    items.Key.transform.DOKill();
                    items.Value.transform.DOKill();
                    isPicTriggered?.Invoke();
                }

                if (Input.GetKey(KeyCode.E))
                {
                    items.Key.transform.DOKill();
                    items.Value.transform.DOKill();
                    player.DOJump(items.Key.transform.GetChild(1).position, 0.8f, 1, 0.4f);

                }

            }



        }
    }

}
